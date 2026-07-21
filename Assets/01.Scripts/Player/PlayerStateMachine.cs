using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerDashState dashState;
    public PlayerAttackState attackState;

    public PlayerStateMachine(PlayerController player)
    {
        idleState = new PlayerIdleState(player, this);
        moveState = new PlayerMoveState(player, this);
        jumpState = new PlayerJumpState(player, this);
        dashState = new PlayerDashState(player, this);
        attackState = new PlayerAttackState(player, this);
    }
}
