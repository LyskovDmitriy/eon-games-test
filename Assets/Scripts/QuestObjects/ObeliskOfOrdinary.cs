using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskOfOrdinary : QuestObject
{
    [SerializeField] private Collider activationTrigger = default;


    private Coroutine activationCoroutine;


    private IEnumerator Activation(Player player)
    {
        float rotatedAngle = 0.0f;
        Vector3 lastDirectionToPlayer = (player.transform.position - transform.position).normalized;

        while (Mathf.Abs(rotatedAngle) < 360.0f)
        {
            yield return null;

            Vector3 newDirectionToPlayer = (player.transform.position - transform.position).normalized;
            rotatedAngle += Vector3.SignedAngle(lastDirectionToPlayer, newDirectionToPlayer, Vector3.up);
            lastDirectionToPlayer = newDirectionToPlayer;
        }

        Activate();
        activationCoroutine = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (WasActivated)
        {
            return;
        }

        Player player = other.attachedRigidbody?.GetComponent<Player>();
        if (player != null)
        {
            activationCoroutine = StartCoroutine(Activation(player));
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Player player = other.attachedRigidbody?.GetComponent<Player>();
        if ((player != null) && (activationCoroutine != null))
        {
            StopCoroutine(activationCoroutine);
            activationCoroutine = null;
        }
    }


    protected override void Activate()
    {
        activationTrigger.gameObject.SetActive(false);

        base.Activate();
    }


    public override void Deactivate()
    {
        activationTrigger.gameObject.SetActive(true);

        base.Deactivate();
    }
}
