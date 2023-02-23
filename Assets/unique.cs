using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class unique : MonoBehaviour
{
    public TMP_Text uniqueText;

    // Start is called before the first frame update
    void Start()
    {
        int unique = PlayerPrefs.GetInt("CurrentID");
        uniqueText.text = "unique users: " + unique.ToString();
    }
}
