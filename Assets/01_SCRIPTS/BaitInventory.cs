using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitInventory
{
    public GameObject selection;
    public List<Slot> allCurrentBaits;

    public void AddRemovebaits(bool addOrRemove, int index)
    {
        switch (addOrRemove)
        {
            case true :
                allCurrentBaits[index].nbBaits += 1;
                break;
            case false :
                allCurrentBaits[index].nbBaits -= 1;
                break;
        }
    }

    public void UpgradeBait(int index)
    {
        //allCurrentBaits[index].baitPrefab.GetComponent<Bait>().Upgrade();
    }
        
    public void AddNewBait(GameObject baitPrefab, int index, int baitAmount)
    {
        Slot slot = new Slot(baitPrefab, baitAmount);
        allCurrentBaits.Insert(index, slot);
    }        
}
