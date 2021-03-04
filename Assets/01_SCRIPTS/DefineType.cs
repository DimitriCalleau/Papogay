using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaitType { PaperBoy, FruitBox, Sign, MarketStand, Perfume, Militant, Bar, Bus}
public enum FirmeType { Carrouf, FastRegal, Sinpharo, NormalGym, Banana, Amazoon }
public enum FirmeSize { Small, Medium, Big }
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