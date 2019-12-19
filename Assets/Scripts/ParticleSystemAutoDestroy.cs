using System.Collections;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private IEnumerator Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        yield return new WaitWhile(() => particleSystem.IsAlive(true));

        Destroy(gameObject);
    }
}
