using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool occupied = false;
    public LayerMask noBaitLayer = -1;
    public bool cantReceiveBait;
    bool hasChecked, isNotReceiver;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckInventory += CanReceive;
        }
    }
    void CanReceive()
    {
        if (hasChecked == false)
        {
            Collider[] nobaitzone = Physics.OverlapSphere(transform.position, 1, noBaitLayer);

            if (nobaitzone.Length > 0)
            {
                cantReceiveBait = true;
                isNotReceiver = true;
            }
            else
            {
                cantReceiveBait = false;
            }
            hasChecked = true;
        }

        else
        {
            if (isNotReceiver == true)
            {
                cantReceiveBait = true;
            }
            else
            {
                cantReceiveBait = false;
            }
        }
    }
    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckInventory += CanReceive;
        }
    }
    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckInventory -= CanReceive;
        }
    }
}