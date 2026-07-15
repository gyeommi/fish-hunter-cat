using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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
        //있으면 Trace 상태로 변경..!

        if (enemy.IsDetectPlayer())
            stateMachine.ChangeState(stateMachine.traceState);

        enemy.Idle();
    }
}
