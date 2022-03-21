using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
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

        player.CreateMoveDust();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(playerData.movementVelocity * xInput);

        if (xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
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

        AudioManager.instance.Play("PlayerFootstepsGrass");
    }
}
