using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    public Vector2 knockBack = Vector2.zero;
    Collider2D attackCol;
    public int attackDamage = 10;


    private void OnTriggerEnter2D(Collider2D col)
    {
        //see if hit can hit
        DamageScript damage = col.GetComponent<DamageScript>();

        if(damage !=null)
        {
            //fliping the scale for the knockback for direction flip
            Vector2 deliverKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
                
            //hit target
            bool hit = damage.Damage(attackDamage, deliverKnockBack);
            if(hit)
            Debug.Log(col.name + " hit for" + attackDamage);
        }
        
    }

  
}
