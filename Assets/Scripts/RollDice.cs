using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollDice : MonoBehaviour
{
    public TMP_Text diceOutputText;

    public void Roll()
    {
        if (PlayerManager.Instance.playerMovement.isMoving) return;

        int randomDiceNumber = Random.Range(1, 7);
        diceOutputText.text = randomDiceNumber.ToString();

        int indexToMove = PlayerManager.Instance.playerMovement.currentCardIndex + randomDiceNumber;

        if(indexToMove >= 40)
        {
            int newIndexToMove = indexToMove - 40;
            PlayerManager.Instance.playerMovement.Move(newIndexToMove);
            return;
        }

        PlayerManager.Instance.playerMovement.Move(indexToMove);
    }
}
