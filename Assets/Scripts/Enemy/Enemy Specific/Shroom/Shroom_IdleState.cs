using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_IdleState : EnemyIdleState
{
    private Shroom shroom;
    public Shroom_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(shroom.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
