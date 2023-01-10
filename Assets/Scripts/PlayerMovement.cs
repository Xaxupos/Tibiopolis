using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float cardMoveTime = 0.5f;
    public Ease cardMoveEase = Ease.InQuart;
    public int currentCardIndex = 0;
    public int currentLineIndex = 0;
    public bool isMoving = false;

    public void Move(int indexToMove)
    {
        PlayerManager.Instance.PlayMoveSound();
        BoardCard cardToMove;

        isMoving = true;

        if (currentCardIndex == indexToMove)
        {
            BoardCard finalCard = BoardManager.Instance.allBoardCards[currentCardIndex];
            finalCard.TriggerCardActions();
            isMoving = false;
            Debug.Log($"Completed movement! Triggering {finalCard.name} card actions!");
            return;
        }

        int indexToMoveInternal = currentCardIndex+1;
        if (indexToMoveInternal >= 40) indexToMoveInternal = 0;
        cardToMove = BoardManager.Instance.allBoardCards[indexToMoveInternal];
      
        Debug.Log($"Moving to card {cardToMove.name}");

        transform.DOLocalMove(cardToMove.cardMovePosition, cardMoveTime)
            .SetEase(cardMoveEase)
            .OnComplete(()=> 
            {
                int lineIndex = (cardToMove.cardIndexInBoard - 1) / 10;
                if (currentLineIndex != lineIndex)
                {
                    currentLineIndex = lineIndex;
                    BoardManager.Instance.RotateBoard();
                }

                currentCardIndex++;
                if (currentCardIndex >= 40) currentCardIndex = 0;
                Move(indexToMove); 
            });
    }
}
