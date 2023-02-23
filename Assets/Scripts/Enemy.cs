using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collectible
{
    public BoardCard assignedBoardCard;
    public CombatStatistics statistics;
    public MonsterLootTable lootTable;
    public AttackAudio attackAudio;
    public MonsterType monsterType;
    public bool scarlet = false;

    public int healAmount = 25;

    public override void OnCollect()
    {
        StartCoroutine(BattleManager.Instance.StartBattle(PlayerManager.Instance, this, assignedBoardCard));
    }
}

public enum EnemyRarity
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}

public enum MonsterType
{
    LARVA,
    TROLL,
    ROTWORM,
    ORC,
    CYCLOPS,
    BONELORD,
    WYRM,
    DRAGON,
    MINOTAUR,
    DEMON,
    EFREET,
    WOODLING,
    SCARAB,
    SCARLETT,
    RAT,
    SPECTRE,
    MUMIA,
    ANCIENT_SCARAB
}
