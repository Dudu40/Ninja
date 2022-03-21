using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_MoveState : EnemyMoveState
{
    private Shroom shroom;
    public Shroom_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.shroom = shroom;
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

        if (isDetectingWall || !isDetectingLedge)
        {
            shroom.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(shroom.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
