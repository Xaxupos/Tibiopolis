using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public List<BoardCard> allBoardCards = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);  
    }

    private void Start()
    {
        SpawnCollectibles();
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

    private void SpawnCollectibles()
    {
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
                SpawnEnemy(card, lines[lineIndex]);
            }
            else if(card.cardType == CardType.BARREL)
            {
                SpawnBarrel(card);
            }
            else if(card.cardType == CardType.CHEST)
            {
                SpawnChest(card);
            }
        }
    }

    private void SpawnEnemy(BoardCard card, List<Collectible> list)
    {
        var randomEnemyIndex = Random.Range(0, list.Count);
        var spawnedEnemy = Instantiate(list[randomEnemyIndex], card.transform);
        spawnedEnemy.transform.localPosition = Vector3.zero;
        spawnedEnemy.transform.localEulerAngles = card.cardNameTransform.localEulerAngles;
        card.boundedCollectible = spawnedEnemy;
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
}
