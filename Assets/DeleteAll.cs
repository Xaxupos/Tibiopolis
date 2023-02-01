using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAll : MonoBehaviour
{
    public void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
