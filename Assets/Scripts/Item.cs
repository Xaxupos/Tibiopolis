using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public ItemType itemType;
    public StatBoostType statBoostType = StatBoostType.HEALTH;
    public Sprite itemSprite;
    public string itemName = "";
    public float dropChance = 0.5f;
    public int indexInDatabase = 0;

    public int itemStats = 0;

    public void EquipItemNoStats(CombatStatistics statistics, InventorySlot slot, int itemIndexInDatabase)
    {
        var stats = ItemDatabase.Instance.GetItemByIndex(itemIndexInDatabase).itemStats;

        if (statBoostType == StatBoostType.ATTACK)
            statistics.damage -= stats;
        else if (statBoostType == StatBoostType.HEALTH)
        {
            statistics.maxHealth -= stats;
            statistics.currentHealth -= stats;
        }

        slot.equippedItem = this;
        slot.slotItemImage.sprite = itemSprite;
        slot.slotItemImage.gameObject.SetActive(true);

        if (statBoostType == StatBoostType.ATTACK)
            slot.tip.tipToShow = $"{itemName} <br> ATK: {itemStats}";
        else if (statBoostType == StatBoostType.HEALTH)
            slot.tip.tipToShow = $"{itemName} <br> HP: {itemStats}";

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

    public void EquipItem(CombatStatistics statistics, int stats, InventorySlot slot, ItemType itemType, int itemIndex)
    {
        if(slot.equippedItem != null)
            if (stats <= slot.equippedItem.itemStats) return;

        if(slot.equippedItem != null)
        {
            if (statBoostType == StatBoostType.ATTACK)
                statistics.damage -= slot.equippedItem.itemStats;
            else if (statBoostType == StatBoostType.HEALTH)
                statistics.maxHealth -= slot.equippedItem.itemStats;
        }

        slot.equippedItem = this;
        slot.slotItemImage.sprite = itemSprite;
        slot.slotItemImage.gameObject.SetActive(true);

        if(statBoostType == StatBoostType.ATTACK)
            slot.tip.tipToShow = $"{itemName} <br> ATK: {itemStats}";
        else if (statBoostType == StatBoostType.HEALTH)
            slot.tip.tipToShow = $"{itemName} <br> HP: {itemStats}";

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

        PlayerManager.Instance.playerInventory.healthText.text = $"{PlayerManager.Instance.statistics.currentHealth}/{PlayerManager.Instance.statistics.maxHealth}";
        PlayerManager.Instance.statistics.healthbar.UpdateHealthbar(PlayerManager.Instance.statistics.currentHealth, PlayerManager.Instance.statistics.maxHealth);
        SaveManager.Instance.SaveItem(itemType, itemIndex);
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
