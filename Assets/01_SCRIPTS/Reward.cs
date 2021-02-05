using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Reward
{
    public GameObject selectedReward;
    public List<BaitType> loots;

    public void RewardSelection()
    {
        for (int i = 0; i < UIManager.Instance.allBaits.Count; i++)
        {
            if (UIManager.Instance.allBaits[i].GetComponent<Baits>().type == loots[loots.Count - 1])
            {
                selectedReward = UIManager.Instance.allBaits[i];
                break;
            }
        }
    }
    public void AddOrUpgradeBait(GameObject baitPrefab, BaitType typeOfBait, int baitAmount)
    {
    
        bool upgraded = false;
        if (UIManager.Instance.allCurrentBaits.Count > 0)
        {
            for (int i = 0; i < UIManager.Instance.allCurrentBaits.Count; i++)
            {
                if (UIManager.Instance.allCurrentBaits[i].type == typeOfBait)
                {
                    UIManager.Instance.allCurrentBaits[i].UpgradeSlotBait();
                    upgraded = true;
                    break;
                }
            }
        }
        if (upgraded == false)
        {
            Slot newSlot = new Slot(baitPrefab, baitAmount, typeOfBait);
            UIManager.Instance.allCurrentBaits.Add(newSlot);
            Debug.Log("test");
        }
    }

    public void AddLootType(BaitType firmeType)
    {
        loots.Add(firmeType);
    }
}
