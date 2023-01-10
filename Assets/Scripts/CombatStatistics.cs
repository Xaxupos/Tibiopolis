using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CombatStatistics : MonoBehaviour
{
    public Healthbar healthbar;
    public TMP_Text statsText;

    public int currentHealth = 50;
    public int maxHealth = 50;
    public int damage = 10;

    public bool isEnemy = false;
    public int minDamage = 1;
    public int maxDamage = 10;
    public int minHealthRandom = 1;
    public int maxHealthRandom = 10;

    private void Awake()
    {
        if(isEnemy)
        {
            damage = Random.Range(minDamage, maxDamage + 1);
            maxHealth = Random.Range(minHealthRandom, maxHealthRandom + 1);
        }
        currentHealth = maxHealth;
    }

    public void Attack(CombatStatistics target, TMP_Text rolledDmgText, bool playerTurn)
    {
        int rolledDamage = Random.Range(1, damage + 1);

        if (playerTurn)
            rolledDmgText.text = "Gracz zadaje " + rolledDamage.ToString() + " obra¿eñ!";
        else
            rolledDmgText.text = "Wróg zadaje " + rolledDamage.ToString() + " obra¿eñ!";

        rolledDmgText.DOFade(1f, 0.75f)
            .SetDelay(0.25f)
            .OnComplete(() => 
            target.TakeDamage(rolledDamage));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.UpdateHealthbar(currentHealth, maxHealth);

        if(isEnemy)
        {
            statsText.text = $"ATK: {this.damage} <br> HP: {currentHealth}";
        }

        if (TryGetComponent(out PlayerInventory inventory))
        {
            inventory.healthText.DOFade(0f, 0.25f).OnComplete(() => { inventory.healthText.text = $"{currentHealth}/{maxHealth}"; inventory.healthText.DOFade(1f, 0.25f); });
        }
        
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        if (TryGetComponent(out PlayerInventory i))
        {
            UIManager.Instance.gameOverCanvas.SetActive(true);
        }

        Destroy(gameObject);
    }
}
