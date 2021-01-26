using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward
{
    public List<GameObject> allBaits;
    public List<BaitType> loots;

    public void AddFirstTraps()
    {

    }

    public void RewardSelection()
    {

    }

    public void AddReward()
    {

    }
    public void AddLootType(BaitType firmeType)
    {
        loots.Add(firmeType);
    }

    public void ClearLoots()
    {
        loots.Clear();
    }
}
