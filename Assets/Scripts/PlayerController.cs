using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    //misc
    Vector2 move;
    Rigidbody2D rb;
    Animator anim;
    CollisionScript touchDirections;
    public GameObject player;
    public GameObject eventSystem;
    public PlayerInput playerControls;

    public Pausemenu pause;
    [SerializeField]private GameObject GameOverUI;

    //floats
    public float topSpeed = 5f;//speed of Player
    public float walkAccel = 5f;
    public float jumpSpeed = 10f;
    public float jumpFallTime = 2.5f;
    public float airDrag = 2f;
 

    public bool isDash = true;
    public bool isDashing;
    public float dashSpeed = 24f;
    public float dashTime = 0.5f;
    public float dashCoolDown = 1f;
    public float ogGravity;
    public float gravityDash;
    public float timeWait;

    public TrailRenderer trailRend;

    DamageScript damage;
    

    //bools
    public bool isAlive { get { return anim.GetBool(StringAnimations.isAlive); } }
    [SerializeField]
    public bool _isRight = true;
    public bool isRight { get { return _isRight; } private set { if (_isRight != value) {
                //Flip the local scale of the player to flip directions
                transform.localScale *= new Vector2(-1, 1); }
            _isRight = value;
        }
    }

    //locksMovement
    public bool canMove { get { return anim.GetBool(StringAnimations.canMove); } }
    public float currentWalk { get
        {
            if (canMove)
            {
                if (isMoving && !touchDirections.isWallCheck &&!touchDirections.isCeilingCheck)
                {
                    rb.gravityScale = 3f;
                    if (touchDirections.isGroundCheck)
                    {
                       
                        if (isMoving)
                        {
                            return topSpeed;
                            
                        }
                        else
                        {
                            //idle Speed is 0
                            return 0;
                        }
                    }
                    else
                    {
                        //idle Speed is 0
                        return 0;
                        
                    }
                }
                //locked movement
                else
                {
                    return 0;
                    
                }
            }
            else
            {
                return 0;
            }

        }
    }

    [SerializeField]
    private bool isMove = false;
    public bool isMoving { get { return isMove; } private set
        {
            //StringAnimations calls the string value from String Animation script
            isMove = value;
            anim.SetBool(StringAnimations.isMove, value);
        }

    }


    public bool _isAttack = false;
    public int jumpCount = 1;
    public int maxJumps = 2;
    public bool isJumping = true;
    

    public bool isAttack { get
        { return _isAttack;
        }
        private set
        { _isAttack = value; }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchDirections = GetComponent<CollisionScript>();
        damage = GetComponent<DamageScript>();
        trailRend = GetComponent<TrailRenderer>();
        ogGravity = rb.gravityScale;
        isDash = true;
        player.GetComponent<PlayerInput>();
        GameOverUI.SetActive(false);
      

    }

     void FixedUpdate()
    {
        if(!isAlive)
        {
            eventSystem.SetActive(false);
            playerControls.enabled = false;
            GameOverUI.SetActive(true);
            pause.enabled = false;

        }
        timeWait += Time.deltaTime;
        if(isDashing)
        {
            return;
        }
      
        if (!damage.lockVelocity)
            //determines players movement
      
        ApplyAirDrag();
        
        rb.AddForce(new Vector2(move.x, 0f) * walkAccel);
        if (Mathf.Abs(rb.velocity.x) > topSpeed)
        rb.velocity = new Vector2(Mathf.Sign(move.x) * currentWalk, rb.velocity.y);
        anim.SetFloat(StringAnimations.yVelocity, rb.velocity.y);
        FallTime();
       
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        if (isAlive)
        {
            isMoving = move != Vector2.zero;
            FlipPlayer(move);
        }
        else if (!isAlive)
        {
            isMoving = false;
        }
    }

   
    private void ApplyAirDrag()
    {
            rb.drag = airDrag;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchDirections.isGroundCheck || touchDirections.isWallCheck && canMove && jumpCount <= maxJumps && !touchDirections.isCeilingCheck)
        {

            if (jumpCount == 0 || !touchDirections.isWallCheck)
            {
                StartCoroutine(ResetJumpCount());
            }
            anim.SetTrigger(StringAnimations.jump);
            ApplyAirDrag();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount++;
            
            
        }
        else if (context.started  && canMove && jumpCount <= maxJumps  || touchDirections.isCeilingCheck && !touchDirections.isWallCheck)
        {
           
            if (jumpCount == 0)
            {
                StartCoroutine(ResetJumpCount());
            }
            
            anim.SetFloat(StringAnimations.yVelocity, rb.velocity.y);
            ApplyAirDrag();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount++;

        }
        
    }
    
   private IEnumerator ResetJumpCount()
    {

        yield return new WaitUntil(() => !isGrounded());//waiting until character is in the air
        yield return new WaitUntil(isGrounded);//waiting until character is on the ground;
        jumpCount = 0;

    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if(context.performed && isDash)
        {
            
            if (timeWait >= dashCoolDown)
            {
                anim.SetTrigger(StringAnimations.isDash);
                timeWait = 0;
                Invoke("Dash", 0);
            }
        }
    }
    public void Dash()
    {
        isDash = false;
        isDashing = true;
        trailRend.emitting = true;
        rb.gravityScale = gravityDash;

        if(move.x == 0)
        {
            if(isRight)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
            }
            if(!isRight)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(move.x * dashSpeed, 0);
        }

        Invoke("DashStop", dashTime);
    }

    public void DashStop()
    {
        isDash = true;
        isDashing = false;
        trailRend.emitting = false;
        rb.gravityScale = ogGravity;
    }
    private bool isGrounded() => touchDirections.isGroundCheck && !touchDirections.isCeilingCheck ||touchDirections.isWallCheck;
    private void FallTime()
    {
        if ( !touchDirections.isGroundCheck || touchDirections.isCeilingCheck || touchDirections.isWallCheck )
        {
            rb.gravityScale = jumpFallTime;
        }
        else if ( touchDirections.isGroundCheck || !touchDirections.isCeilingCheck || !touchDirections.isWallCheck)
        {
            rb.gravityScale = 1f;
        }
        
    }
    public void OnAttack(InputAction.CallbackContext context)
    { 
        if (context.started)
        {
            isAttack = true;
            anim.SetTrigger(StringAnimations.attack);
         
        }
    }
    
        public void FlipPlayer(Vector2 move)
        {
           if(move.x > 0 && !isRight)
            {
                //face right
                isRight = true;
            }
           else if(move.x < 0 && isRight)
            {
                //face left
                isRight = false;
            }
        }
    
   
  
    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

}
