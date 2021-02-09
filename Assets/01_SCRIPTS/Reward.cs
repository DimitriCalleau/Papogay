using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Reward
{
    public List<BaitType> loots;
    public void AddOrUpgradeBait(BaitType typeOfBait, int baitAmount)
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
            Slot newSlot = new Slot(baitAmount, typeOfBait);
            UIManager.Instance.allCurrentBaits.Add(newSlot);
        }
        UIManager.Instance.inventory.SwitchBaitSelection(true);
    }

    public void AddLootType(BaitType firmeType)
    {
        loots.Add(firmeType);
    }
}
