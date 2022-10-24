using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWalk : StateMachineBehaviour
{
    Rigidbody2D rb;
    public float speed = 6.0f;
    public float horizontal;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("Walk", horizontal);
        if (Input.GetButtonDown("Roll")) {
            animator.SetTrigger("Roll");

        }
        else if (Input.GetButtonDown("JumpRoll"))
        {
            animator.SetTrigger("JumpRoll");
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
        
        
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 position = rb.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        rb.position = position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Roll");
        animator.ResetTrigger("JumpRoll");
        animator.ResetTrigger("Attack");
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