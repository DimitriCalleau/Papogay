using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Baits : MonoBehaviour
{
    #region Stats
    public BaitType type;
    public int upgradeIndex;
    [HideInInspector] public Location location;
    [HideInInspector] public Vector3 colliderCenter;
    public float offsetHeightCollider, offSetForwardCollider;

    [Header("baitDuration")]
    public float usure;
    public float currentUsureMax;
    public List<float> usureMax;
    public float usurePercentage;

    [Header("Costs")]
    public List<int> costs;
    public int currentCost;

    [Header("UI")]
    public List<Sprite> ui_Sprites;
    public Image ui_healthBar;
    public TextMeshProUGUI ui_UsureAmountText;

    [Header("Damages")]
    public List<int> damages;
    public List<float> cooldown;
    public float countdown;
    public LayerMask ennemisMask = -1;
    #endregion


    public Baits()
    {

    }

    public void InitBait()
    {
        this.currentUsureMax = this.usureMax[upgradeIndex];
        this.usure = this.currentUsureMax;
        this.usurePercentage = this.currentUsureMax;
        this.currentCost = costs[upgradeIndex];
        this.ui_UsureAmountText.text = this.usure.ToString();
        this.ui_healthBar.fillAmount = this.usurePercentage;
        if (this.cooldown.Count > 0)
        {
            this.countdown = this.cooldown[this.upgradeIndex];
        }
    }

    public void Upgrade()
    {
        this.upgradeIndex += 1;
        this.currentCost = costs[upgradeIndex];
        this.usure += this.usure * (this.usureMax[this.upgradeIndex] / this.currentUsureMax);
        this.currentUsureMax = this.usureMax[this.upgradeIndex];
    }

    public void LoseLife(float damage)
    {
        this.usure -= damage;
        this.usurePercentage = this.usure / this.currentUsureMax;
        this.ui_UsureAmountText.text = Mathf.CeilToInt(this.usure).ToString();
        this.ui_healthBar.fillAmount = this.usurePercentage;

        if (this.usure <= 0)
        {
            location.occupied = false;
            Destroy(this.gameObject);
        }
    }
}