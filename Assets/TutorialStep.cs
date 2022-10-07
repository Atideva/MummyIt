using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    [HideInInspector] public int id;
    public void Init(Tutorial tut) => Tutorial = tut;
    protected Tutorial Tutorial { get; private set; }

    public void StartStep() => OnStart();
    protected abstract void OnStart();
    public event Action<TutorialStep> OnComplete = delegate { };
    public void Complete() => OnComplete(this);
}