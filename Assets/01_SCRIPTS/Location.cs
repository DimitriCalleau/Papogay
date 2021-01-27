using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool occupied = false;

    void Awake()
    {
        GameManager.Instance.allLocations.Add(this);
    }
}
