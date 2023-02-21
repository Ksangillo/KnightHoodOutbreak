using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    Rigidbody2D rb;
    CollisionScript touchDirections;
    public Detect attackZone;
    DamageScript damage;
    Animator anim;

 
    public bool _isTargeted = false;
    public bool isTargeted
    {
        get
        {
            return _isTargeted;
        }
        private set 
        {
            _isTargeted = value;
            anim.SetBool(StringAnimations.isTarget, value);//switch between attacks and moving
        }
    }
   
    
    public float attackCoolDown { get
    {
            return anim.GetFloat(StringAnimations.attackCoolDown);

    } 
        private set
        {
            //take ethier the max or the 0 to improve the visual
            anim.SetFloat(StringAnimations.attackCoolDown, MathF.Max(value, 0));

        }
              
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchDirections = GetComponent<CollisionScript>();
        anim = GetComponent<Animator>();
        damage = GetComponent<DamageScript>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        isTargeted = attackZone.detectColliders.Count > 0;

        if (attackCoolDown > 0)
        {
            //get the old value and subtracts from delta time to set new value for attack cooldown
            attackCoolDown -= Time.deltaTime;
        }
        

    }
    
    public void OnHit(int damage, Vector2 knockBack)
    {
       
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

  
          
            
        
    
}
