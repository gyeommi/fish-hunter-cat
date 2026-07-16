using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
    }

    public override void Update()
    {
        base.Update();
        //감지 범위 내에 플레이어가 있는지 검사
        //없으면 Idle 상태로 변경
        //있으면 거리를 조금 좁혀서 Attack 상태로 변경

        if (!enemy.IsDetectPlayer())
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        if (!enemy.IsAttackRange())
        {
            stateMachine.ChangeState(stateMachine.traceState);
            return;
        }

        enemy.Attack();
    }
}
