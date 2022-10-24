using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xc119Sword : MonoBehaviour
{
    public static xc119Sword sword { get; private set; }

    void Awake()
    {
        sword = this;
    }
    void Start()
    {

    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        KnightControl knight = other.gameObject.GetComponent<KnightControl>();
        if (knight != null)
        {
            KnightHealth knightHealth = other.gameObject.GetComponent<KnightHealth>();
            knightHealth.ChangeKnightHealth(-15);
        }
    }
}
