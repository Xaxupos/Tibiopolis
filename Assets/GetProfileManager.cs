using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProfileManager : MonoBehaviour
{
    public void LoadProfile()
    {
        ProfileManager.Instance.LoadProfile();
    }

    public void NewProfiel()
    {
        ProfileManager.Instance.CreateProfile();
    }
}
