using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerMovement playerMovement;
    public CombatStatistics statistics;
    public PlayerInventory playerInventory;
    public AttackAudio attackAudio;
    public AttackAudio moveAudio;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void PlayMoveSound()
    {
        moveAudio.PlayAttackClip();
    }
}
