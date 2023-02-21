using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotBehaviour : StateMachineBehaviour
{

    public AudioClip sounds;
    public float volume = 1f;
    public bool playEnter = true;
    public bool playExit = false;
    public bool playDelay = false;

    public float delayTimer = 0.24f;
    private float timewhenEntered = 0f;
    private bool hasDelayed = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playEnter)
        {
            AudioSource.PlayClipAtPoint(sounds, anim.gameObject.transform.position, volume);


        }

        timewhenEntered = 0f;
        hasDelayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if (playDelay && !hasDelayed)
        {
            timewhenEntered += Time.deltaTime;

            if (timewhenEntered > delayTimer)
            {
                AudioSource.PlayClipAtPoint(sounds, anim.gameObject.transform.position, volume);
                hasDelayed = true;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playExit)
        {
            AudioSource.PlayClipAtPoint(sounds, anim.gameObject.transform.position, volume);


        }
    }

  
}
