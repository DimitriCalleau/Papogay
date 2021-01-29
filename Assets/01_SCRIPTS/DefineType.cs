using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bait
public enum BaitTypes { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Antenna, Bar, Threadmill, Iphone }
//entity
public enum EntityStatus { Neutral, Enemy, Ally }
//corporation
//public enum CorpoType { Perfumery = BaitType.Perfume, Grossery = BaitType.MarketStand, Food = BaitType.Bar, Sport = BaitType.Threadmill, Tech = BaitType.Antenna, Delivery }

public class DefineType
{
    //bait
    public void ChangeBaitType(BaitTypes type)
    {
        BaitTypes switchBait = type;

        switch (switchBait)
        {
            case BaitTypes.PaperBoy:
                Debug.Log("PaperBoy  Bait");
                break;
            case BaitTypes.FruitBox:
                Debug.Log("FruitBox  Bait");
                break;
            case BaitTypes.Sign:
                Debug.Log("Sign  Bait");
                break;
            case BaitTypes.MarketStand:
                Debug.Log("MarketStand  Bait");
                break;
            case BaitTypes.Perfume:
                Debug.Log("Perfume  Bait");
                break;
            case BaitTypes.Antenna:
                Debug.Log("Antenna Bait");
                break;
            case BaitTypes.Bar:
                Debug.Log("Bar Bait");
                break;
            case BaitTypes.Threadmill:
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
    //Corporation
    /*public void ChangeFirmeType(CorpoType type)
    {
        CorpoType switchCorpo = type;

        switch (switchCorpo)
        {
            case CorpoType.Perfumery:
                Debug.Log("Perfumery  Type");
                break;
            case CorpoType.Grossery:
                Debug.Log("Grossery  Type");
                break;
            case CorpoType.Food:
                Debug.Log("Food  Type");
                break;
            case CorpoType.Sport:
                Debug.Log("Sport  Type");
                break;
            case CorpoType.Tech:
                Debug.Log("Tech  Type");
                break;
            case CorpoType.Delivery:
                Debug.Log("Delivery Type");
                break;
        }
    }*/
}