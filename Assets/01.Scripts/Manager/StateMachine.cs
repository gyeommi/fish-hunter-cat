using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();

    public void FixedUpdate();

}

public class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
    public void Update() => currentState?.Update();

    public void FixedUpdate() => currentState?.FixedUpdate();
}
