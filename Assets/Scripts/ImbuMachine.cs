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

        SaveManager.Instance.SaveProfile();
    }

    private IEnumerator Reward20()
    {
        goldAmount = 20;
        goldSound.Play();
        PlayerManager.Instance.playerInventory.gold += goldAmount;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(false);

        goldAmount = 0;

        SaveManager.Instance.SaveProfile();
    }

    private IEnumerator Reward10()
    {
        goldAmount = 10;
        goldSound.Play();
        PlayerManager.Instance.playerInventory.gold += goldAmount;
        PlayerManager.Instance.playerInventory.goldText.text = $"{PlayerManager.Instance.playerInventory.gold}";
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(false);

        goldAmount = 0;

        SaveManager.Instance.SaveProfile();
    }

    public override void OnCollect()
    {
        UIManager.Instance.rollButton.gameObject.SetActive(false);
        UIManager.Instance.imbuCanvas.gameObject.SetActive(true);
        imbuSound.Play();

        SetButtonText("", UIManager.Instance.reward1.button);
        SetButtonText("", UIManager.Instance.reward2.button);
        SetButtonText("", UIManager.Instance.reward3.button);

        RemoveOnClicks(UIManager.Instance.reward1.button);
        RemoveOnClicks(UIManager.Instance.reward2.button);
        RemoveOnClicks(UIManager.Instance.reward3.button);

        int randomNic = Random.Range(1, 4);

        if(randomNic == 1)
        {
            UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1,3);

            if(random10 == 1)
            {
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); });
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward20()); });
            }
            else if(random10 == 2)
            {
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward3.button); });
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward20()); });
            }
        }
        else if(randomNic == 2)
        {
            UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1, 3);

            if (random10 == 1)
            {
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); });
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward3.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward3.button); StartCoroutine(Reward20()); });
            }
            else if (random10 == 2)
            {
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); });
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward1.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward1.button); StartCoroutine(Reward20()); });
            }
        }
        else if(randomNic == 3)
        {
            UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward3.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1, 3);

            if (random10 == 1)
            {
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward2.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); });
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward3.button); SetButtonText("10 TC", UIManager.Instance.reward1.button); SetButtonText("20 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward20()); });
            }
            else if (random10 == 2)
            {
                UIManager.Instance.reward3.button.onClick.AddListener(() => { SetButtonText("20 TC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); });
                UIManager.Instance.reward2.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward1.button.onClick.AddListener(() => { SetButtonText("NIC", UIManager.Instance.reward3.button); SetButtonText("20 TC", UIManager.Instance.reward1.button); SetButtonText("10 TC", UIManager.Instance.reward2.button); StartCoroutine(Reward20()); });
            }
        }

        SaveManager.Instance.SaveProfile();
    }

    private void SetButtonText(string text, Button button)
    {
        button.GetComponentInChildren<TMPro.TMP_Text>().text = text;
    }

    private void RemoveOnClicks(Button button)
    {
        button.onClick.RemoveAllListeners();
    }
}
