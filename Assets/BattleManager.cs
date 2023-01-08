using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public Transform playerBattlePosition;
    public Transform enemyBattlePosition;
    public GameObject rollButton;
    public SpriteRenderer swordsSprite;
    public TMP_Text rolledDmgText;

    public static BattleManager Instance;

    private Vector3 playerSavedPos;
    private Vector3 enemySavedPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public IEnumerator StartBattle(PlayerManager player, Enemy enemy)
    {
        swordsSprite.DOFade(1f, 1f);
        rollButton.SetActive(false);

        playerSavedPos = player.transform.localPosition;
        enemySavedPos = enemy.transform.localPosition;

        player.transform.DOMove(playerBattlePosition.position, 1f);
        enemy.transform.DOMove(enemyBattlePosition.position, 1f);

        yield return new WaitForSeconds(2f);

        player.statistics.hpText.text = "HP: "+player.statistics.currentHealth.ToString();
        enemy.statistics.hpText.text = "HP: "+ enemy.statistics.currentHealth.ToString();

        player.statistics.hpText.DOFade(1f, 0.25f);
        enemy.statistics.hpText.DOFade(1f, 0.25f);

        yield return new WaitForSeconds(0.75f);

        StopAllCoroutines();
        StartCoroutine(PlayerTurn(player, enemy));
    }

    public IEnumerator PlayerTurn(PlayerManager player, Enemy enemy)
    {
        player.statistics.Attack(enemy.statistics, rolledDmgText, true);

        yield return new WaitForSeconds(1.6f);

        if(enemy != null)
        {
            StopAllCoroutines();
            StartCoroutine(EnemyTurn(player, enemy));
        }
        else
        {
            FinishBattle(true, player, enemy);
        }
    }

    public IEnumerator EnemyTurn(PlayerManager player, Enemy enemy)
    {
        enemy.statistics.Attack(player.statistics, rolledDmgText, false);

        yield return new WaitForSeconds(1.6f);

        if (player != null)
        {
            StopAllCoroutines();
            StartCoroutine(PlayerTurn(player, enemy));
        }
        else
        {
            FinishBattle(false, player, enemy);
        }
    }

    public void FinishBattle(bool win, PlayerManager player, Enemy enemy)
    {
        swordsSprite.DOFade(0f, 1f);
        rolledDmgText.DOFade(0, 0.5f);
        if(win)
        {
            StopAllCoroutines();
            player.statistics.hpText.DOFade(0f, 0.25f);
            player.transform.DOLocalMove(playerSavedPos, 1f)
               .OnComplete(()=> rollButton.SetActive(true));
        }
        else
        {
            StopAllCoroutines();
            enemy.statistics.hpText.DOFade(0f, 0.25f);
            enemy.transform.DOLocalMove(enemySavedPos, 1f);
        }
    }
}
