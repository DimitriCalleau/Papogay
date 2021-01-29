using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool occupied = false;
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.allLocations.Add(this);
        }
    }
}
