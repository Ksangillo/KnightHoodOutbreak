using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrols;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public Transform enemyTransform;

    public bool isChasing = false;
    public float aggroDistance;

    public void Awake()
    {
        isChasing = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;//checks left
            }

            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;//checks right
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, playerTransform.position) <= aggroDistance)
            {
                isChasing = true;
            }
            


            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrols[0].position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, patrols[0].position) < .2f)//calculate between enemy and patrol point
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
             
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrols[1].position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, patrols[1].position) < .2f)//calculate between enemy and patrol point
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                   

                }
              

            }
        }

    }

}
