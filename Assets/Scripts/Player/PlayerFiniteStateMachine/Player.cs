using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public SpawnState SpawnState { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public InAirState InAirState { get; private set; }
    public LandState LandState { get; private set; }
    public HeavyLandState HeavyLandState { get; private set; }
    public DashState DashState { get; private set; }
    public PushState PushState { get; private set; }
    public AttackState AttackState { get; private set; }
    public DeadState DeadState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    private Collider2D coll;


    #endregion

    #region Check/Positions Transforms

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private ParticleSystem landDust; 
    [SerializeField] private ParticleSystem dashDust; 
    [SerializeField] private ParticleSystem moveDust; 

    #endregion

    #region Other Variables

    public bool isDead;
    public bool hasKey;
    public int coinCount;
    public int FacingDirection { get; private set; }  
    public int currentHealth;
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;
    private GameManager gameManager;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        SpawnState = new SpawnState(this, StateMachine, playerData, "spawn");
        IdleState = new IdleState(this, StateMachine, playerData, "idle");
        MoveState = new MoveState(this, StateMachine, playerData, "move");
        JumpState = new JumpState(this, StateMachine, playerData, "inAir");
        InAirState = new InAirState(this, StateMachine, playerData, "inAir");
        LandState = new LandState(this, StateMachine, playerData, "land");  
        HeavyLandState = new HeavyLandState(this, StateMachine, playerData, "heavyLand");  
        DashState = new DashState(this, StateMachine, playerData, "dash");
        PushState = new PushState(this, StateMachine, playerData, "push");
        AttackState = new AttackState(this, StateMachine, playerData, "attack");
        DeadState = new DeadState(this, StateMachine, playerData, "dead");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //transform.position = gameManager.lastCheckPointPos;
        FacingDirection = 1;
        currentHealth = playerData.maxHealth;
        isDead = false;
        hasKey = false;
        coinCount=0;

        StateMachine.Initialize(SpawnState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;

        StateMachine.CurrentState.LogicUpdate(); 
    }

    private void FixedUpdate()
    {
 
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        //Old overlap circle method, which didn't work correctly with buttons.
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
       
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, playerData.extraHeightTest, 1 << LayerMask.NameToLayer("Ground"));
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + playerData.extraHeightTest), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + playerData.extraHeightTest), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + playerData.extraHeightTest), Vector2.right * (coll.bounds.extents.x), rayColor);  

        return raycastHit;
    }    

    public bool CheckIfStoodOnPushableObject()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsPushable);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingPushableObject()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsPushable);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion

    #region Combat
    /*public void TriggerAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(meleeAttackPosition.position, playerData.attackRadius, playerData.whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            if(collider != null)
            {
                //Debug.Log("we hit " + collider.name);     
                //collider.GetComponentInParent<Enemy1>().TakeDamage(playerData.attackDamage);
                collider.SendMessageUpwards("TakeDamage", playerData.attackDamage);
            }
            else
            {
                return;
            }
            
        }
    }*/

    public void TriggerAttack()
    {
        AudioManager.instance.Play("PlayerSwordSwing");
        GetComponentInChildren<CircleCollider2D>().enabled = true;

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(meleeAttackPosition.position, playerData.attackRadius);
        

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider != null)
            {
                //Contact with wall / non damageable
                //play sword effect
                //Debug.Log("we hit " + collider.name); 

                /*if(damageable layer)
                {
                    call enemy take damage function
                }
                else*/
                if (collider.gameObject.CompareTag("switch"))
                {
                    //change animation to 'on'
                    collider.GetComponent<Switch>().ToggleOn();

                }
            }
            else
            {
                return;
            }
        }
    }

    public void FinishAttack()
    {
        GetComponentInChildren<CircleCollider2D>().enabled = false;
    }

    public void TakeDamage()
    {
        currentHealth--;
        
        if(currentHealth == 0)
        {
            //player dead
            isDead = true;
           this.SetVelocityZero();
          FindObjectOfType<GameManager>().Respawn();
        }
    }

    #endregion

    #region Particle Effects
    public void CreateLandDust()
    {
        landDust.Play();
    }

    public void CreateDashDust()
    {
        dashDust.Play();
    }

    public void CreateMoveDust()
    {
        moveDust.Play();
    }

    public void CreateSwordEffect()
    {

    }

    #endregion

    public void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(wallCheck.position, playerData.wallCheckDistance);
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(meleeAttackPosition.position, playerData.attackRadius);
       
    }
}
