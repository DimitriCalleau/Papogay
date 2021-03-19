using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitManager
{
    public int baitRotation;
    public float cooldownTimer;
    public void PlaceTrap()
    {
        if (UIManager.Instance.inventoryOpened == true)
        {
            Slot inventorySelection = UIManager.Instance.inventory.selection;
            if (cooldownTimer <= 0)
            {
                if (inventorySelection != null && UIManager.Instance.selectedLocation != null && UIManager.Instance.selectedLocation.cantReceiveBait == false && inventorySelection.nbBaits > 0 && UIManager.Instance.selectedLocation.occupied == false)
                {
                    GameManager.Instance.player.GetComponent<PlayerMovementController>().playerAnimator.SetTrigger("Pose");
                    GameObject baitSpawner = GameObject.Instantiate(UIManager.Instance.baitSpawnerPrefab, UIManager.Instance.selectedLocation.transform.position, Quaternion.Euler(0, baitRotation, 0));
                    baitSpawner.GetComponent<BaitSpawner>().InitSpawn(inventorySelection.baitPrefab, UIManager.Instance.selectedLocation);
                    UIManager.Instance.selectedLocation.occupied = true;
                    inventorySelection.AddRemove(false);
                    cooldownTimer = UIManager.Instance.timeBetweenBaits;
                }
            }
        }
    }

    public void RotateSelectedBait(bool _whichDirection)
    {
        if (UIManager.Instance.inventoryOpened == true)
        {
            if (_whichDirection == true)
            {
                baitRotation += 90;
                if (baitRotation > 360)
                {
                    baitRotation = 90;
                }
            }
            else
            {
                baitRotation -= 90;
                if (baitRotation < 0)
                {
                    baitRotation = 270;
                }
            }
            UIManager.Instance.inventory.selection.UpdatePreviewMesh();
        }
    }
}