using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public static event Action OnLevelFinished;

    [SerializeField] private CameraController mainCamera = default;
    [SerializeField] private LevelUi levelUi = default;
    [Space]
    [SerializeField] private Player playerPrefab = default;
    [SerializeField] private Bounds playerSpawnZoneBounds = default;
    [Space]
    [SerializeField] private QuestObject[] questObjectsPrefabs = default;
    [SerializeField] private Bounds questObjectsSpawnZoneBounds = default;
    [SerializeField] private float minDistanceBetweenQuestObjects = default;
    [SerializeField] private float minQuestObjectDistanceToPlayer = default;

    private Player player;
    private List<QuestObject> questObjects = new List<QuestObject>();
    private int enabledQuestObjectIndex;


    private void Awake()
    {
        QuestObject.OnActivation += OnQuestObjectActivation;
        QuestObject.OnInvalidObjectActivationTry += OnInvalidObjectActivationTry;
    }


    private void OnDestroy()
    {
        QuestObject.OnActivation -= OnQuestObjectActivation;
        QuestObject.OnInvalidObjectActivationTry -= OnInvalidObjectActivationTry;
    }

    public void Play()
    {
        player = Instantiate(playerPrefab, GetRandomPositionInBounds(playerSpawnZoneBounds), Quaternion.identity, transform);

        mainCamera.StartFollowing(player.transform);

        List<QuestObject> createdQuestObjects = new List<QuestObject>();

        foreach (var questObjectPrefab in questObjectsPrefabs)
        {
            Vector3 position = GetRandomPositionInBounds(questObjectsSpawnZoneBounds);

            while (((player.transform.position - position).magnitude < minQuestObjectDistanceToPlayer) ||
                        createdQuestObjects.Exists((questObject) => (questObject.transform.position - position).magnitude < minDistanceBetweenQuestObjects))
            {
                position = GetRandomPositionInBounds(questObjectsSpawnZoneBounds);
            }

            QuestObject createdQuestObject = Instantiate(questObjectPrefab, position, Quaternion.identity, transform);
            createdQuestObjects.Add(createdQuestObject);
        }

        while (createdQuestObjects.Count > 0)
        {
            QuestObject randomObject = createdQuestObjects[Random.Range(0, createdQuestObjects.Count)];
            questObjects.Add(randomObject);
            createdQuestObjects.Remove(randomObject);
        }

        levelUi.CreateIcons(questObjects);
        for (int i = 0; i < questObjects.Count; i++)
        {
            questObjects[i].SetActivationEnabled(i == 0);
        }

        Vector3 GetRandomPositionInBounds(Bounds bounds)
        {
            return bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x),
                                    Random.Range(-bounds.extents.y, bounds.extents.y), Random.Range(-bounds.extents.z, bounds.extents.z));
        }
    }


    private void OnQuestObjectActivation()
    {
        Debug.Log("Activate");
        enabledQuestObjectIndex++;

        if (enabledQuestObjectIndex == questObjects.Count)
        {
        }
        else
        {
            questObjects[enabledQuestObjectIndex].SetActivationEnabled(true);
        }
    }


    private void OnInvalidObjectActivationTry()
    {
        questObjects[enabledQuestObjectIndex].SetActivationEnabled(false);
        for (int i = enabledQuestObjectIndex - 1; i >= 0; i--)
        {
            questObjects[i].SetActivationEnabled(i == 0);
            questObjects[i].Deactivate();
        }

        enabledQuestObjectIndex = 0;
    }
}
