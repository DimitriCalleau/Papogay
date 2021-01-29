using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public void BuyBait(BaitTypes typeOfBait)
    {
        Slot slotSelection = null;

        for (int i = 0; i < UIManager.Instance.allCurrentBaits.Count; i++)
        {
            if (UIManager.Instance.allCurrentBaits[i].type == typeOfBait)
            {
                slotSelection = UIManager.Instance.allCurrentBaits[i];
                break;
            }
        }
        if (slotSelection != null)
        {
            if (slotSelection.currentCost >= GameManager.Instance.playerStats.gold)
            {
                slotSelection.AddRemove(true);
                GameManager.Instance.playerStats.Pay(slotSelection.currentCost);
            }
        }
    }
}
