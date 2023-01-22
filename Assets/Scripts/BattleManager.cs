using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public AudioSource battleEngageSound;
    public AttackAudio dieAudio;
    public AudioSource goldLootAudio;

    public Transform playerBattlePosition;
    public Transform enemyBattlePosition;
    public Transform lootedItemPosition;
    public Transform goldItemPosition;

    public GameObject goldPrefab;

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

    public IEnumerator StartBattle(PlayerManager player, Enemy enemy, BoardCard card)
    {
        battleEngageSound.Play();
        InitialSettings(player, enemy);

        MoveFighters(player, enemy);

        yield return new WaitForSeconds(2f);

        SetFightersHP(player, enemy);

        yield return new WaitForSeconds(0.75f);

        StopAllCoroutines();
        StartCoroutine(PlayerTurn(player, enemy, card));
    }

    private static void SetFightersHP(PlayerManager player, Enemy enemy)
    {
        player.statistics.healthbar.holder.SetActive(true);
        enemy.statistics.healthbar.holder.SetActive(true);

        enemy.statistics.statsText.gameObject.SetActive(true);
        enemy.statistics.statsText.text = $"ATK: {enemy.statistics.damage} <br> HP: {enemy.statistics.currentHealth}";
    }

    private void MoveFighters(PlayerManager player, Enemy enemy)
    {
        player.transform.DOMove(playerBattlePosition.position, 1f);
        enemy.transform.DOMove(enemyBattlePosition.position, 1f);
    }

    private void InitialSettings(PlayerManager player, Enemy enemy)
    {
        UIManager.Instance.StartBattle();

        playerSavedPos = player.transform.localPosition;
        enemySavedPos = enemy.transform.localPosition;
    }

    public IEnumerator PlayerTurn(PlayerManager player, Enemy enemy, BoardCard card)
    {
        player.statistics.Attack(enemy.statistics, UIManager.Instance.rolledDmgText, true);

        yield return new WaitForSeconds(1.0f);
        player.attackAudio.PlayAttackClip();
        if(enemy == null)
            dieAudio.PlayAttackClip();
        yield return new WaitForSeconds(0.6f);

        if (enemy != null)
        {
            StopAllCoroutines();
            StartCoroutine(EnemyTurn(player, enemy, card));
        }
        else
        {
            FinishBattle(true, player, enemy, card);
        }
    }

    public IEnumerator EnemyTurn(PlayerManager player, Enemy enemy, BoardCard card)
    {
        enemy.statistics.Attack(player.statistics, UIManager.Instance.rolledDmgText, false);

        yield return new WaitForSeconds(1.0f);
        enemy.attackAudio.PlayAttackClip();
        if (player == null)
            dieAudio.PlayAttackClip();
        yield return new WaitForSeconds(0.6f);

        if (player != null)
        {
            StopAllCoroutines();
            StartCoroutine(PlayerTurn(player, enemy, card));
        }
        else
        {
            FinishBattle(false, player, enemy, card);
        }
    }

    public void FinishBattle(bool win, PlayerManager player, Enemy enemy, BoardCard battleCard)
    {
        UIManager.Instance.EndBattle();
        if(win)
        {
            int possibleGold = Random.Range(enemy.lootTable.minGoldDrop, enemy.lootTable.maxGoldDrop + 1);

            var gold = Instantiate(goldPrefab, goldItemPosition.position, Quaternion.identity);
            gold.GetComponent<SpriteRenderer>().DOFade(1f, 1.5f).OnComplete(() => gold.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetDelay(0.6f));
            goldLootAudio.Play();

            UIManager.Instance.goldLootText.text = $"{possibleGold}";
            UIManager.Instance.goldLootText.DOFade(1f, 1.5f).OnComplete(()=> UIManager.Instance.goldLootText.DOFade(0f, 1f).SetDelay(0.6f));

            List<Item> possibleLoot = enemy.lootTable.GetLoot();

            foreach (var item in possibleLoot)
            {
                var newItem = Instantiate(item, lootedItemPosition.position, Quaternion.identity);;
                var newItemSpriteRenderer = newItem.GetComponent<SpriteRenderer>();

                if (item.statBoostType == StatBoostType.ATTACK)
                    UIManager.Instance.itemLootText.text = $"{newItem.itemName} <br> ATK: {newItem.itemStats}";
                else if (item.statBoostType == StatBoostType.HEALTH)
                    UIManager.Instance.itemLootText.text = $"{newItem.itemName} <br> HP: {newItem.itemStats}";

                UIManager.Instance.itemLootText.DOFade(1f, 1.5f).OnComplete(() => UIManager.Instance.itemLootText.DOFade(0f, 1f).SetDelay(0.6f));

                newItemSpriteRenderer.color = new Color(newItemSpriteRenderer.color.r, newItemSpriteRenderer.color.g, newItemSpriteRenderer.color.b, 0);
                newItemSpriteRenderer.DOFade(1f, 1.5f).OnComplete(() => newItemSpriteRenderer.DOFade(0f, 1f).SetDelay(0.6f));

                newItem.EquipItem(player.statistics, newItem.itemStats, player.playerInventory.equipment[newItem.itemType], newItem.itemType, newItem.indexInDatabase);
                Debug.Log($"Looted {newItem.itemName} with {newItem.itemStats} statistics!");
            }

            player.playerInventory.gold += possibleGold;
            player.playerInventory.goldText.text = $"{player.playerInventory.gold}";

            StopAllCoroutines();
            player.statistics.healthbar.holder.SetActive(false);
            player.transform.DOLocalMove(playerSavedPos, 1f)
               .OnComplete(()=> UIManager.Instance.rollButton.gameObject.SetActive(true));

            BoardManager.Instance.savedMonsterOnBoard.Remove(battleCard.cardIndexInBoard);

            if(PlayerPrefs.HasKey($"_Enemy{battleCard.cardIndexInBoard}Board{ProfileManager.Instance.currentProfile}"))
            {
                PlayerPrefs.DeleteKey($"_Enemy{battleCard.cardIndexInBoard}Board{ProfileManager.Instance.currentProfile}");
            }

            SaveManager.Instance.SaveProfile();
        }
        else
        {
            StopAllCoroutines();
            enemy.statistics.statsText.gameObject.SetActive(false);
            enemy.statistics.healthbar.holder.SetActive(false);
            enemy.transform.DOLocalMove(enemySavedPos, 1f);
            SaveManager.Instance.DeleteProfile();
        }
    }
}
