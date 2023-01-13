using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldChest : Collectible
{
    public AudioSource goldSound;
    public int minGoldAmount = 10;
    public int maxGoldAmount = 25;
    public int goldAmount = 0;
    public CanvasGroup goldCanvas;
    public TMPro.TMP_Text goldValueText;

    private Vector2 localPos;

    public override void OnCollect()
    {
        localPos = goldCanvas.transform.localPosition;

        goldAmount = Random.Range(minGoldAmount, maxGoldAmount + 1);
        goldValueText.text = $"{goldAmount}";

        goldCanvas.transform.DOMove(new Vector3(-1.5f, 0, 0), 0.75f);
        goldCanvas.DOFade(1f, 0.75f).OnComplete(() => { goldCanvas.DOFade(0f, 0.75f).SetDelay(0.6f).OnComplete(()=> goldCanvas.transform.localPosition = localPos); });

        Invoke("InvokeAction", 0.4f);
    }

    public void InvokeAction()
    {
        goldSound.Play();

        PlayerManager.Instance.playerInventory.gold += goldAmount;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";

        SaveManager.Instance.SaveProfile();
    }
}
