using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void Start()
    {
        if(PlayerPrefs.GetInt($"_ATK{ProfileManager.Instance.currentProfile}") > 0)
            LoadExistingProfile();

        BoardManager.Instance.SpawnCollectibles();
    }

    public void SaveProfile()
    {
        //Inventory
        SaveGold();
        SaveHP();
        SaveATK();

        //Enemies on board
        SaveEnemiesOnBoard();
    }

    public void LoadExistingProfile()
    {
        //Inventory
        LoadGold();
        LoadHP();
        LoadATK();

        //Enemies on board
        LoadEnemiesOnBoard();
    }

    public void DeleteProfile()
    {
        DeleteAll();
    }

    public void SaveGold()
    {
        string key = $"_Gold{ProfileManager.Instance.currentProfile}";
        int value = PlayerManager.Instance.playerInventory.gold;

        PlayerPrefs.SetInt(key, value);
    }
    public void SaveHP()
    {
        string currentHPKey = $"_CurrentHP{ProfileManager.Instance.currentProfile}";
        int currentHP = PlayerManager.Instance.statistics.currentHealth;

        string maxHPKey = $"_MaxHP{ProfileManager.Instance.currentProfile}";
        int maxHp = PlayerManager.Instance.statistics.maxHealth;

        PlayerPrefs.SetInt(currentHPKey, currentHP);
        PlayerPrefs.SetInt(maxHPKey, maxHp);
    }
    public void SaveATK()
    {
        string key = $"_ATK{ProfileManager.Instance.currentProfile}";
        int value = PlayerManager.Instance.statistics.damage;

        PlayerPrefs.SetInt(key, value);
    }
    public void LoadGold()
    {
        int gold = PlayerPrefs.GetInt($"_Gold{ProfileManager.Instance.currentProfile}");
        PlayerManager.Instance.playerInventory.gold = gold;
        PlayerManager.Instance.playerInventory.goldText.text = gold.ToString();
    }
    public void LoadHP()
    {
        int currentHP = PlayerPrefs.GetInt($"_CurrentHP{ProfileManager.Instance.currentProfile}");
        PlayerManager.Instance.statistics.currentHealth = currentHP;

        int maxHP = PlayerPrefs.GetInt($"_MaxHP{ProfileManager.Instance.currentProfile}");
        PlayerManager.Instance.statistics.maxHealth = maxHP;

        PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
    }
    public void LoadATK()
    {
        int atk = PlayerPrefs.GetInt($"_ATK{ProfileManager.Instance.currentProfile}");
        PlayerManager.Instance.playerInventory.attackText.text = atk.ToString();
        PlayerManager.Instance.statistics.damage = atk;
    }
    public void SaveEnemiesOnBoard()
    {
        foreach(var entry in BoardManager.Instance.savedMonsterOnBoard)
        {
            PlayerPrefs.SetInt($"_Enemy{entry.Key}Board{ProfileManager.Instance.currentProfile}", (int)entry.Value);
        }
    }
    public void LoadEnemiesOnBoard()
    {
        foreach(var card in BoardManager.Instance.allBoardCards)
        {
            if (PlayerPrefs.HasKey($"_Enemy{card.cardIndexInBoard}Board{ProfileManager.Instance.currentProfile}"))
            {
                foreach (var enemy in SpawnManager.Instance.allEnemies)
                {
                    if(((int)enemy.monsterType) == PlayerPrefs.GetInt($"_Enemy{card.cardIndexInBoard}Board{ProfileManager.Instance.currentProfile}"))
                    {
                        Debug.Log("Spawning loaded enemy");
                        BoardManager.Instance.SpawnLoadedEnemy(enemy, card);
                        break;
                    }
                }
            }
        }
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteKey($"_Gold{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_CurrentHP{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_MaxHP{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_ATK{ProfileManager.Instance.currentProfile}");
    }
}
