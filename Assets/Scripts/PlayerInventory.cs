using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [Header("Slots")]
    public InventorySlot headSlot;
    public InventorySlot chestSlot;
    public InventorySlot weaponSlot;
    public InventorySlot shieldSlot;
    public InventorySlot legsSlot;
    public InventorySlot bootsSlot;
    public InventorySlot ringSlot;

    [Header("UI")]
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text goldText;

    public Dictionary<ItemType, InventorySlot> equipment = new();
    public int gold = 0;

    private void Awake()
    {
        equipment.Add(ItemType.HEAD, headSlot);
        equipment.Add(ItemType.CHEST, chestSlot);
        equipment.Add(ItemType.WEAPON, weaponSlot);
        equipment.Add(ItemType.SHIELD, shieldSlot);
        equipment.Add(ItemType.LEGS, legsSlot);
        equipment.Add(ItemType.BOOTS, bootsSlot);
        equipment.Add(ItemType.RING, ringSlot);
    }
}
