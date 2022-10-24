using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        xc119Health health = GameObject.FindWithTag("Enemy").GetComponent<xc119Health>();
        health.ChangeXc119Health(-1);

    }


}
