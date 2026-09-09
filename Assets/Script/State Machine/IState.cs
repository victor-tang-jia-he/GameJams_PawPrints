using UnityEngine;

public interface IState
{
    void Enter(StateMachine stateMachine);
    void Update(StateMachine stateMachine);
    void FixUpdate(StateMachine stateMachine);
    void Exit(StateMachine stateMachine);
}
