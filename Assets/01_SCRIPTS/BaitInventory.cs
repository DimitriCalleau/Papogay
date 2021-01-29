using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitInventory
{
    public GameObject selection;
    public int selectionIndex;
    public void SwitchBaitSelection(bool rightOrLeft)
    {
        Debug.Log(selectionIndex);
        switch (rightOrLeft)
        {
            case true:
                if(selectionIndex == UIManager.Instance.allCurrentBaits.Count)
                {
                    selectionIndex = 0;
                }
                else
                {
                    selectionIndex += 1;
                }
                break;
            case false:
                if (selectionIndex == 0)
                {
                    selectionIndex = UIManager.Instance.allCurrentBaits.Count;
                }
                else
                {
                    selectionIndex -= 1;
                }
                break;
        }
        if(UIManager.Instance.allCurrentBaits.Count > 0)
        {
            selection = UIManager.Instance.allCurrentBaits[selectionIndex].baitPrefab;
            UIManager.Instance.selectionTruc.transform.position = selection.transform.position;
        }
    }
}
