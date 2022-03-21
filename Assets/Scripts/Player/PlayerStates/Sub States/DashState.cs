using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AbilityState
{
    public bool CanDash { get; private set; }

    private bool isDashPressed;
    private bool dashInputStop;

    private float lastDashTime;
    private float zeroDrag = 0f;

    private Vector2 dashDirection;
    private Vector2 lastAfterImagePosition;
    public DashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();
        dashDirection = Vector2.right * player.FacingDirection;
        isDashPressed = true;
        player.CreateDashDust();
        AudioManager.instance.Play("PlayerDash");
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
            if (isDashPressed)
            {
                dashInputStop = player.InputHandler.DashInputStop;

                if (dashInputStop)
                {
                    startTime = Time.time;
                    isDashPressed = false;
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                    PlaceAfterImage();
                }  
            }
            else
            {
                player.SetVelocity(playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = zeroDrag;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePosition = player.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePosition) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
