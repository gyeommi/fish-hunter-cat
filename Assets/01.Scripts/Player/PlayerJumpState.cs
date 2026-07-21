using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        player.PlayJumpAnim(true);
        player.Jump();
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
        player.PlayJumpAnim(false);
    }

    public override void Update()
    {
        base.Update();

        if (player.dashPressed)
            stateMachine.ChangeState(stateMachine.dashState);

        if (player.meleeAttackPressed)
            stateMachine.ChangeState(stateMachine.attackState);

        if (player.rangedAttackPressed)
            stateMachine.ChangeState(stateMachine.attackState);

        player.Move();

        if (player.isGround)
        {
            if (player.dir == 0)
                stateMachine.ChangeState(stateMachine.idleState);
            else
                stateMachine.ChangeState(stateMachine.moveState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
