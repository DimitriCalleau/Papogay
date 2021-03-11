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
    public int upgradeCost;
    public Sprite currentUIImage;
    public Sprite currentUpgradeImage;
    public GameObject thisInventorySlot;
    public GameObject thisShopSlot;
    public GameObject thisUpgradeSlot;

    public Slot(int amount, BaitType baitType)
    {
        baitPrefab = UIManager.Instance.PickBait(baitType);
        nbBaits = amount;
        type = baitType;

        Baits thisBait = baitPrefab.GetComponent<Baits>();
        thisBait.InitBait();
        currentCost = thisBait.currentCost;
        upgradeCost = thisBait.currentUpgradeCost;
        currentUIImage = thisBait.ui_Sprites[thisBait.upgradeIndex]; 
        if (thisBait.upgradeIndex < thisBait.nbUpgradeMax - 1)
        {
            currentUpgradeImage = thisBait.ui_Sprites[thisBait.upgradeIndex + 1];
        }
        thisInventorySlot = GameObject.Instantiate(UIManager.Instance.inventorySlotPrefab);
        thisInventorySlot.transform.SetParent(UIManager.Instance.inventoryPanel.transform);

        //Slot pour acheter des appats
        thisShopSlot = GameObject.Instantiate(UIManager.Instance.shopSlotPrefab);
        thisShopSlot.transform.SetParent(UIManager.Instance.shopLayout.transform);
        thisShopSlot.GetComponent<Button>().onClick.AddListener(this.BuyBait);

        //Slot pour upgrade des appats
        thisUpgradeSlot = GameObject.Instantiate(UIManager.Instance.upgradeSlotPrefab);
        thisUpgradeSlot.transform.SetParent(UIManager.Instance.upgradeLayout.transform);
        thisUpgradeSlot.GetComponent<Button>().onClick.AddListener(this.UpgradeSlotBait);

        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        thisInventorySlot.GetComponent<Image>().sprite = currentUIImage;
        thisInventorySlot.GetComponentInChildren<Text>().text = nbBaits.ToString();
        thisShopSlot.GetComponent<Image>().sprite = currentUIImage;
        thisShopSlot.GetComponentInChildren<Text>().text = currentCost.ToString();
        thisUpgradeSlot.GetComponent<Image>().sprite = currentUpgradeImage;
        thisUpgradeSlot.GetComponentInChildren<Text>().text = upgradeCost.ToString();
    }
    public void UpgradeSlotBait()
    {
        Baits thisBait = baitPrefab.GetComponent<Baits>();
        if (upgradeCost <= GameManager.Instance.playerStats.gold && thisBait.upgradeIndex < thisBait.nbUpgradeMax - 1)
        {
            thisBait.Upgrade();
            currentCost = thisBait.currentCost;
            upgradeCost = thisBait.currentUpgradeCost;
            currentUIImage = thisBait.ui_Sprites[thisBait.upgradeIndex];
            if (thisBait.upgradeIndex < thisBait.nbUpgradeMax - 1)
            {
                currentUpgradeImage = thisBait.ui_Sprites[thisBait.upgradeIndex + 1];
            }
            GameManager.Instance.playerStats.Pay(upgradeCost);
            UpdateDisplay();
        }
    }
    public void BuyBait()
    {
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

    /*public void UpdatePreviewMesh()
    {
        if(UIManager.Instance.selectedLocation != null)
        {
            switch (type)
            {
                case BaitType.PaperBoy:
                    if (baitPrefab.GetComponent<PaperBoy>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<PaperBoy>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<PaperBoy>().colliderCenter, baitPrefab.GetComponent<PaperBoy>().detectionRange);
                    }
                    break;
                case BaitType.FruitBox:
                    if (baitPrefab.GetComponent<FruitBox>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<FruitBox>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<FruitBox>().colliderCenter, baitPrefab.GetComponent<FruitBox>().rotatedColliderSize);
                    }
                    break;
                case BaitType.Sign:
                    if (baitPrefab.GetComponent<Sign>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Sign>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Sign>().colliderCenter, baitPrefab.GetComponent<Sign>().rotatedColliderSize);
                    }
                    break;
                case BaitType.MarketStand:
                    if (baitPrefab.GetComponent<MarketStand>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<MarketStand>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<MarketStand>().colliderCenter, baitPrefab.GetComponent<MarketStand>().rotatedColliderSize);
                    }
                    break;
                case BaitType.Perfume:
                    if (baitPrefab.GetComponent<Perfume>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Perfume>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Perfume>().colliderCenter, baitPrefab.GetComponent<Perfume>().range);
                    }
                    break;
                case BaitType.Militant:
                    if (baitPrefab.GetComponent<Militant>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Militant>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Militant>().colliderCenter, baitPrefab.GetComponent<Militant>().range);
                    }
                    break;
                case BaitType.Bar:
                    if (baitPrefab.GetComponent<Bar>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Bar>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Bar>().colliderCenter, baitPrefab.GetComponent<Bar>().range);
                    }
                    break;
                case BaitType.Bus:
                    if (baitPrefab.GetComponent<Bus>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Bus>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Bus>().colliderCenter, baitPrefab.GetComponent<Bus>().range);
                    }
                    break;
            }
        }
    }*/

    public void UpdatePreviewMesh()
    {
        if (UIManager.Instance.selectedLocation != null)
        {
            switch (type)
            {
                case BaitType.PaperBoy:
                    if (baitPrefab.GetComponent<PaperBoy>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<PaperBoy>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position, Vector3.one * baitPrefab.GetComponent<PaperBoy>().detectionRange, true);
                    }
                    break;
                case BaitType.FruitBox:
                    if (baitPrefab.GetComponent<FruitBox>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<FruitBox>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<FruitBox>().colliderCenter, baitPrefab.GetComponent<FruitBox>().rotatedColliderSize, false);
                    }
                    break;
                case BaitType.Sign:
                    if (baitPrefab.GetComponent<Sign>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Sign>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Sign>().colliderCenter, baitPrefab.GetComponent<Sign>().rotatedColliderSize, false);
                    }
                    break;
                case BaitType.MarketStand:
                    if (baitPrefab.GetComponent<MarketStand>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<MarketStand>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<MarketStand>().colliderCenter, baitPrefab.GetComponent<MarketStand>().rotatedColliderSize, false);
                    }
                    break;
                case BaitType.Perfume:
                    if (baitPrefab.GetComponent<Perfume>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Perfume>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position, Vector3.one * baitPrefab.GetComponent<Perfume>().range, true);
                    }
                    break;
                case BaitType.Militant:
                    if (baitPrefab.GetComponent<Militant>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Militant>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position, Vector3.one * baitPrefab.GetComponent<Militant>().range, true);
                    }
                    break;
                case BaitType.Bar:
                    if (baitPrefab.GetComponent<Bar>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Bar>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position, Vector3.one * baitPrefab.GetComponent<Bar>().range, true);
                    }
                    break;
                case BaitType.Bus:
                    if (baitPrefab.GetComponent<Bus>().colliderCenter != null)
                    {
                        baitPrefab.GetComponent<Bus>().SetCollider();
                        UIManager.Instance.preview.RangeDisplayer(UIManager.Instance.selectedLocation.transform.position, Vector3.one * baitPrefab.GetComponent<Bus>().range, true);
                    }
                    break;
            }
        }

    }
}