using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class ImbuMachine : Collectible
{
    public AudioSource imbuSound;
    public AudioSource goldSound;
    public AudioSource nothingSound;

    private int goldAmount = 0;

    private IEnumerator NoReward()
    {
        nothingSound.Play();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(false);
    }

    private IEnumerator Reward()
    {
        goldAmount = 25;
        goldSound.Play();
        PlayerManager.Instance.playerInventory.gold += goldAmount;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(false);

        goldAmount = 0;
    }

    public override void OnCollect()
    {
        UIManager.Instance.rollButton.gameObject.SetActive(false);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(true);
        imbuSound.Play();

        int random = Random.Range(1, 3);

        if (random == 1)
        {
            UIManager.Instance.reward1.SetReward(() => StartCoroutine(NoReward()));
            UIManager.Instance.reward2.SetReward(() => StartCoroutine(Reward()));
            UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); SetButtonText("25TC", UIManager.Instance.reward2.button); });
            UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("25TC", UIManager.Instance.reward2.button); SetButtonText("NIC", UIManager.Instance.reward1.button); });
        }
        else
        {
            UIManager.Instance.reward1.SetReward(() => StartCoroutine(Reward()));
            UIManager.Instance.reward2.SetReward(() => StartCoroutine(NoReward()));
            UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("25TC", UIManager.Instance.reward1.button); SetButtonText("NIC", UIManager.Instance.reward2.button); });
            UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); SetButtonText("25TC", UIManager.Instance.reward1.button); });
        }

        SaveManager.Instance.SaveProfile();
    }

    private void SetButtonText(string text, Button button)
    {
        button.GetComponentInChildren<TMPro.TMP_Text>().text = text;
    }
}
