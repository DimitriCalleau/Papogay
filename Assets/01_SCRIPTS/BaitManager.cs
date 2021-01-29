using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitManager : MonoBehaviour
{
    Location closestLocation;
    Location selectedLocation;

    int baitRotation;

    public GameObject preview;

    void FixedUpdate()
    {
        float minDist = Mathf.Infinity;
        for (int i = 0; i < GameManager.Instance.allLocations.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, GameManager.Instance.allLocations[i].transform.position);
            if(dist <= minDist)
            {
                minDist = dist;
                closestLocation = GameManager.Instance.allLocations[i];
                if(closestLocation.occupied == false)
                {
                    selectedLocation = closestLocation;
                }
            }
        }
        MovePreview(selectedLocation, baitRotation);
    }
    void PlaceTrap()
    {
        GameObject inventorySelection = UIManager.Instance.inventory.selection;
        int playerGold = GameManager.Instance.playerStats.gold;

        if (playerGold > UIManager.Instance.inventory.selection.GetComponent<Baits>().currentCost && inventorySelection != null)
        {
            GameObject bait = GameObject.Instantiate(inventorySelection, selectedLocation.transform.position, Quaternion.Euler(0, baitRotation, 0));
            bait.GetComponent<Baits>().location = selectedLocation;
            bait.GetComponent<Baits>().InitBait();
            selectedLocation.occupied = true;
            GameManager.Instance.playerStats.Pay(inventorySelection.GetComponent<Baits>().currentCost);
        }
        else
            Debug.Log("not enough gold");
    }

    void MovePreview(Location location, int rotation)
    {
        if(location != null)
        {
            preview.transform.position = location.transform.position;
            preview.transform.Rotate(new Vector3(0, rotation, 0));
        }
    }

    public void RotateBait(bool rotationDirection)
    {
        switch (rotationDirection)
        {
            case true:
                baitRotation += 90;
                if (baitRotation > 360)
                {
                    baitRotation = 90;
                }
                break;
            case false:
                baitRotation -= 90;
                if (baitRotation < 0)
                {
                    baitRotation = 270;
                }
                break;
        }
    }
    //Security Events
    void OnEnable()
    {
        if(InputEvents.Instance !=null)
            InputEvents.Instance.OnPlace += PlaceTrap;
    }
    void OnDisable()
    {
        if (InputEvents.Instance != null)
            InputEvents.Instance.OnPlace -= PlaceTrap;
    }
}
