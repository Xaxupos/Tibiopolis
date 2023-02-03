using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WinnersManager : MonoBehaviour
{
    public Winner[] winners;

    private void Start()
    {
        foreach(var winner in winners)
        {
            winner.nickText.text = "";
            winner.winsText.text = "";
            winner.wins = 0;
        }

        List<int> winnnerIDS = new List<int>();

        for(int i=0; i<1000; i++)
        {
            if(PlayerPrefs.HasKey($"GetWinsByID{i}"))
            {
                int wins = PlayerPrefs.GetInt($"GetWinsByID{i}");

                if (wins>0)
                {
                    winnnerIDS.Add(i);
                }
            }
        }

        winnnerIDS = winnnerIDS.OrderBy(x => x).Take(8).ToList();

        for(int w=0; w<winnnerIDS.Count; w++)
        {
            winners[w].nickText.text = PlayerPrefs.GetString($"GetNickByID{winnnerIDS[w]}");
            winners[w].wins = PlayerPrefs.GetInt($"GetWinsByID{winnnerIDS[w]}");
            winners[w].winsText.text = winners[w].wins.ToString();
        }
    }

}

[System.Serializable]
public class Winner
{
    public TMP_Text nickText;
    public TMP_Text winsText;
    public int wins = 0;
}