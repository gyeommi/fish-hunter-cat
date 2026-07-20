using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyIdleState idleState;
    public EnemyTraceState traceState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;

    public EnemyStateMachine(EnemyController enemy)
    {
        idleState = new EnemyIdleState(enemy, this);
        traceState = new EnemyTraceState(enemy, this);
        attackState = new EnemyAttackState(enemy, this);
        deadState = new EnemyDeadState(enemy, this);
    }
}
