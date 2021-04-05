using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Militant, Bar, Bus}
public enum FirmeType { Carrouf, FastRegal, Sinpharo, NormalGym, Banana, Amazoon }
public enum BuildingSize { Small, Big }
public enum EntityStatus { Neutral, Enemy, Ally }
public enum LocationState {Occupied, NoBait, Free}
public enum BusState { isTraveling, isArriving, isGoing, isWaiting, isSpawning }
public class DefineType
{
    //bait
    public void ChangeBaitType(BaitType type)
    {
        BaitType switchBait = type;

        switch (switchBait)
        {
            case BaitType.PaperBoy:
                break;
            case BaitType.FruitBox:
                break;
            case BaitType.Sign:
                break;
            case BaitType.MarketStand:
                break;
            case BaitType.Perfume:
                break;
            case BaitType.Militant:
                break;
            case BaitType.Bar:
                break;
            case BaitType.Bus:
                break;
        }
    }
}