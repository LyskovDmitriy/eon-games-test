using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Level level = default;
    [SerializeField] private float secondsToReloadLevelAfterFinish = default;


    private void Awake()
    {
        Level.OnFinished += OnLevelFinished;
    }


    private void OnDestroy()
    {
        Level.OnFinished -= OnLevelFinished;
    }

    private void Start()
    {
        level.Play();
    }


    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(secondsToReloadLevelAfterFinish);

        level.Clear();
        level.Play();
    }


    private void OnLevelFinished() => StartCoroutine(Reload());
}
