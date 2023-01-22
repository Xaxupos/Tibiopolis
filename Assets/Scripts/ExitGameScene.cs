using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameScene : MonoBehaviour
{
    public void LoadMenu()
    {
        ProfileManager.Instance.DeleteProfile();
        SceneManager.LoadScene("Menu");
    }
}
