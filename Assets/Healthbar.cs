using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject holder;
    public Image healthbarImage;

    public void UpdateHealthbar(int currentHealth, int maxHealth)
    {
        float hpBarValue = (float)currentHealth / (float)maxHealth;

        DOVirtual.Float(healthbarImage.fillAmount, hpBarValue, 0.5f, x => healthbarImage.fillAmount = x);
    }
}
