using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bait
public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Antenna, Bar, Threadmill, Iphone }
//entity
public enum EntityStatus { Neutral, Enemy, Ally }

public class DefineType
{
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
}