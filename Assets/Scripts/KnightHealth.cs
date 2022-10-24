using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public int maxHealth = 50;
    public int knightHealth;
    public float invincibilityFrames = 2.0f;
    public float invincibilityTimer;
    public bool invincible;
    
    private GameObject knight;
    
    // Start is called before the first frame update
    void Start()
    {
        knight = GameObject.FindWithTag("Player");
        invincibilityTimer = invincibilityFrames;
        knightHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {

            invincibilityTimer -= Time.deltaTime;
            invincible = (invincibilityTimer <= 0) ? false : true;
        }
    }

    public void ChangeKnightHealth(int modifier)
    {
        if (modifier > 0)
        {
            if (invincible) 
            {
                modifier = 0;
            }
            else
            {
                invincibilityTimer = invincibilityFrames;
                invincible = true;
            }
        }
        knightHealth = (knightHealth + modifier <= maxHealth) ? knightHealth + modifier : maxHealth;
        HealthUI.instance.UpdateMask(knightHealth / (float)maxHealth);
        
        if (knightHealth <= 0)
        {
            knightHealth = 0;
            PlayerDeath();

        }
        
        else if (!invincible)
        {
            animator.SetTrigger("Recoil");
        }
    }

    void PlayerDeath()
    {
        Destroy(knight);
    }
}
