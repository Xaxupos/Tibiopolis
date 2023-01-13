using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HealBarrel : Collectible
{
    public AudioSource healSound;
    public int minHealAmount = 12;
    public int maxHealAmount = 20;
    public int healAmount = 0;
    public CanvasGroup healCanvas;
    public TMPro.TMP_Text healValueText;

    private Vector2 localPos;

    public override void OnCollect()
    {
        localPos = healCanvas.transform.localPosition;
        healAmount = Random.Range(minHealAmount, maxHealAmount + 1);
        healValueText.text = $"{healAmount}";
        healCanvas.transform.DOMove(new Vector3(-1.5f, 0, 0), 0.75f);
        healCanvas.DOFade(1f, 0.75f).OnComplete(()=> { healCanvas.DOFade(0f, 0.75f).SetDelay(0.6f).OnComplete(() => healCanvas.transform.localPosition = localPos); });
        Invoke("InvokeAction", 0.4f);
    }

    public void InvokeAction()
    {
        PlayerManager.Instance.statistics.currentHealth += healAmount;
        if (PlayerManager.Instance.statistics.currentHealth > PlayerManager.Instance.statistics.maxHealth)
            PlayerManager.Instance.statistics.currentHealth = PlayerManager.Instance.statistics.maxHealth;

        healSound.Play();
        PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
        PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);
        SaveManager.Instance.SaveProfile();
    }
}
