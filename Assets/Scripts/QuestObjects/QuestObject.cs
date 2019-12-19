using System;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public static event Action OnActivation;
    public static event Action OnInvalidObjectActivationTry;


    [SerializeField] private Sprite icon = default;
    [SerializeField] private ParticleSystem activationEffectPrefab = default;
    [SerializeField] private ParticleSystem invalidActivationEffectPrefab = default;
    [SerializeField] private Transform effectsSpawnRoot = default;

    private ParticleSystem activationEffect;

    protected bool WasActivated { get; private set; }


    public Sprite Icon => icon;

    protected bool CanBeActivated { get; private set; }


    private void Start()
    {
        activationEffect = Instantiate(activationEffectPrefab, effectsSpawnRoot.position, effectsSpawnRoot.rotation, effectsSpawnRoot);
        activationEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void SetActivationEnabled(bool canBeActivated)
    {
        CanBeActivated = canBeActivated;
    }


    public virtual void Deactivate()
    {
        WasActivated = false;
        activationEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }


    protected virtual void Activate()
    {
        WasActivated = true;
        CanBeActivated = false;

        activationEffect.Play(true);
        OnActivation?.Invoke();
    }


    protected void TryActivateInvalidObject()
    {
        Instantiate(invalidActivationEffectPrefab, effectsSpawnRoot.position, effectsSpawnRoot.rotation, effectsSpawnRoot);
        OnInvalidObjectActivationTry?.Invoke();
    }
}
