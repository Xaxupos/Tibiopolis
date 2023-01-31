using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zapytania : Collectible
{
    public AudioSource goldSound;
    public AudioSource healSound;
    public AudioSource nicSound;
    public AudioSource attackSound;

    private void InvokeClose()
    {
        UIManager.Instance.zapytaniaCanvas.gameObject.SetActive(false);
    }

    public override void OnCollect()
    {
        UIManager.Instance.zapytaniaCanvas.gameObject.SetActive(true);

        Zapytanie zapytanie = new Zapytanie();

        zapytanie = BattleManager.Instance.zapytanias[Random.Range(0, BattleManager.Instance.zapytanias.Length)];

        UIManager.Instance.zapytaniaText.text = zapytanie.nagroda;
        UIManager.Instance.zapytaniaImage.sprite = zapytanie.sprite;

        if (zapytanie.nagroda == "+10 TC")
        {
            goldSound.Play();

            PlayerManager.Instance.playerInventory.gold += 10;
            PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";
        }
        else if (zapytanie.nagroda == "-10 TC")
        {
            goldSound.Play();

            PlayerManager.Instance.playerInventory.gold -= 10;
            PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";
        }
        else if (zapytanie.nagroda == "+5 HP")
        {
            PlayerManager.Instance.statistics.maxHealth += 5;
            PlayerManager.Instance.statistics.currentHealth += 5;
            if (PlayerManager.Instance.statistics.currentHealth > PlayerManager.Instance.statistics.maxHealth)
                PlayerManager.Instance.statistics.currentHealth = PlayerManager.Instance.statistics.maxHealth;

            healSound.Play();
            PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
            PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);
        }
        else if(zapytanie.nagroda == "+3 ATK")
        {
            attackSound.Play();
            PlayerManager.Instance.statistics.damage += 3;
            PlayerManager.Instance.playerInventory.attackText.text = PlayerManager.Instance.statistics.damage.ToString();
        }
        else if(zapytanie.nagroda == "-3 ATK")
        {
            attackSound.Play();
            PlayerManager.Instance.statistics.damage -= 3;
            PlayerManager.Instance.playerInventory.attackText.text = PlayerManager.Instance.statistics.damage.ToString();
        }
        else if(zapytanie.nagroda == "NIC")
        {
            nicSound.Play();
        }

        Invoke(nameof(InvokeClose), 2.5f);
        SaveManager.Instance.SaveProfile();
    }
}

[System.Serializable]
public class Zapytanie
{
    public string nagroda;
    public Sprite sprite;
}

