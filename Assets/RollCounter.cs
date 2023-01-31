using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RollCounter : MonoBehaviour
{
    public TMP_Text text;

    public int counter = 0;

    public void IncreaseCounter()
    {
        counter++;
        text.text = "Licznik: " + counter.ToString();
    }
}
