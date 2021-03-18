using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitInventory
{
    public Slot selection, oldSelection;
    public int selectionIndex;
    public void SwitchBaitSelection(Vector2 rightOrLeft)
    {
        if (UIManager.Instance.inventoryOpened == true)
        {
            if (UIManager.Instance.allCurrentBaits.Count > 0)
            {
                oldSelection = UIManager.Instance.allCurrentBaits[selectionIndex];
            }
            if (rightOrLeft.y > 0)
            {
                selectionIndex += 1;
                if (selectionIndex == UIManager.Instance.allCurrentBaits.Count)
                {
                    selectionIndex = 0;
                }
            }
            if (rightOrLeft.y < 0)
            {
                selectionIndex -= 1;
                if (selectionIndex == -1)
                {
                    selectionIndex = UIManager.Instance.allCurrentBaits.Count - 1;
                }
            }
        }
        if (UIManager.Instance.allCurrentBaits.Count > 0)
        {
            selection = UIManager.Instance.allCurrentBaits[selectionIndex];
            selection.inventorySlotAnimator.SetBool("Selected", true);
            if (oldSelection != null)
            {
                oldSelection.inventorySlotAnimator.SetBool("Selected", false);
            }
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
                    UIManager.Instance.preview.HidePreview(true);
                    break;
                case false:
                    UIManager.Instance.inventoryPanel.SetActive(false);
                    UIManager.Instance.preview.HidePreview(false);
                    break;
            }
        }
    }
}