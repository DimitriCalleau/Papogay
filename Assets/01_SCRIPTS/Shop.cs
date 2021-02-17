using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    public float detectionRange;
    [SerializeField]
    LayerMask shopLayer = -1;
    Collider[] shops;
    public bool closeToShop;
    GameObject selectedShop, oldShopSelection;
    public void DetectCloseShop()
    {
        shops = Physics.OverlapSphere(GameManager.Instance.player.transform.position, detectionRange, shopLayer);
        if (shops.Length > 0)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < shops.Length; i++)
            {
                float distance = Vector3.Distance(GameManager.Instance.player.transform.position, shops[i].transform.position);
                if (distance < minDist)
                {
                    selectedShop = shops[i].gameObject;
                    minDist = distance;
                }
            }
            if (selectedShop != oldShopSelection)
            {
                if (oldShopSelection != null)
                {
                    GameObject oldChild = oldShopSelection.transform.GetChild(0).gameObject;
                    oldChild.SetActive(false);
                    GameObject oldAnimatedPart = oldShopSelection.transform.GetChild(1).gameObject;
                    oldAnimatedPart.GetComponent<Animator>().SetBool("OpenClose", false);
                }
            }
            GameObject child = selectedShop.transform.GetChild(0).gameObject;
            child.SetActive(true);
            GameObject animatedPart = selectedShop.transform.GetChild(1).gameObject;
            animatedPart.GetComponent<Animator>().SetBool("OpenClose", true);
            oldShopSelection = selectedShop;
            closeToShop = true;
        }
        else
        {
            if(selectedShop != null)
            {
                GameObject child = selectedShop.transform.GetChild(0).gameObject;
                child.SetActive(false);
                GameObject animatedPart = selectedShop.transform.GetChild(1).gameObject;
                animatedPart.GetComponent<Animator>().SetBool("OpenClose", false);
                selectedShop = null;
            }
            closeToShop = false;
        }
    }

}