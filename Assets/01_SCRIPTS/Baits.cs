using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baits : MonoBehaviour
{
    #region Stats
    public BaitTypes type;
    public int upgradeIndex;
    public Location location;

    [Header("baitDuration")]
    float usure;
    float currentUsureMax;
    public List<float> usureMax;
    public float usurePercentage;

    [Header("Costs")]
    public List<int> costs;
    public int currentCost;

    [Header("Damages")]
    public int damages;
    public float cooldown;
    float countdown;
    public LayerMask ennemisMask = -1;
    #endregion

    [Header("UI")]
    public List<Sprite> ui_Sprites;

    public Baits()
    {

    }

    public void InitBait()
    {
        this.upgradeIndex = 0;
        this.currentUsureMax = this.usureMax[upgradeIndex];
        this.usurePercentage = this.currentUsureMax;
        this.currentCost = costs[upgradeIndex];
    }

    public void Upgrade()
    {
        this.upgradeIndex += 1;
        this.currentCost = costs[upgradeIndex];
        this.usure += this.usure * (this.usureMax[this.upgradeIndex] / this.currentUsureMax);
        this.currentUsureMax = this.usureMax[this.upgradeIndex];
    }

    public void LoseLife(int damage)
    {
        this.usure -= damages;
        this.usurePercentage = this.usure / this.currentUsureMax;

        if(this.usure <= 0)
        {
            location.occupied = false;
            Destroy(this.gameObject);
        }
    }
}