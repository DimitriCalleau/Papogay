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
                    Debug.Log(selectedLocation.name);
                }
            }
        }
    }
    void Update()
    {
        MovePreview(selectedLocation, baitRotation);
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlaceTrap(GameManager.Instance.inventory.selection, selectedLocation, GameManager.Instance.playerStats.gold);
        }
    }
    public void PlaceTrap(GameObject inventorySelection, Location location, int playerGold)
    {
        if (playerGold > inventorySelection.GetComponent<Baits>().currentCost)
        {
            GameObject bait = GameObject.Instantiate(inventorySelection, location.transform.position, Quaternion.Euler(0, baitRotation, 0));
            bait.GetComponent<Baits>().location = location;
            bait.GetComponent<Baits>().InitBait();
            location.occupied = true;
        }
        else
            Debug.Log("not enough gold");
    }

    public void MovePreview(Location location, int rotation)
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
}
