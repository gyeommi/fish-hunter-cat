using UnityEngine;

public class BossTraceState : BossBaseState
{
    public BossTraceState(BossController boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        boss.PlayMoveAnim(true);
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
        boss.PlayMoveAnim(false);
    }

    public override void Update()
    {
        base.Update();
        
        if (!boss.IsDetectPlayer())
            stateMachine.ChangeState(stateMachine.idleState);

        if (boss.IsAttackRange())
            stateMachine.ChangeState(stateMachine.attackState);

        if (boss.IsDefenseRange() && boss.CanDefense())
            stateMachine.ChangeState(stateMachine.defenseState);

        boss.Trace();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
