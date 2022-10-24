using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xc119Control : MonoBehaviour
{
    public bool facingRight = false;
    Rigidbody2D rb;

    public void Update()
    {
        Vector2 bossPosition = transform.position;
        Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;
        Vector3 faceOtherWay = transform.localScale;
        faceOtherWay.z = -1f;
        if (bossPosition.x > playerPosition.x && facingRight == true)
        {
            transform.localScale = faceOtherWay;
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
        else if (bossPosition.x < playerPosition.x && facingRight == false)
        {
            transform.localScale = faceOtherWay;
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        KnightControl knight = other.gameObject.GetComponent<KnightControl>();
        if (knight != null)
        {
            KnightHealth knightHealth = other.gameObject.GetComponent<KnightHealth>();
            knightHealth.ChangeKnightHealth(-5);

        }

    }
}



