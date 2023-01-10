using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollDice : MonoBehaviour
{
    public TMP_Text diceOutputText;
    public AudioSource rollSound;

    private int move1;
    private int move2;

    public void Roll()
    {
        if (PlayerManager.Instance.playerMovement.isMoving) return;
        PlayerManager.Instance.playerMovement.isMoving = true;
        rollSound.Play();

        int randomDiceNumber = Random.Range(1, 7);
        diceOutputText.text = randomDiceNumber.ToString();

        int indexToMove = PlayerManager.Instance.playerMovement.currentCardIndex + randomDiceNumber;

        if(indexToMove >= 40)
        {
            int newIndexToMove = indexToMove - 40;
            move1 = newIndexToMove;
            Invoke("InvokeMove", 0.75f);
            return;
        }

        move2 = indexToMove;
        Invoke("InvokeMove2", 0.75f);
    }

    public void InvokeMove()
    {
        PlayerManager.Instance.playerMovement.Move(move1);
    }
    public void InvokeMove2()
    {
        PlayerManager.Instance.playerMovement.Move(move2);
    }
}
