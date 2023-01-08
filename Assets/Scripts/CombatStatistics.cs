using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CombatStatistics : MonoBehaviour
{
    public int maxHealth = 50;
    public int damage = 10;
    public TMP_Text hpText;

    public int currentHealth = 50;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Attack(CombatStatistics target, TMP_Text rolledDmgText, bool playerTurn)
    {
        int rolledDamage = Random.Range(1, damage + 1);

        if (playerTurn)
            rolledDmgText.text = "Player dealing " + rolledDamage.ToString() + " damage";
        else
            rolledDmgText.text = "Enemy dealing " + rolledDamage.ToString() + " damage";

        rolledDmgText.DOFade(1f, 0.75f)
            .SetDelay(0.25f)
            .OnComplete(() => 
            target.TakeDamage(rolledDamage));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpText.DOFade(0f, 0.25f).OnComplete(() => hpText.DOFade(1f, 0.25f).OnComplete(() => hpText.text = $"HP {currentHealth}"));
        
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
