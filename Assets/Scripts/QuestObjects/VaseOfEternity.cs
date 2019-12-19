using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseOfEternity : QuestObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (WasActivated)
        {
            return;
        }

        if (other.attachedRigidbody?.GetComponent<Sword>() != null)
        {
            if (CanBeActivated)
            {
                Activate();
            }
            else
            {
                TryActivateInvalidObject();
            }
        }
    }
}
