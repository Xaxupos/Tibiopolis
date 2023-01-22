using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject inventoryCanvas;
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

    private void Start()
    {
        inventoryCanvas.transform.SetParent(null);
    }

    public void PlayMoveSound()
    {
        moveAudio.PlayAttackClip();
    }
}
