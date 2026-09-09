using System;
using System.Collections.Generic;
using UnityEngine;

#region Methods
public class StateMachine
{
    public IState CurrentState { get; private set; }

    private readonly Dictionary<Type, IState> _states = new();

    public void RegisterState<T>(IState state) where T : IState
    {
        _states[typeof(T)] = state;
    }

    public T GetCurrentState<T>() where T : IState
    {
        return (T)CurrentState;
    }
    public T GetState<T>() where T: IState
    {
        return (T)_states[typeof(T)];
    }
    public void Initialize<T>() where T : IState
    {
        if (_states[typeof(T)] != null)
        {
            CurrentState = _states[typeof(T)];
            CurrentState.Enter(this);
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

        CurrentState?.Exit(this);
        CurrentState = _states[typeof(T)];
        CurrentState.Enter(this);
    }

    public void Update() => CurrentState?.Update(this);
    public void FixUpdate() => CurrentState?.FixUpdate(this);
}
#endregion
