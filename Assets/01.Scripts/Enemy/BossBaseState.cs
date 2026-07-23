using UnityEngine;

public class BossBaseState : IState
{
    protected BossController boss;
    protected BossStateMachine stateMachine;

    public BossBaseState(BossController boss, BossStateMachine stateMachine)
    {
        this.boss = boss;
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