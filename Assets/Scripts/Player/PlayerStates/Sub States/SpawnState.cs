using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : PlayerState
{
    protected int xInput;
    public SpawnState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (xInput != 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (xInput == 0 && !isExitingState)
        {
                stateMachine.ChangeState(player.IdleState);
        }
    }
}
