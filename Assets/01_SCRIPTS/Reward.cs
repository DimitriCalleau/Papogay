using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Reward
{
    public List<BaitType> loots;
    public void AddBait(BaitType typeOfBait, int baitAmount)
    {
        Slot newSlot = new Slot(baitAmount, typeOfBait);
        UIManager.Instance.allCurrentBaits.Add(newSlot);
        UIManager.Instance.inventory.SwitchBaitSelection(true);
    }
}
