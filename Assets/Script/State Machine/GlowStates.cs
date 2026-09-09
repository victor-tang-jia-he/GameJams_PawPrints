using UnityEngine;
using UnityEngine.Events;

public class GlowingState : IState
{
    private Timer glowTimer;

    public UnityEvent<float> GlowingEvent = new();
    public UnityEvent OnFullyGlowedEvent = new();
    public GlowingState(float glowTime)
    {
        glowTimer = new Timer(glowTime);
    }
    void IState.Enter(StateMachine stateMachine)
    {
        glowTimer.Reset();
    }
    void IState.Update(StateMachine stateMachine)
    {
        bool isCompleted = glowTimer.AddTime(Time.deltaTime);
        if (isCompleted)
        {
            stateMachine.TransitionTo<FullyGlowedState>();
            OnFullyGlowedEvent.Invoke();
        } else
        {
            GlowingEvent.Invoke(glowTimer.GetRatio());
        }
    }
    void IState.FixUpdate(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
    void IState.Exit(StateMachine stateMachine)
    {
        glowTimer.Reset();
    }
}

public class FullyGlowedState : IState
{
    private Timer fullyGlowedTimer;

    public FullyGlowedState(float fullyGlowedTime)
    {
        fullyGlowedTimer = new Timer(fullyGlowedTime);
    }
    void IState.Enter(StateMachine stateMachine)
    {
        fullyGlowedTimer.Reset();
    }
    void IState.Update(StateMachine stateMachine)
    {
       if (fullyGlowedTimer.AddTime(Time.deltaTime))
       {
            stateMachine.TransitionTo<UnglowingState>();
       }
    }
    void IState.FixUpdate(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
    void IState.Exit(StateMachine stateMachine)
    {
        fullyGlowedTimer.Reset();
    }
}

public class UnglowingState : IState
{
    private Timer unglowTimer;
    public UnityEvent<float> UnglowingEvent = new();
    public UnityEvent OnUnglowedEvent = new();
    public UnglowingState(float glowTime)
    {
        unglowTimer = new Timer(glowTime);
    }
    void IState.Enter(StateMachine stateMachine)
    {
        unglowTimer.Reset();
    }
    void IState.Update(StateMachine stateMachine)
    {
        bool isCompleted = unglowTimer.AddTime(Time.deltaTime);
        if (!isCompleted)
        {
            UnglowingEvent.Invoke(unglowTimer.GetRatio());
        } else
        {
            stateMachine.TransitionTo<UnglowState>();
            OnUnglowedEvent.Invoke();
        }
    }
    void IState.FixUpdate(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
    void IState.Exit(StateMachine stateMachine)
    {
        unglowTimer.Reset();
    }
}

public class UnglowState : IState
{
    
    // nothing to do just idle state
    void IState.Enter(StateMachine stateMachine)
    {
        
    }
    void IState.Update(StateMachine stateMachine)
    {
        
    }
    void IState.FixUpdate(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
    void IState.Exit(StateMachine stateMachine)
    {
        
    }
}

class GlowStateMachineInit
{
    public static StateMachine Init(float glowingTime,float fullyGlowedTime,float unglowingTime)
    {
        GlowingState glowingState = new GlowingState(glowingTime);
        FullyGlowedState fullyGlowedState = new FullyGlowedState(fullyGlowedTime);
        UnglowingState unglowingState = new UnglowingState(unglowingTime);
        UnglowState unglowState = new UnglowState();

        StateMachine stateMachine = new StateMachine();
        stateMachine.RegisterState<GlowingState>(glowingState);
        stateMachine.RegisterState<FullyGlowedState>(fullyGlowedState);
        stateMachine.RegisterState<UnglowingState>(unglowingState);
        stateMachine.RegisterState<UnglowState>(unglowState);
        

        return stateMachine;
    }
}
