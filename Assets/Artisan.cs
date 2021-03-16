using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artisan : MonoBehaviour
{
    public int zoneIndex;

    public string activatedTag, unactivatedTag;
    public void ActivateShop(int index)
    {
        if(zoneIndex == index)
        {
            this.gameObject.tag = activatedTag;
        }
        /*else
        {
            this.gameObject.tag = unactivatedTag;
        }*/
    }
    public void UnactivateShop()
    {
        this.gameObject.tag = unactivatedTag;
    }
}
