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
        if (boss.attackCount < 3)
        {
            boss.SetAttack(false);
            boss.PlayNormalAttackAnim();
        }
        else
        {
            boss.SetAttack(true);
            boss.PlayBetterAttackAnim();
        }
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
        boss.attackCount++;

        if (boss.attackCount >= 4)
            boss.attackCount = 0;
    }

    public override void Update()
    {
        base.Update();

        if (!boss.IsDetectPlayer())
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        if (!boss.IsAttackRange())
        {
            stateMachine.ChangeState(stateMachine.traceState);
            return;
        }

        if (boss.IsDefenseRange() && boss.CanDefense())
        {
            stateMachine.ChangeState(stateMachine.defenseState);
            return;
        }

        boss.Attack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
