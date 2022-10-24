using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRoll : StateMachineBehaviour
{
    private float timer = 1.0f;
    Rigidbody2D rb;
    private float rollSpeed = 12.0f;
    private float directionOfRoll;
    private float horizontalResizer;
    private float verticalResizer;
    private BoxCollider2D playerCollider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
        KnightControl knightControl = GameObject.FindWithTag("Player").GetComponent<KnightControl>();
        rb = animator.GetComponent<Rigidbody2D>();
        GameObject.FindWithTag("Player").layer = LayerMask.NameToLayer("PlayerRolling");
        directionOfRoll = knightControl.whichWayFacing;
        horizontalResizer = 0.2f * playerCollider.size.x;
        verticalResizer = 0.15f * playerCollider.size.y;
        playerCollider.size = new Vector2(horizontalResizer, verticalResizer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            animator.SetTrigger("RollEnd");
        }
        Vector2 position = rb.position;
        position.x = position.x + rollSpeed * directionOfRoll * Time.deltaTime;
        rb.position = position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindWithTag("Player").layer = LayerMask.NameToLayer("Player");
        playerCollider.size = new Vector2(1 / horizontalResizer, 1 / verticalResizer);
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
