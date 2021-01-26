using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public GameObject baitPrefab;
    public int nbBaits;

    public Slot(GameObject bait, int amount)
    {
        baitPrefab = bait;
        nbBaits = amount;
    }
}
