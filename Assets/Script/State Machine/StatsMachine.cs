using System;
using System.Collections.Generic;
using UnityEngine;

#region Methods
public class StatsMachine
{
    public IState CurrentState { get; private set; }

    private readonly Dictionary<Type, IState> _states = new();

    public void RegisterState<T>(IState state) where T : IState
    {
        _states[typeof(T)] = state;
    }

    public void Initialize<T>() where T : IState
    {
        if (_states[typeof(T)] != null)
        {
            CurrentState = _states[typeof(T)];
            CurrentState.Enter();
        }
        else
        {
            CurrentState = null;
            Debug.LogError($"Stats {typeof(T)} does not exist or isn't set");
        }
    }

    public void TransitionTo<T>() where T : IState
    {
        if (_states[typeof(T)] == null)
        {
            CurrentState = null;
            Debug.LogError($"Stats {typeof(T)} does not exist or isn't set");
        }

        CurrentState?.Exit();
        CurrentState = _states[typeof(T)];
        CurrentState.Enter();
    }

    public void Update() => CurrentState?.Update();
    public void FixUpdate() => CurrentState?.FixUpdate();
}
#endregion
