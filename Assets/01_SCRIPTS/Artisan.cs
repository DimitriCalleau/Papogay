using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artisan : MonoBehaviour
{
    public Animator shopAnm;
    public bool basicallyInactive;
    public BuildingSize size;

    public string activatedTag, unactivatedTag;
    public void ActivateShop()
    {
        this.gameObject.tag = activatedTag;
    }
    public void UnactivateShop()
    {
        if(basicallyInactive == true)
        {
            this.gameObject.tag = unactivatedTag;
        }
    }
}
