using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Reward
{
    public List<BaitType> loots;
    public List<int> goldReward;
    public void AddBait(BaitType typeOfBait, int baitAmount, int _goldReward)
    {
        Slot newSlot = new Slot(baitAmount, typeOfBait);
        UIManager.Instance.allCurrentBaits.Add(newSlot);
        GameManager.Instance.playerStats.gold += _goldReward;
        UIManager.Instance.inventory.SwitchBaitSelection(true);
    }
}
