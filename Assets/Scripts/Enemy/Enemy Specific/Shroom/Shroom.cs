using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : Entity
{
    public Shroom_IdleState idleState { get; private set; }
    public Shroom_MoveState moveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;


    public override void Start()
    {
        base.Start();

        idleState = new Shroom_IdleState(this, stateMachine, "move", idleStateData, this);
        moveState = new Shroom_MoveState(this, stateMachine, "idle", moveStateData, this);
        

        stateMachine.Initialize(moveState);

    }
}
