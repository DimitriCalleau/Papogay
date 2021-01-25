using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bait
public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Antenna, Bar, Threadmill }
//entity
public enum EntityStatus { Neutral, Enemy, Ally }
//corporation
public enum CorpoType { Perfumery, Grossery, Food, Sport, Tech, Delivery }

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
    public void ChangeFirmeType(CorpoType type)
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
    }
}