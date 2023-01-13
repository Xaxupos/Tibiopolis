using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public ItemType itemType;
    public string itemDesc;
    public StatBoostType statBoostType = StatBoostType.HEALTH;
    public Sprite itemSprite;
    public string itemName = "";
    public float dropChance = 0.5f;
    public int itemMinStatistics = 0;
    public int itemMaxStatistics = 0;

    public int randomStatistic = 0;

    public void SetItemStatistics()
    {
        randomStatistic = Random.Range(itemMinStatistics, itemMaxStatistics + 1);
    }

    public void EquipItem(CombatStatistics statistics, int stats, InventorySlot slot)
    {
        if(slot.equippedItem != null)
            if (stats <= slot.equippedItem.randomStatistic) return;

        if(slot.equippedItem != null)
        {
            if (statBoostType == StatBoostType.ATTACK)
                statistics.damage -= slot.equippedItem.randomStatistic;
            else if (statBoostType == StatBoostType.HEALTH)
                statistics.maxHealth -= slot.equippedItem.randomStatistic;
        }

        slot.equippedItem = this;
        slot.slotItemImage.sprite = itemSprite;
        slot.slotItemImage.gameObject.SetActive(true);

        if(statBoostType == StatBoostType.ATTACK)
            slot.tip.tipToShow = $"{itemName} <br> ATK: {randomStatistic}";
        else if (statBoostType == StatBoostType.HEALTH)
            slot.tip.tipToShow = $"{itemName} <br> HP: {randomStatistic}";

        if (statBoostType == StatBoostType.ATTACK)
        {
            statistics.damage += stats;
            PlayerManager.Instance.playerInventory.attackText.text = statistics.damage.ToString();
        }
        else if (statBoostType == StatBoostType.HEALTH)
        {
            statistics.maxHealth += stats;
            statistics.currentHealth += stats;

            if (statistics.currentHealth > statistics.maxHealth) statistics.currentHealth = statistics.maxHealth;
            if (statistics.maxHealth < statistics.currentHealth) statistics.maxHealth = statistics.currentHealth;

            PlayerManager.Instance.playerInventory.healthText.text = $"{statistics.currentHealth}/{statistics.maxHealth}";
        }
    }
}

public enum ItemType
{
    WEAPON,
    HEAD,
    CHEST,
    BOOTS,
    LEGS,
    SHIELD,
    RING
}

public enum StatBoostType
{
    ATTACK,
    HEALTH,
}
