using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCard : MonoBehaviour
{
    public Collectible boundedCollectible;
    public Transform cardNameTransform;
    public Transform cardObjectTransform;
    public Vector2 cardMovePosition;
    public CardType cardType = CardType.ENEMY;
    public int cardIndexInBoard = -1;

    private void Awake()
    {
        cardMovePosition = transform.localPosition;
    }

    public void TriggerCardActions()
    {
        if (boundedCollectible == null) return;

        boundedCollectible.OnCollect();
        boundedCollectible = null;
    }
}

public enum CardType
{
    ENEMY,
    CHANCE,
    CHEST,
    BARREL
}
