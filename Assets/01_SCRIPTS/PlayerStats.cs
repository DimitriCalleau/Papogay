using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStats
{
    public float currentHealth;
    public float maxHealth;
    public float healthPercentage;
    public int gold;
    public int startGold;
    public bool invincible;
    public float invincibilityTime;
    public float speed;
    public float rollSpeed;
    public float boostFactor;

    public float indicatorShowTime;
    public float timerIndicator;

    bool itsAlreadyDead;
    public void SetHealth()
    {
        gold = startGold;
        currentHealth = maxHealth;
        healthPercentage = currentHealth / maxHealth;
    }
    public void DamagePlayer(int damages)
    {
        if(invincible == false && itsAlreadyDead == false)
        {
            timerIndicator = indicatorShowTime;
            UIManager.Instance.damageIndicatorPanel.SetActive(true);
            currentHealth -= damages;
            healthPercentage = currentHealth / maxHealth;
            GameManager.Instance.mainCam.GetComponent<CameraController>().shake = true;
            UIManager.Instance.OpenHealthBar();
        }
        if(itsAlreadyDead == false && currentHealth <= 0)
        {
            GameManager.Instance.PlayerDeath();
            itsAlreadyDead = true;
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }
    public void Pay(int amount)
    {
        gold -= amount;
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        healthPercentage = currentHealth / maxHealth;
    }

    public void Invincibility(bool switchInvinsibility)
    {

        switch (switchInvinsibility)
        {
            case true:
                if (invincible == false)
                    invincible = true;
                break;
            case false:
                if (invincible == true)
                    invincible = false;
                break;
        }
    }
}
