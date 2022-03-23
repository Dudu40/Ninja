using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;
    private bool dashInputStop;
    private bool attackInput;

    //Checks
    private bool isGrounded;
    private bool isStoodOnPushableObject;
    private bool isJumping;
    private bool isDead;
 
    private bool coyoteTime;
    
    float fallTime = 0f;

    public InAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isStoodOnPushableObject = player.CheckIfStoodOnPushableObject();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        fallTime = 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
 
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        isDead = player.isDead;
        CheckDoubleJump();
        CheckJumpMultiplier();
        UpdateFallTime();
        
        if(isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        else if((!isGrounded || !isStoodOnPushableObject) && attackInput)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if ((isGrounded || isStoodOnPushableObject)  && fallTime < playerData.maxYFallTimeThreshold ) 
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if ((isGrounded || isStoodOnPushableObject) && fallTime >= playerData.maxYFallTimeThreshold )
        {
            stateMachine.ChangeState(player.HeavyLandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            coyoteTime = false;
            stateMachine.ChangeState(player.JumpState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            //Uncomment this if you have different animations for standing jump and moving jump. Remember to set xVelocity in animator, and update blend tree.
            //player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckDoubleJump(){
     if(player.CheckIfTouchingWall() && isJumping){
       player.JumpState.ResetAmountOfJumpsLeft();
     }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    public void UpdateFallTime()
    {
        if (player.CurrentVelocity.y < 0)
        {
            fallTime += Time.deltaTime;
        }
    }
   
}
