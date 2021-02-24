﻿using System.Collections;
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
    public bool invincible;
    public float invincibilityTime;
    public float speed;
    public float rollSpeed;
    public float boostFactor;

    public void DamagePlayer(int damages)
    {
        if(invincible == false)
        {
            currentHealth -= damages;
            healthPercentage = currentHealth / maxHealth;
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
