using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private GameObject swordPrefab;
    private GameObject swordCollider;
    private Transform parent;
    private Transform player;

    private const float windupTime = 0.5f;
    private const float dashTime = 0.5f;
    private const float dashForce = 10.0f;
    private const float dragWindow = 0.2f;
    private const float dashDrag = 0.04f;
    private float timer;

    private Vector2 dashDirection;
    private float shiftSword;

    private bool dashed;
    private int damageModifier;




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        anim = animator.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        parent = GameObject.FindWithTag("Enemy").transform;
        dashed = false;

        timer = windupTime + dashTime;

        //find direction of dash
        dashDirection = (rb.position.x >= player.position.x) ? Vector2.left : Vector2.right;
        shiftSword = ((rb.position.x >= player.position.x) ? -1f : 1f) * 1.5f;

        //set XC-119 to 'windup' animation
        anim.SetFloat("BasicDash", 0);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        //activate dash
        if (timer <= dashTime && !dashed)
        {
            swordCollider = Instantiate(swordPrefab, new Vector2(parent.position.x + shiftSword, parent.position.y - 0.5f), Quaternion.identity, parent);

            //XC-119 and sword dash
            anim.SetFloat("BasicDash", 1);
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
            //rbXc119WeaponCollider.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
            dashed = true;


        }
        if (timer < dashTime - dragWindow)
        {
            rb.velocity -= dashDrag * rb.velocity;
            //rbXc119WeaponCollider.velocity -= dashDrag * rbXc119WeaponCollider.velocity;

        }

        if (timer < 0)
        {
            animator.SetTrigger("AtkEnd");
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(swordCollider);
        animator.ResetTrigger("AtkEnd");
        //Destroy(xc119WeaponCollider);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("hit!");
        KnightControl knight = other.gameObject.GetComponent<KnightControl>();
        if (knight != null)
        {
            KnightHealth knightHealth = other.gameObject.GetComponent<KnightHealth>();
            damageModifier = -5;
            knightHealth.ChangeKnightHealth(damageModifier);
        }
    }
}
