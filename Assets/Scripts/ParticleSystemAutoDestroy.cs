using System.Collections;
using System.Collections.Generic;
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
