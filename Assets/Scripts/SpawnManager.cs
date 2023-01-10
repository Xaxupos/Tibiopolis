using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Collectible healBarrel = new();

    public List<Collectible> firstLineEnemies = new();
    public List<Collectible> secondLineEnemies = new();
    public List<Collectible> thirdLineEnemies = new();
    public List<Collectible> fourthLineEnemies = new();

    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
