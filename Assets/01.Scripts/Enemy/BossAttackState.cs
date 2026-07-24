using UnityEngine;

public class BossAttackState : BossBaseState
{
    public BossAttackState(BossController boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        boss.Attack();
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
