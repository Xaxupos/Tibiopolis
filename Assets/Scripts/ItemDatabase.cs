using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<Item> items = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public Item GetItemByName(string itemName)
    {
        foreach(var item in items)
        {
            if (item.itemName == itemName)
                return item;
        }

        return null;
    }
}
