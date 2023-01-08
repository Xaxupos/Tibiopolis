using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collectible
{
    public CombatStatistics statistics;

    public override void OnCollect()
    {
        StartCoroutine(BattleManager.Instance.StartBattle(PlayerManager.Instance, this));
    }
}
