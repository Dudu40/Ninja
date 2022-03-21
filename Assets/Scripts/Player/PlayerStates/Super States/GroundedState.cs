using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    protected int xInput;

    protected bool isGrounded;
    protected bool isTouchingPushableObject;
    protected bool isStoodOnPushableObject;
    protected bool jumpInput;
    protected bool dashInput;
    private bool attackInput;
    private bool isDead;
    public GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingPushableObject = player.CheckIfTouchingPushableObject();
        isStoodOnPushableObject = player.CheckIfStoodOnPushableObject();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        isDead = player.isDead;

        if (isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded && !isStoodOnPushableObject)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingPushableObject)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if(xInput != 0 && isTouchingPushableObject && !dashInput)
        {
            stateMachine.ChangeState(player.PushState);
        }
        else if (attackInput)
        {
            stateMachine.ChangeState(player.AttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
