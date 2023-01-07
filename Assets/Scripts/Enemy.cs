using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collectible
{
    public override void OnCollect()
    {
        Destroy(gameObject);
    }
}
