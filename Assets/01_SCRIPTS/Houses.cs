using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houses : MonoBehaviour
{
    public string houseTag;
    public int zone;
    public bool shopped;
    public void ActivateTag(int _zone)
    {
        if(zone == _zone)
        {
            gameObject.tag = houseTag;
        }
    }

    public void Unactivate()
    {
        gameObject.tag = "Untagged";
        gameObject.SetActive(true);
    }

    public void GetReplaced()
    {

    }
}
