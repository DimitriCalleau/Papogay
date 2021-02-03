using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitManager
{
    public int baitRotation;
    public float cooldownTimer;
    public void PlaceTrap()
    {
        Slot inventorySelection = UIManager.Instance.inventory.selection;
        if (cooldownTimer <= 0)
        {
            if (inventorySelection.nbBaits > 0 && inventorySelection != null)
            {
                GameObject bait = GameObject.Instantiate(inventorySelection.baitPrefab, UIManager.Instance.selectedLocation.transform.position, Quaternion.Euler(0, baitRotation, 0));
                bait.GetComponent<Baits>().InitBait();
                bait.GetComponent<Baits>().location = UIManager.Instance.selectedLocation;
                UIManager.Instance.selectedLocation.occupied = true;
                inventorySelection.AddRemove(false);
                cooldownTimer = UIManager.Instance.timeBetweenBaits;
            }
            else
                Debug.Log("not enough gold");
        }
    }

    public void MovePreview(Location location, int rotation)
    {
        if(UIManager.Instance.inventory.selection != null)
        {
            UIManager.Instance.preview.GetComponent<MeshFilter>().mesh = UIManager.Instance.inventory.selection.baitPrefab.GetComponent<MeshFilter>().sharedMesh;
        }
        if (location != null)
        {
            UIManager.Instance.preview.transform.position = location.transform.position;
            UIManager.Instance.preview.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void RotateSelectedBait(Vector2 _whichDirection)
    {
        if (_whichDirection.y > 0)
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
        Debug.Log(baitRotation);
    }
}