using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public static event Action OnActivation;
    public static event Action OnInvalidObjectActivationTry;


    [SerializeField] private Sprite icon = default;

    protected bool WasActivated { get; private set; }


    public Sprite Icon => icon;

    protected bool CanBeActivated { get; private set; }

    public void SetActivationEnabled(bool canBeActivated)
    {
        CanBeActivated = canBeActivated;
    }


    public virtual void Deactivate()
    {
        WasActivated = false;
    }


    protected virtual void Activate()
    {
        WasActivated = true;
        CanBeActivated = false;

        OnActivation?.Invoke();
    }
}
