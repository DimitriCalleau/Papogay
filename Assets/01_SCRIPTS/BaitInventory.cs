using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitInventory
{
    public Slot selection;
    public int selectionIndex;
    public void SwitchBaitSelection(bool rightOrLeft)
    {
        if (UIManager.Instance.inventoryOpened == true)
        {
            Debug.Log("tryed to select");

            switch (rightOrLeft)
            {
                case true:
                    selectionIndex += 1;
                    if (selectionIndex == UIManager.Instance.allCurrentBaits.Count)
                    {
                        selectionIndex = 0;
                    }
                    break;
                case false:
                    selectionIndex -= 1;
                    if (selectionIndex == -1)
                    {
                        selectionIndex = UIManager.Instance.allCurrentBaits.Count - 1;
                    }
                    break;
            }
        }
        if (UIManager.Instance.allCurrentBaits.Count > 0)
        {
            selection = UIManager.Instance.allCurrentBaits[selectionIndex];
        }
    }

    public void OpenInventory()
    {
        if(GameManager.Instance.gameState.start == true && GameManager.Instance.gameState.pause == false)
        {
            UIManager.Instance.inventoryOpened = !UIManager.Instance.inventoryOpened;
            switch (UIManager.Instance.inventoryOpened)
            {
                case true:
                    UIManager.Instance.inventoryPanel.SetActive(true);
                    break;
                case false:
                    UIManager.Instance.inventoryPanel.SetActive(false);
                    break;
            }
        }
    }
}