using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStatistics : MonoBehaviour
{
    public int currentHealth = 50;
    public int maxHealth = 50;
    public int damage = 10;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
