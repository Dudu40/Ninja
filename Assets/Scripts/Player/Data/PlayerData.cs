using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Basic Stats")]
    public int maxHealth = 3;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float maxYFallTimeThreshold = 1.5f;
   

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Push State")]
    public float pushMovementVelocity = 5f;

    [Header("Attack State")]
    public float attackRadius = 0.1f;
    public float attackDamage = 1f;
    public float invincibilityTime = 2f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
    public float wallCheckDistance = 0.1f;
    public float extraHeightTest = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsDamageable;
    public LayerMask whatIsPushable;
}
