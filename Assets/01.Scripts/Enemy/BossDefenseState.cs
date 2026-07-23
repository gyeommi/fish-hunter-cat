using UnityEngine;

public class BossDefenseState : BossBaseState
{
    public BossDefenseState(BossController boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        boss.PlayDefenseAnim();
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
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

        if (boss.IsAttackRange())
        {
            stateMachine.ChangeState(stateMachine.attackState);
        }

        boss.Defense();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
