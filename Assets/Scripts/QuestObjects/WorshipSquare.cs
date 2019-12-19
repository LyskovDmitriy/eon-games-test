using System.Collections;
using UnityEngine;

public class WorshipSquare : QuestObject
{
    [SerializeField] private float secondsToActivate = default;
    [SerializeField] private Collider activationTrigger = default;

    private Coroutine activationCoroutine;


    private IEnumerator Activation(Player player)
    {
        float activationTimer = 0.0f;

        while (activationTimer < secondsToActivate)
        {
            if (Mathf.Approximately(player.CurrentSpeedX, 0.0f) && Mathf.Approximately(player.CurrentSpeedZ, 0.0f))
            {
                activationTimer += Time.deltaTime;
            }
            else
            {
                activationTimer = 0.0f;
            }

            yield return null;
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
            if (CanBeActivated)
            {
                activationCoroutine = StartCoroutine(Activation(player));
            }
            else
            {
                TryActivateInvalidObject();
            }
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
