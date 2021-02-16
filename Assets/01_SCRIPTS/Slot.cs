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

    public void UpdatePreviewMesh()
    {
        if(UIManager.Instance.selectedLocation != null)
        {
            switch (type)
            {
                case BaitType.PaperBoy:
                    if (baitPrefab.GetComponent<PaperBoy>().colliderCenter != null)
                    {
                        Debug.Log("paperboy");
                        baitPrefab.GetComponent<PaperBoy>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<PaperBoy>().colliderCenter, baitPrefab.GetComponent<PaperBoy>().detectionRange);
                    }
                    break;
                case BaitType.FruitBox:
                    if (baitPrefab.GetComponent<FruitBox>().colliderCenter != null)
                    {
                        Debug.Log("fruitbox");
                        baitPrefab.GetComponent<FruitBox>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<FruitBox>().colliderCenter, baitPrefab.GetComponent<FruitBox>().rotatedColliderSize);
                    }
                    break;
                case BaitType.Sign:
                    if (baitPrefab.GetComponent<Sign>().colliderCenter != null)
                    {
                        Debug.Log("sign");
                        baitPrefab.GetComponent<Sign>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Sign>().colliderCenter, baitPrefab.GetComponent<Sign>().rotatedColliderSize);
                    }
                    break;
                case BaitType.MarketStand:
                    if (baitPrefab.GetComponent<MarketStand>().colliderCenter != null)
                    {
                        Debug.Log("marketstand");
                        baitPrefab.GetComponent<MarketStand>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<MarketStand>().colliderCenter, baitPrefab.GetComponent<MarketStand>().rotatedColliderSize);
                    }
                    break;
                case BaitType.Perfume:
                    if (baitPrefab.GetComponent<Perfume>().colliderCenter != null)
                    {
                        Debug.Log("perfume");
                        baitPrefab.GetComponent<Perfume>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Perfume>().colliderCenter, baitPrefab.GetComponent<Perfume>().range);
                    }
                    break;
                case BaitType.Antenna:
                    if (baitPrefab.GetComponent<Antenna>().colliderCenter != null)
                    {
                        Debug.Log("antenna");
                        baitPrefab.GetComponent<Antenna>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Antenna>().colliderCenter, baitPrefab.GetComponent<Antenna>().range);
                    }
                    break;
                case BaitType.Bar:
                    if (baitPrefab.GetComponent<Bar>().colliderCenter != null)
                    {
                        Debug.Log("bar");
                        baitPrefab.GetComponent<Bar>().SetCollider();
                        UIManager.Instance.preview.SphereRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Bar>().colliderCenter, baitPrefab.GetComponent<Bar>().range);
                    }
                    break;
                case BaitType.Threadmill:
                    if (baitPrefab.GetComponent<Treadmill>().colliderCenter != null)
                    {
                        Debug.Log("treadmill");
                        baitPrefab.GetComponent<Treadmill>().SetCollider();
                        UIManager.Instance.preview.BoxRangeDisplayer(UIManager.Instance.selectedLocation.transform.position + baitPrefab.GetComponent<Treadmill>().colliderCenter, baitPrefab.GetComponent<Treadmill>().rotatedColliderSize);
                    }
                    break;
            }
        }

    }
}