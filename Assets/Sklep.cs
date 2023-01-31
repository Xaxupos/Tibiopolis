using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sklep : Collectible
{
    public override void OnCollect()
    {
        UIManager.Instance.sklepCanvas.SetActive(true);
        UIManager.Instance.rollButton.gameObject.SetActive(false);
    }
}
