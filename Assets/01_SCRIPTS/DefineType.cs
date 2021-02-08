using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bait
public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Antenna, Bar, Threadmill, Iphone }
//entity
public enum EntityStatus { Neutral, Enemy, Ally }

public class DefineType
{
    public GameObject enm;

    //bait
    public void ChangeBaitType(BaitType type)
    {
        BaitType switchBait = type;

        switch (switchBait)
        {
            case BaitType.PaperBoy:
                Debug.Log("PaperBoy  Bait");
                break;
            case BaitType.FruitBox:
                Debug.Log("FruitBox  Bait");
                break;
            case BaitType.Sign:
                Debug.Log("Sign  Bait");
                break;
            case BaitType.MarketStand:
                Debug.Log("MarketStand  Bait");
                break;
            case BaitType.Perfume:
                Debug.Log("Perfume  Bait");
                break;
            case BaitType.Antenna:
                Debug.Log("Antenna Bait");
                break;
            case BaitType.Bar:
                Debug.Log("Bar Bait");
                break;
            case BaitType.Threadmill:
                Debug.Log("Threadmill Bait");
                break;
        }
    }
    //entity
    public void ChangeEntityStatus(EntityStatus status)
    {
        EntityStatus switchStatus = status;

        switch (switchStatus)
        {
            case EntityStatus.Neutral:
                Debug.Log("Neutral status");
                break;
            case EntityStatus.Enemy:
                Debug.Log("Enemy status");
                break;
            case EntityStatus.Ally:
                Debug.Log("Ally status");
                break;
        }
    }
}