using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    [SerializeField] private GameObject attackAreaPrefab;
    private GameObject attackArea;
    private Transform player;
    private float attackAreaShift;
    private float time = 0.4f;
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = time;
        player = GameObject.FindWithTag("Player").transform;
        KnightControl knightControl = GameObject.FindWithTag("Player").GetComponent<KnightControl>();
        attackAreaShift = knightControl.whichWayFacing;
        attackArea = Instantiate(attackAreaPrefab, new Vector2(player.position.x + attackAreaShift * 0.7f, player.position.y), Quaternion.identity, player);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            animator.SetTrigger("AttackEnd");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackEnd");
        Destroy(attackArea);
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
