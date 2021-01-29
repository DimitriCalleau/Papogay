using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Reward
{
    public GameObject selectedReward;
    public List<BaitTypes> loots;

    public void AddFirstTraps()
    {
        AddOrUpgradeBait(GameManager.Instance.allBaits[0], BaitTypes.PaperBoy, 10);
        AddOrUpgradeBait(GameManager.Instance.allBaits[1], BaitTypes.FruitBox, 10);
        AddOrUpgradeBait(GameManager.Instance.allBaits[2], BaitTypes.Sign, 10);
        GameManager.Instance.EventStartGame();
    }

    public void RewardSelection()
    {
        for (int i = 0; i < GameManager.Instance.allBaits.Count; i++)
        {
            if (GameManager.Instance.allBaits[i].GetComponent<Baits>().type == loots[loots.Count - 1])
            {
                selectedReward = GameManager.Instance.allBaits[i];
                break;
            }
        }
    }
    public void AddOrUpgradeBait(GameObject baitPrefab, BaitTypes typeOfBait, int baitAmount)
    {
        bool upgraded = false;
        if (UIManager.Instance.allCurrentBaits.Count > 0)
        {
            for (int i = 0; i < UIManager.Instance.allCurrentBaits.Count; i++)
            {
                if (UIManager.Instance.allCurrentBaits[i].type == typeOfBait)
                {
                    UIManager.Instance.allCurrentBaits[i].baitPrefab.GetComponent<Baits>().upgradeIndex += 1;
                    upgraded = true;
                    break;
                }
            }
        }
        if (upgraded == false)
        {
            Slot newSlot = new Slot(baitPrefab, baitAmount, typeOfBait);
            UIManager.Instance.allCurrentBaits.Add(newSlot);
        }
    }
    public void AddReward()
    {
        RewardSelection();
        AddOrUpgradeBait(selectedReward, loots[loots.Count - 1], 10);
        loots.Clear();
        GameManager.Instance.EventStartWave();
    }
    public void AddLootType(BaitTypes firmeType)
    {
        loots.Add(firmeType);
    }
}
