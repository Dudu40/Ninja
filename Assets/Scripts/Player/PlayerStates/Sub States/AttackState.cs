using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbilityState
{
    protected Transform attackPosition;
   
    private bool isAttackPressed;
    private bool meleeAttackInputStop;
    
    private float lastAttackTime;

    private Vector2 attackDirection;
    public AttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        player.SetVelocityZero();
        player.InputHandler.UseMeleeAttackInput();
        //attackDirection = Vector2.right * player.FacingDirection;
        isAttackPressed = true;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (isAttackPressed)
            {
                meleeAttackInputStop = player.InputHandler.AttackInputStop;

                if (meleeAttackInputStop)
                {
                    startTime = Time.time;
                    isAttackPressed = false;
                    
                }
            }
            else
            {
                isAbilityDone = true;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
