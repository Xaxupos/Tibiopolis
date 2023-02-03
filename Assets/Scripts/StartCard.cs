using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartCard : Collectible
{
    public AudioSource winSound;
    public int goldAmount = 100;
    public CanvasGroup goldCanvas;
    public TMPro.TMP_Text goldValueText;

    private Vector2 localPos;

    public override void OnCollect()
    {
        localPos = goldCanvas.transform.localPosition;
        goldValueText.text = $"{goldAmount}";

        goldCanvas.transform.DOMove(new Vector3(-1.5f, 0, 0), 0.75f);
        goldCanvas.DOFade(1f, 0.75f).OnComplete(() => { goldCanvas.DOFade(0f, 0.75f).SetDelay(0.6f).OnComplete(() => goldCanvas.transform.localPosition = localPos); });

        int wins = PlayerPrefs.GetInt($"GetWinsByID{ProfileManager.Instance.currentID}");
        wins++;

        PlayerPrefs.SetInt($"GetWinsByID{ProfileManager.Instance.currentID}", wins);

        Invoke("InvokeAction", 0.4f);
    }

    public void InvokeAction()
    {
        winSound.Play();

        PlayerManager.Instance.playerInventory.gold += goldAmount;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";

        int gold = PlayerManager.Instance.playerInventory.gold;
        int roundedGold = 25 * Mathf.RoundToInt((float)gold / 25);
        PlayerManager.Instance.playerInventory.goldText.text = $"{roundedGold}";

        UIManager.Instance.rollButton.gameObject.SetActive(false);
        UIManager.Instance.menuButton.SetActive(true);
        SaveManager.Instance.DeleteProfile();
    }
}
