using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundedState
{
    public LandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        player.CreateLandDust();
        AudioManager.instance.Play("PlayerLandGrass");
    }
}