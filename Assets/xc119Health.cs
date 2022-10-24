using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xc119Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int enemyHealth;
    [SerializeField] private GameObject xc119;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeXc119Health(int modifier)
    {

       
        enemyHealth = (enemyHealth + modifier <= maxHealth) ? enemyHealth + modifier : maxHealth;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            xc119Death();
        }
        Debug.Log(enemyHealth);
    }

    public void xc119Death()
    {
        Destroy(xc119);
    }
}
