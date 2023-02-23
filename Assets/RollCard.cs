using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class RollCard : Collectible
{
    public AudioSource rollEnterSound;
    public AudioSource winSound;
    public AudioSource nothingSound;

    private IEnumerator NoReward()
    {
        nothingSound.Play();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.rollkopertyCanvas.gameObject.SetActive(false);

        SaveManager.Instance.SaveProfile();
    }

    private IEnumerator Reward20()
    {
        winSound.Play();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.rollkopertyCanvas.gameObject.SetActive(false);
        UIManager.Instance.SetExtraText(2);

        SaveManager.Instance.SaveProfile();
    }

    private IEnumerator Reward10()
    {
        winSound.Play();

        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.rollButton.gameObject.SetActive(true);
        UIManager.Instance.rollkopertyCanvas.gameObject.SetActive(false);
        UIManager.Instance.SetExtraText(1);

        SaveManager.Instance.SaveProfile();
    }

    public override void OnCollect()
    {
        UIManager.Instance.rollButton.gameObject.SetActive(false);
        UIManager.Instance.rollkopertyCanvas.gameObject.SetActive(true);
        rollEnterSound.Play();

        SetButtonText("", UIManager.Instance.reward1roll.button);
        SetButtonText("", UIManager.Instance.reward2roll.button);
        SetButtonText("", UIManager.Instance.reward3roll.button);

        RemoveOnClicks(UIManager.Instance.reward1roll.button);
        RemoveOnClicks(UIManager.Instance.reward2roll.button);
        RemoveOnClicks(UIManager.Instance.reward3roll.button);

        int randomNic = Random.Range(1, 4);

        if (randomNic == 1)
        {
            UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward1roll.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1, 3);

            if (random10 == 1)
            {
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); });
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); StartCoroutine(Reward20()); });
            }
            else if (random10 == 2)
            {
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward3roll.button); });
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward2roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward2roll.button); StartCoroutine(Reward20()); });
            }
        }
        else if (randomNic == 2)
        {
            UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward2roll.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1, 3);

            if (random10 == 1)
            {
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); });
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward3roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward3roll.button); StartCoroutine(Reward20()); });
            }
            else if (random10 == 2)
            {
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); });
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward1roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward1roll.button); StartCoroutine(Reward20()); });
            }
        }
        else if (randomNic == 3)
        {
            UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward3roll.button); StartCoroutine(NoReward()); });

            int random10 = Random.Range(1, 3);

            if (random10 == 1)
            {
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward2roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); });
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward2roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward3roll.button); SetButtonText("1", UIManager.Instance.reward1roll.button); SetButtonText("2", UIManager.Instance.reward2roll.button); StartCoroutine(Reward20()); });
            }
            else if (random10 == 2)
            {
                UIManager.Instance.reward3roll.button.onClick.AddListener(() => { SetButtonText("2", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); });
                UIManager.Instance.reward2roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); StartCoroutine(Reward10()); });
                UIManager.Instance.reward1roll.button.onClick.AddListener(() => { SetButtonText("0", UIManager.Instance.reward3roll.button); SetButtonText("2", UIManager.Instance.reward1roll.button); SetButtonText("1", UIManager.Instance.reward2roll.button); StartCoroutine(Reward20()); });
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
