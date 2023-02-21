using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
   //misc
    CapsuleCollider2D touch;
    Animator anim;
    public ContactFilter2D cast;
   
    //Stored Raycast Arrays for each check
    RaycastHit2D[] groundCheck = new RaycastHit2D[5];
    RaycastHit2D[] wallCheckCollision = new RaycastHit2D[5];
    RaycastHit2D[] ceilingCheckCollision = new RaycastHit2D[5];
    //Floats
    public float groundDist = 0.05f;
    public float wallCheckDistances = 0.02f;
    public float ceilingCheckDistances = 0.05f;
    //bools & getter/setters each one determines which  animation state our character is in
    [SerializeField]
    private bool _isGroundCheck;
    public bool isGroundCheck { get
        {
            return _isGroundCheck;
        }
        private set 
        {
            _isGroundCheck = value;
            anim.SetBool(StringAnimations.isGroundCheck, value);
        } 
    }
    [SerializeField]
    private bool _isWallCheck;
    public bool isWallCheck
    {
        get
        {
            return _isWallCheck;
        }
        private set
        {
            _isWallCheck = value;
            anim.SetBool(StringAnimations.isWallCheck, value);
        }
    }

    [SerializeField]
    private bool _isCeilingCheck;
    //checking the if statement is true or false
    private Vector2 wallCheckDirect => gameObject.transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
    
    public bool isCeilingCheck
    {
        get
        {
            return _isCeilingCheck;
        }
        private set
        {
            _isCeilingCheck = value;
            anim.SetBool(StringAnimations.isCeilingCheck, value);
        }
    }


    private void Awake()
    {
        touch = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //determine if the player is able to jump
        isGroundCheck = touch.Cast(Vector2.down, cast, groundCheck, groundDist) > 0;
        isWallCheck = touch.Cast(wallCheckDirect, cast, wallCheckCollision, wallCheckDistances) > 0;
        isCeilingCheck = touch.Cast(Vector2.up, cast, ceilingCheckCollision, ceilingCheckDistances) > 0;
       
    }
}
