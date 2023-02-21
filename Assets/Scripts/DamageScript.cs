using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DamageScript : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageHit;
 
    public static Action OnDamage;
    public static Action OnHeal;
    Animator anim;

    [SerializeField]
    public int maxHP = 100;

    public int _maxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
           
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool isAlive { get
        {
            return _isAlive;
        }

        set 
        {
            _isAlive = value;
            anim.SetBool(StringAnimations.isAlive, value);
            Debug.Log("IsAlive set " + value);
        } 
    }
    [SerializeField]
    public int _HP = 100;
    [SerializeField]
    private bool isBetweenHit = false;
    
    private float timeBetweenHit = 0;
    [SerializeField]
    private float hitTimer = 0.25f;

    public int HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
          
            //if health is 0, chracter dies
            if (_HP <= 0)
            {
               
                isAlive = false;
               

            }
        }
    }
    //The velocity should not change while true but follows other physics such as player controller
    public bool lockVelocity
    {
        get
        {
            return anim.GetBool(StringAnimations.lockVelocity);
        }
        set
        {
            anim.SetBool(StringAnimations.lockVelocity, value);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
     
    }
    public void FixedUpdate()
    {
        //this is meant to set up that brief moment after damage is applied to be briefly immune to damage
        if(isBetweenHit)
        {
            if(timeBetweenHit > hitTimer)
            {
                //remove the brief invincble time from when the character was hit and resets the timer
                isBetweenHit = false;
                timeBetweenHit = 0;
            }
            timeBetweenHit += Time.deltaTime;

            
        }
       
    }

    public bool Damage(int damage,Vector2 knockback)
    {
        //return whether damage was given or not
        if(isAlive && !isBetweenHit)
        {
            HP -= damage;
            OnDamage?.Invoke();
            isBetweenHit = true;

            //Notify that the damage hit and handled the knockback
            anim.SetTrigger(StringAnimations.hitTrigger);
            lockVelocity = true;
            damageHit?.Invoke(damage, knockback);//invoke the unity event for hit and knockback
            CharacterEvents.damagedCharacter?.Invoke(gameObject, damage);//invokes the character event for the gameObject's damage
            //if hit
            return true;
        }
        //unable to hit
        return false;
    }
   


    public bool Heal(int healthHeal)
    {

        if (isAlive && _HP < maxHP && CompareTag("Player"))
        {
            int healMax = Mathf.Max(maxHP - _HP, 0);//heal cap
            int realHeal = Mathf.Min(healMax, healthHeal);
            _HP += realHeal;
            OnHeal?.Invoke();
            CharacterEvents.healCharacter(gameObject, realHeal);//invoke event to heal and display text
          
            return true;
        }
        return false;

    }


}
