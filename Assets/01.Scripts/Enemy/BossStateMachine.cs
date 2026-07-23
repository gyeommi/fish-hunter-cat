using UnityEngine;

public class BossStateMachine : StateMachine
{
    public BossIdleState idleState;
    public BossTraceState traceState;
    public BossAttackState attackState;
    public BossDefenseState defenseState;
    public BossDeadState deadState;

    public BossStateMachine(BossController boss)
    {
        idleState = new BossIdleState(boss, this);
        traceState = new BossTraceState(boss, this);
        attackState = new BossAttackState(boss, this);
        defenseState = new BossDefenseState(boss, this);
        deadState = new BossDeadState(boss, this);
    }
}