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

        //Player on board
        SavePlayerOnBoard();
    }

    public void LoadExistingProfile()
    {
        //Player on board
        LoadPlayerOnBoard();

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
    public void SaveEnemiesOnBoard()
    {
        foreach(var entry in BoardManager.Instance.savedMonsterOnBoard)
        {
            PlayerPrefs.SetInt($"_Enemy{entry.Key}Board{ProfileManager.Instance.currentProfile}", (int)entry.Value);
        }
    }
    public void SavePlayerOnBoard()
    {
        PlayerPrefs.SetInt($"_PlayerBoardPos{ProfileManager.Instance.currentProfile}", PlayerManager.Instance.playerMovement.currentCardIndex);
        PlayerPrefs.SetInt($"_PlayerBoardRotation{ProfileManager.Instance.currentProfile}", PlayerManager.Instance.playerMovement.currentLineIndex);
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
        PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);
    }
    public void LoadATK()
    {
        int atk = PlayerPrefs.GetInt($"_ATK{ProfileManager.Instance.currentProfile}");
        PlayerManager.Instance.playerInventory.attackText.text = atk.ToString();
        PlayerManager.Instance.statistics.damage = atk;
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
    public void LoadPlayerOnBoard()
    {
        var cardIndex = PlayerPrefs.GetInt($"_PlayerBoardPos{ProfileManager.Instance.currentProfile}");
        var player = Instantiate(SpawnManager.Instance.playerPrefab, BoardManager.Instance.transform);
        player.transform.localPosition = BoardManager.Instance.allBoardCards[cardIndex].cardMovePosition;

        int lineIndex = (BoardManager.Instance.allBoardCards[cardIndex].cardIndexInBoard - 1) / 10;
        PlayerManager.Instance.playerMovement.currentLineIndex = lineIndex;
        PlayerManager.Instance.playerMovement.currentCardIndex = cardIndex;

        PlayerManager.Instance.transform.localEulerAngles = new Vector3(
            PlayerManager.Instance.transform.localEulerAngles.x,
            PlayerManager.Instance.transform.localEulerAngles.y,
            PlayerManager.Instance.transform.localEulerAngles.z + (-90 * PlayerPrefs.GetInt($"_PlayerBoardRotation{ProfileManager.Instance.currentProfile}", PlayerManager.Instance.playerMovement.currentLineIndex))
            ); ;

        BoardManager.Instance.transform.localEulerAngles = new Vector3(
            BoardManager.Instance.transform.localEulerAngles.x,
            BoardManager.Instance.transform.localEulerAngles.y,
            BoardManager.Instance.transform.localEulerAngles.z + (90 * PlayerPrefs.GetInt($"_PlayerBoardRotation{ProfileManager.Instance.currentProfile}", PlayerManager.Instance.playerMovement.currentLineIndex))
            ); ;
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteKey($"_Gold{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_CurrentHP{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_MaxHP{ProfileManager.Instance.currentProfile}");
        PlayerPrefs.DeleteKey($"_ATK{ProfileManager.Instance.currentProfile}");
    }
}
