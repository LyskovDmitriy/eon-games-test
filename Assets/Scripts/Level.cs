using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
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

        Vector3 GetRandomPositionInBounds(Bounds bounds)
        {
            return bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x),
                                    Random.Range(-bounds.extents.y, bounds.extents.y), Random.Range(-bounds.extents.z, bounds.extents.z));
        }
    }
}
