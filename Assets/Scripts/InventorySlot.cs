using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemType itemType;
    public Item equippedItem;
    public HoverTip tip;

    public Image slotItemImage;
}
