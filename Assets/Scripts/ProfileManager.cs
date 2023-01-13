using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    public string currentProfile = "";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateProfile()
    {
        if (currentProfile == "") return;
        if (PlayerPrefs.HasKey($"_ProfileName{currentProfile}")) return;

        PlayerPrefs.SetString($"_ProfileName{currentProfile}", $"{currentProfile}");
        Debug.Log($"Created new profile with key _ProfileName{currentProfile} and value {currentProfile}");

        SceneManager.LoadScene("Game");
    }

    public void LoadProfile()
    {
        if (currentProfile == "") return;
        if (!PlayerPrefs.HasKey($"_ProfileName{currentProfile}")) return;

        SceneManager.LoadScene("Game");
    }
}
