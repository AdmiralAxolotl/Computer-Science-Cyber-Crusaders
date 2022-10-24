using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xc119LongNeutral : StateMachineBehaviour
{

    //System.Random rnd = new System.Random();
    private float minTime = 1f;
    private float maxTime = 3f;
    private float backForthMinTime = 1f;
    private float backForthMaxTime = 4f;
    private float backForthTimer;
    private float timer;
    private float speed = 3.0f;
    private float whichDirection;
    Rigidbody2D rb;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        rb = animator.GetComponent<Rigidbody2D>();
        timer = Random.Range(minTime, maxTime); ; //rnd.Next(minTime, maxTime);
        whichDirection = (Random.Range(-1, 1) > 0) ? 1.0f : -1.0f;
        speed = speed * whichDirection;
        backForthTimer = 0.0f;
        player = GameObject.FindWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        float playerProximity = player.position.x - rb.position.x;
        timer -= Time.deltaTime;
        if (backForthTimer <= 0.0f)
        {
            speed = -speed;
            backForthTimer = Random.Range(backForthMinTime, backForthMaxTime);
        }
        backForthTimer -= Time.deltaTime;
        Vector2 bossPosition = rb.transform.position;
        bossPosition.x = bossPosition.x + speed * Time.deltaTime;
        rb.transform.position = bossPosition;


        if (playerProximity >= -6.0f && playerProximity <= 6.0f)
        {
            //possibly instant attack initiation
            animator.SetTrigger("InRange");
        }
        else if (timer <= 0) 
        {
            animator.SetTrigger("LongAtk");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("InRange");
        animator.ResetTrigger("LongAtk");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
