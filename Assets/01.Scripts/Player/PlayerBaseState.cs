using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        //이 상태에 들어갈 때 동작할 기능
    }

    public virtual void Exit()
    {
        //이 상태에서 나갈 때 동작할 기능
    }

    public virtual void Update()
    {
        //이 상태에서 할 행동
    }

    public virtual void FixedUpdate()
    {

    }
}
