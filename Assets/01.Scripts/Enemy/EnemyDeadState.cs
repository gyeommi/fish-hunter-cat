using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        enemy.PlayDeadAnim();
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
}
