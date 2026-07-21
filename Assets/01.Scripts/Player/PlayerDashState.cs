using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        Debug.Log("DashState : " + PlayerStats.instance.isUnlockDash);

        if (PlayerStats.instance.isUnlockDash)
            player.PlayDashAnim(true);
        
        player.Dash();
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
        player.PlayDashAnim(false);
    }

    public override void Update()
    {
        base.Update();

        if (!player.isDashing)
        {
            if (player.dir == 0)
                stateMachine.ChangeState(stateMachine.idleState);
            else
                stateMachine.ChangeState(stateMachine.moveState);
        }

        if (player.jumpPressed)
            stateMachine.ChangeState(stateMachine.jumpState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
