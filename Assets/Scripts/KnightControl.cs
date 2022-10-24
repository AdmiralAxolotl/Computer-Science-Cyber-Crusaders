using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControl: MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontal;
    public float whichWayFacing;
    private bool jumpPossible;
    public float jumpForce = 12.0f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0 && horizontal != whichWayFacing)
        {
            whichWayFacing = horizontal;
            FaceOtherWay();
        }
    }

    void FixedUpdate()
    {
        
            
        
        if (jumpPossible && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpPossible = false;
        }
    }

    void FaceOtherWay()
    {
        Vector2 knight = transform.localScale;
        knight.x = Mathf.Abs(knight.x) * horizontal;    
        transform.localScale = knight;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                jumpPossible = true;
            }
        }
    }
}