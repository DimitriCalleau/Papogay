using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Slot
{
    public BaitType type;
    public GameObject baitPrefab;
    public int nbBaits;
    public int currentCost;
    public Sprite currentUIImage;
    public GameObject thisInventorySlot;
    public GameObject thisShopSlot;

    public Slot(int amount, BaitType baitType)
    {
        baitPrefab = UIManager.Instance.PickBait(baitType);
        nbBaits = amount;
        type = baitType;

        Baits thisBait = baitPrefab.GetComponent<Baits>();
        thisBait.InitBait();
        currentCost = thisBait.currentCost;
        currentUIImage = thisBait.ui_Sprites[thisBait.upgradeIndex];
        thisInventorySlot = GameObject.Instantiate(UIManager.Instance.inventorySlotPrefab);
        thisInventorySlot.transform.SetParent(UIManager.Instance.inventoryPanel.transform);

        thisShopSlot = GameObject.Instantiate(UIManager.Instance.shopSlotPrefab);
        thisShopSlot.transform.SetParent(UIManager.Instance.shopPanel.transform);
        thisShopSlot.GetComponent<Button>().onClick.AddListener(this.BuyBait);

        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        thisInventorySlot.GetComponent<Image>().sprite = currentUIImage;
        thisInventorySlot.GetComponentInChildren<Text>().text = nbBaits.ToString();
        thisShopSlot.GetComponent<Image>().sprite = currentUIImage;
        thisShopSlot.GetComponentInChildren<Text>().text = currentCost.ToString();
    }
    public void UpgradeSlotBait()
    {
        Baits thisBait = baitPrefab.GetComponent<Baits>();
        thisBait.Upgrade();
        currentCost = thisBait.currentCost;
        currentUIImage = thisBait.ui_Sprites[thisBait.upgradeIndex];
        UpdateDisplay();
    }
    public void BuyBait()
    {
        Debug.Log("buy : " + this.baitPrefab.name);
        if (currentCost <= GameManager.Instance.playerStats.gold)
        {
            AddRemove(true);
            GameManager.Instance.playerStats.Pay(currentCost);
        }
    }
    public void AddRemove(bool addRemove)
    {
        switch (addRemove)
        {
            case false:
                nbBaits -= 1;
                UpdateDisplay();
                break;            
            case true:
                nbBaits += 1;
                UpdateDisplay();
                break;
        }
    }

    public void SelectDeselect(bool selection)
    {
        switch (selection)
        {
            case false:
                nbBaits -= 1;
                UpdateDisplay();
                break;
            case true:
                nbBaits += 1;
                UpdateDisplay();
                break;
        }
    }
}