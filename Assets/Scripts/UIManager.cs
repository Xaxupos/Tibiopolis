using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject sklepCanvas;
    public GameObject imbuCanvas;
    public GameObject zapytaniaCanvas;
    public TMP_Text zapytaniaText;
    public Image zapytaniaImage;
    public Reward reward1;
    public Reward reward2;
    public Reward reward3;
    public GameObject menuButton;
    public GameObject gameOverCanvas;
    public Button rollButton;
    public SpriteRenderer swordsSprite;
    public TMP_Text rolledDmgText;
    public TMP_Text goldLootText;
    public TMP_Text itemLootText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void StartBattle()
    {
        swordsSprite.DOFade(1f, 1f);
        rollButton.gameObject.SetActive(false);
    }

    public void EndBattle()
    {
        swordsSprite.DOFade(0f, 1f);
        rolledDmgText.DOFade(0, 0.5f);
    }

    public void BuyAttack()
    {
        if (PlayerManager.Instance.playerInventory.gold < 7) return;

        PlayerManager.Instance.playerInventory.gold -= 7;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";

        BattleManager.Instance.attackSound.Play();
        PlayerManager.Instance.statistics.damage += 3;
        PlayerManager.Instance.playerInventory.attackText.text = PlayerManager.Instance.statistics.damage.ToString();

        SaveManager.Instance.SaveProfile();
    }

    public void BuyMaxHP()
    {
        if (PlayerManager.Instance.playerInventory.gold < 7) return;

        PlayerManager.Instance.playerInventory.gold -= 7;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";

        PlayerManager.Instance.statistics.currentHealth += 8;
        PlayerManager.Instance.statistics.maxHealth += 8;
        if (PlayerManager.Instance.statistics.currentHealth > PlayerManager.Instance.statistics.maxHealth)
            PlayerManager.Instance.statistics.currentHealth = PlayerManager.Instance.statistics.maxHealth;

        BattleManager.Instance.healSound.Play();

        PlayerManager.Instance.statistics.damage += 1;
        PlayerManager.Instance.playerInventory.attackText.text = PlayerManager.Instance.statistics.damage.ToString();

        PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
        PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);

        SaveManager.Instance.SaveProfile();
    }

    public void BuyHealToFull()
    {
        if (PlayerManager.Instance.playerInventory.gold < 9) return;

        PlayerManager.Instance.playerInventory.gold -= 9;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";

        BattleManager.Instance.healSound.Play();
        PlayerManager.Instance.statistics.currentHealth = PlayerManager.Instance.statistics.maxHealth;

        PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
        PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);

        SaveManager.Instance.SaveProfile();
    }
    public void Leave()
    {
        sklepCanvas.SetActive(false);
        rollButton.gameObject.SetActive(true);
        SaveManager.Instance.SaveProfile();
    }
}
