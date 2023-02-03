using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    public string currentProfile = "";
    public int currentID = 0;
    public int currentWins = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentID"))
            PlayerPrefs.SetInt("CurrentID", 0);
    }

    public void CreateProfile()
    {
        if (currentProfile == "") return;
        if (PlayerPrefs.HasKey($"_ProfileName{currentProfile}")) return;

        PlayerPrefs.SetString($"_ProfileName{currentProfile}", $"{currentProfile}");
        
        if(PlayerPrefs.HasKey($"GetIDByName{currentProfile}"))
        {
            currentID = PlayerPrefs.GetInt($"GetIDByName{currentProfile}");
            currentWins = PlayerPrefs.GetInt($"GetWinsByID{currentID}");
        }
        else
        {
            int tempID = PlayerPrefs.GetInt("CurrentID");
            tempID++;
            currentID = tempID;
            currentWins = 0;
            PlayerPrefs.SetInt("CurrentID", currentID);
            PlayerPrefs.SetInt($"GetWinsByID{currentID}", currentWins);
            PlayerPrefs.SetInt($"GetIDByName{currentProfile}", currentID);
            PlayerPrefs.SetString($"GetNickByID{currentID}", currentProfile);
        }

        SceneManager.LoadScene("Game");
    }

    public void LoadProfile()
    {
        if (currentProfile == "") return;
        if (!PlayerPrefs.HasKey($"_ProfileName{currentProfile}")) return;

        currentID = PlayerPrefs.GetInt($"GetIDByName{currentProfile}");
        currentWins = PlayerPrefs.GetInt($"GetWinsByID{currentID}");

        SceneManager.LoadScene("Game");
    }

    public void DeleteProfile()
    {
        if(PlayerPrefs.HasKey($"_ProfileName{currentProfile}"))
        {
            PlayerPrefs.DeleteKey($"_ProfileName{currentProfile}");
        }
    }
}
