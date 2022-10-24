using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBack : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 direction;
    private float force = 10.0f;
    private float upForce = 7.0f;
    Animator anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator.GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        direction = (rb.position.x >= player.position.x) ? Vector2.right : Vector2.left;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                anim.SetTrigger("AtkEnd");
            }
        }
    }

            // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
            //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            //{
            //    
            //}

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
