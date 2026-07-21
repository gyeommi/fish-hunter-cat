using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        player.PlayMoveAnim(true);
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
        player.PlayMoveAnim(false);
    }

    public override void Update()
    {
        base.Update();
        
        if (player.dashPressed)
            stateMachine.ChangeState(stateMachine.dashState);

        if (player.jumpPressed)
            stateMachine.ChangeState(stateMachine.jumpState);
        
        if (player.dir == 0)
            stateMachine.ChangeState(stateMachine.idleState);
        
        if (player.meleeAttackPressed)
            stateMachine.ChangeState(stateMachine.attackState);

        if (player.rangedAttackPressed)
            stateMachine.ChangeState(stateMachine.attackState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.Move();
    }
}
