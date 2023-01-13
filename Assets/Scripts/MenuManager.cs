using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PassProfileName()
    {
        ProfileManager.Instance.currentProfile = GetComponent<TMP_InputField>().text;
    }
}
