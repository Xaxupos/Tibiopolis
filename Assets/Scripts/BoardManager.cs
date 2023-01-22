using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;


    public List<BoardCard> allBoardCards = new();
    public Dictionary<int, MonsterType> savedMonsterOnBoard = new Dictionary<int, MonsterType>(); 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);  
    }

    public void RotateBoard()
    {
        transform.DOLocalRotate(new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z + 90
            ), 0.5f);

        PlayerManager.Instance.transform.DOLocalRotate(new Vector3(
        PlayerManager.Instance.transform.localEulerAngles.x,
        PlayerManager.Instance.transform.localEulerAngles.y,
        PlayerManager.Instance.transform.localEulerAngles.z + -90
        ), 0.5f);
    }

    public void SpawnCollectibles()
    {
        bool spawnEnemies = true;
        if (savedMonsterOnBoard.Count > 0) spawnEnemies = false;

        foreach (var card in allBoardCards)
        {
            int lineIndex = (card.cardIndexInBoard - 1) / 10;
            var lines = new List<List<Collectible>>
                {
                  SpawnManager.Instance.firstLineEnemies,
                  SpawnManager.Instance.secondLineEnemies,
                  SpawnManager.Instance.thirdLineEnemies,
                  SpawnManager.Instance.fourthLineEnemies
                };

            if (card.cardType == CardType.ENEMY)
            {
                if(spawnEnemies)
                {
                    SpawnEnemy(card, lines[lineIndex]);
                    Debug.Log("Spawning not saved enemy!");
                }
            }
            else if(card.cardType == CardType.BARREL)
            {
                SpawnBarrel(card);
            }
            else if(card.cardType == CardType.CHEST)
            {
                SpawnChest(card);
            }
            else if (card.cardType == CardType.START)
            {
                SpawnStart(card);
            }
        }

        if(!PlayerPrefs.HasKey($"_PlayerBoardPos{ProfileManager.Instance.currentProfile}"))
        {
            var player = Instantiate(SpawnManager.Instance.playerPrefab, transform);
            player.transform.localPosition = new Vector2(3.5f, -3.5f);
        }
    }

    private void SpawnEnemy(BoardCard card, List<Collectible> list)
    {
        var randomEnemyIndex = Random.Range(0, list.Count);
        var spawnedEnemy = Instantiate(list[randomEnemyIndex], card.transform);
        spawnedEnemy.transform.localPosition = Vector3.zero;
        spawnedEnemy.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedEnemy;

        Shuffle(spawnedEnemy.GetComponent<Enemy>().lootTable.possibleItemsDrop);

        spawnedEnemy.GetComponent<Enemy>().assignedBoardCard = card;
        savedMonsterOnBoard.Add(card.cardIndexInBoard, spawnedEnemy.GetComponent<Enemy>().monsterType);
    }


    void Shuffle(List<Item> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            Item value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void SpawnLoadedEnemy(Collectible enemy, BoardCard card)
    {
        var spawnedEnemy = Instantiate(enemy, card.transform);
        spawnedEnemy.transform.localPosition = Vector3.zero;
        spawnedEnemy.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedEnemy;

        spawnedEnemy.GetComponent<Enemy>().assignedBoardCard = card;
        savedMonsterOnBoard.Add(card.cardIndexInBoard, spawnedEnemy.GetComponent<Enemy>().monsterType);
    }

    private void SpawnBarrel(BoardCard card)
    {
        var spawnedBarrel = Instantiate(SpawnManager.Instance.healBarrel, card.transform);
        spawnedBarrel.transform.localPosition = Vector3.zero;
        spawnedBarrel.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedBarrel;
    }

    private void SpawnChest(BoardCard card)
    {
        var spawnedChest = Instantiate(SpawnManager.Instance.goldChest, card.transform);
        spawnedChest.transform.localPosition = Vector3.zero;
        spawnedChest.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedChest;
    }

    private void SpawnStart(BoardCard card)
    {
        var spawnedStart = Instantiate(SpawnManager.Instance.startCard, card.transform);
        spawnedStart.transform.localPosition = Vector3.zero;
        spawnedStart.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedStart;
    }
}
