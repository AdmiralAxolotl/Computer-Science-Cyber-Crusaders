using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xc119CloseNeutral : StateMachineBehaviour
{
    Rigidbody2D rb;
    private float closeTime;
    private float timer;
    private float[] pauseTime = { 0.0f, 0.5f, 0.5f, 0.1f, 1.0f, 2.0f, };
    private float speed = 1.0f;
    private float directionOfMovement;
    private float movingTimer;
    private bool backingUp;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        timer = pauseTime[Random.Range(0, pauseTime.Length - 1)];
        closeTime = timer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        closeTime -= Time.deltaTime;

        if (movingTimer < 0)
        {
            movingTimer = Random.Range(1.0f, 4.0f);
            backingUp = (Random.Range(0, 1.0f) > 0.4f) ? true : false;
            
        }
        movingTimer -= Time.deltaTime;

        Vector2 bossPosition = rb.transform.position;
        Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;
        directionOfMovement = (backingUp) ? 0.0f : (bossPosition.x > playerPosition.x) ? 1.0f : -1.0f;
        bossPosition.x = bossPosition.x + speed * directionOfMovement * Time.deltaTime;
        rb.transform.position = bossPosition;
        
        if (closeTime < 0.0f)
        {
            animator.SetTrigger("CloseAtk");
        }
        if (bossPosition.x - playerPosition.x > 6.0f || bossPosition.x - playerPosition.x < -6.0f)
        {
            animator.SetTrigger("OutRange");
        }
    

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("CloseAtk");
        animator.ResetTrigger("OutRange");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
