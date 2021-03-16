using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    public bool hasNewBaitToAdd, baitHasBeenTaken;
    
    public float detectionRange;
    [SerializeField]
    LayerMask shopLayer = -1;
    Collider[] detectedCloseShops;
    public Collider[] allShops;
    public bool closeToShop;
    GameObject selectedShop, oldShopSelection;

    public void AllShopsDetection()
    {
        allShops = Physics.OverlapSphere(Vector3.zero, 1000, shopLayer);
    }
    public void DetectCloseShop()
    {
        detectedCloseShops = Physics.OverlapSphere(GameManager.Instance.player.transform.position, detectionRange, shopLayer);
        if (detectedCloseShops.Length > 0)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < detectedCloseShops.Length; i++)
            {
                float distance = Vector3.Distance(GameManager.Instance.player.transform.position, detectedCloseShops[i].transform.position);
                if (distance < minDist)
                {
                    selectedShop = detectedCloseShops[i].gameObject;
                    minDist = distance;
                }
            }
            if (selectedShop != oldShopSelection)
            {
                if (oldShopSelection != null)
                {
                    GameObject oldChild = oldShopSelection.transform.GetChild(1).gameObject;
                    oldChild.SetActive(false);
                    GameObject oldAnimatedPart = oldShopSelection.transform.GetChild(0).gameObject;
                    oldAnimatedPart.GetComponent<Animator>().SetBool("OpenClose", false);
                }
            }
            GameObject child = selectedShop.transform.GetChild(1).gameObject;
            child.SetActive(true);
            GameObject animatedPart = selectedShop.transform.GetChild(0).gameObject;
            animatedPart.GetComponent<Animator>().SetBool("OpenClose", true);
            oldShopSelection = selectedShop;
            closeToShop = true;
        }
        else
        {
            if(selectedShop != null)
            {
                GameObject child = selectedShop.transform.GetChild(1).gameObject;
                child.SetActive(false);
                GameObject animatedPart = selectedShop.transform.GetChild(0).gameObject;
                animatedPart.GetComponent<Animator>().SetBool("OpenClose", false);
                selectedShop = null;
            }
            closeToShop = false;
        }
    }
}