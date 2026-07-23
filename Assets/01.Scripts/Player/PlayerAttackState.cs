using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 실행
        if (player.meleeAttackPressed)
        {
            player.PlayMeleeAttackAnim();
        }
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 끄기
    }

    public override void Update()
    {
        base.Update();

        if (player.dashPressed)
            stateMachine.ChangeState(stateMachine.dashState);

        if (player.jumpPressed)
            stateMachine.ChangeState(stateMachine.jumpState);

        if (player.dir != 0)
            stateMachine.ChangeState(stateMachine.moveState);

        if (player.meleeAttackPressed)
            player.MeleeAttack();
        else if (player.rangedAttackPressed)
            player.RangedAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
