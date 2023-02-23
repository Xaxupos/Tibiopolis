using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinsAdderRemover : MonoBehaviour
{
    public TMP_Text savedEmptyID;

    //Najpierw to, zdebugowa� puste ID
    public void Debugs()
    {
        var idByName = PlayerPrefs.GetInt($"GetIDByName ");
        Debug.Log("Empty Winner ID: " + idByName);
        savedEmptyID.text = idByName.ToString();
    }

    //Ustawi� puste ID na zero
    public void ClearWins(int id)
    {
        PlayerPrefs.SetInt($"GetWinsByID{id}", 0);
    }

    //Ustawi� boburas na 1
    public void SetWinToOne(string name)
    {
        if(PlayerPrefs.HasKey($"GetIDByName{name}"))
        {
            int ID = PlayerPrefs.GetInt($"GetIDByName{name}");

            PlayerPrefs.SetInt($"GetWinsByID{ID}", 1);
        }
    }
}
