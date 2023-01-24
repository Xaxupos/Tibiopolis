using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public Collectible healBarrel = new();
    public Collectible goldChest = new();
    public Collectible scarlett = new();
    public Collectible startCard = new();
    public Collectible imbu = new();

    public List<Collectible> firstLineEnemies = new();
    public List<Collectible> secondLineEnemies = new();
    public List<Collectible> thirdLineEnemies = new();
    public List<Collectible> fourthLineEnemies = new();

    public List<Enemy> allEnemies = new List<Enemy>();

    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
