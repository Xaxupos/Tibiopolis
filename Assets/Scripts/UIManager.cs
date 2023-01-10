using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject gameOverCanvas;
    public Button rollButton;
    public SpriteRenderer swordsSprite;
    public TMP_Text rolledDmgText;
    public TMP_Text goldLootText;
    public TMP_Text itemLootText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void StartBattle()
    {
        swordsSprite.DOFade(1f, 1f);
        rollButton.gameObject.SetActive(false);
    }

    public void EndBattle()
    {
        swordsSprite.DOFade(0f, 1f);
        rolledDmgText.DOFade(0, 0.5f);
    }
}
