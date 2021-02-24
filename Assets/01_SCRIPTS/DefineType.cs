using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bait
public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Antenna, Bar, Threadmill, Amazoon }
//entity
public enum EntityStatus { Neutral, Enemy, Ally }

//useless mais stylé
public class DefineType
{
    //bait
    public void ChangeBaitType(BaitType type)
    {
        BaitType switchBait = type;

        switch (switchBait)
        {
            case BaitType.PaperBoy:
                Debug.Log("PaperBoy");
                break;
            case BaitType.FruitBox:
                Debug.Log("FruitBox");
                break;
            case BaitType.Sign:
                Debug.Log("Sign");
                break;
            case BaitType.MarketStand:
                Debug.Log("MarketStand / carrefour");
                break;
            case BaitType.Perfume:
                Debug.Log("Perfume / sephora");
                break;
            case BaitType.Antenna:
                Debug.Log("Antenna / apple");
                break;
            case BaitType.Bar:
                Debug.Log("Bar / mc donald");
                break;
            case BaitType.Threadmill:
                Debug.Log("Threadmill / basic thicc");
                break;
        }
    }
}