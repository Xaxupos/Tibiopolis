using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLootTable : MonoBehaviour
{
    public List<Item> possibleItemsDrop = new();
    public int minGoldDrop = 1;
    public int maxGoldDrop = 4;
    public int maxItemsDrop = 1;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public List<Item> GetLoot()
    {
        List<Item> lootedItems = new();
        foreach(var item in possibleItemsDrop)
        {
            if (lootedItems.Count == maxItemsDrop) return lootedItems;

            if (Random.value < item.dropChance)
                lootedItems.Add(item);
        }

        return lootedItems;
    }
}
