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
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        if (boss.IsDefenseRange() && boss.CanDefense())
        {
            stateMachine.ChangeState(stateMachine.defenseState);
            return;
        }
        
        if (boss.IsAttackRange() && boss.CanAttack())
        {
            stateMachine.ChangeState(stateMachine.attackState);
            return;
        }

        boss.Trace();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
