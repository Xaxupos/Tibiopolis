using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackAudio : MonoBehaviour
{
    public AudioClip[] attackClips;
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAttackClip()
    {
        source.PlayOneShot(attackClips[Random.Range(0, attackClips.Length)]);
    }
}
