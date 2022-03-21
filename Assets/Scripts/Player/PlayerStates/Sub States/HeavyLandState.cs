using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyLandState : GroundedState
{
    public HeavyLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.CreateLandDust();
        player.SetVelocityZero();
        CinemachineShake.Instance.ShakeCamera(10f, 0.5f);
        AudioManager.instance.Play("PlayerHeavyLand");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        

        if (!isExitingState)
        {
            if(isAnimationFinished)
            {
                if (xInput != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
                else if (xInput ==0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            } 
        }
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