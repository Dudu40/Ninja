using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    //public AttackDetails attackDetails;
    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    //public AnimationToStateMachine atsm { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;
    //[SerializeField] private Transform touchDamageCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    private float lastTouchDamageTime;
    private float touchDamageCooldown;


    private Vector2 velocityWorkspace;
    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
       // atsm = aliveGO.GetComponent<AnimationToStateMachine>();


        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", rb.velocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        // Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);

        bool val = false;
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val = true;
            }
        }
        else
        {
            val = false;
        }

        return val;
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        //return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);

        bool val = false;
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val = true;
            }
        }
        else
        {
            val = false;
        }

        return val;
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        //return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);

        bool val = false;
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val = true;
            }
        }
        else
        {
            val = false;
        }

        return val;
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    /*public virtual void TakeDamage(float playerAttackDamage)
    {
        lastDamageTime = Time.time;


        currentHealth -= playerAttackDamage;
        //currentStunResistance -= attackDetails.stunDamageAmount;
        //Debug.Log(currentHealth);

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }*/

    /*public void CheckTouchDamage()
    {
        if (Time.time >= lastDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, entityData.whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails.damageAmount = entityData.touchDamage;
                attackDetails.position.x = aliveGO.transform.position.x;
                hit.GetComponent<Player>().TakeDamage(attackDetails);
            }
        }
    }*/

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0, 180, 0);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.2f);

        /*Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y - (entityData.touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (entityData.touchDamageWidth / 2), touchDamageCheck.position.y + (entityData.touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);*/
    }
}
