using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushState : GroundedState
{
    public PushState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(playerData.pushMovementVelocity * xInput);
        if(!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(xInput != 0 && !isTouchingPushableObject)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
