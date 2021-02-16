using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : Baits
{
    public Vector3 colliderSize, gatheringPoint;
    [HideInInspector]
    public Vector3 rotatedColliderSize;
    Collider[] Enemies;
    public void SetCollider()
    {
        offsetHeightCollider = colliderSize.y / 2;
        offSetForwardCollider = colliderSize.z / 2;
        Quaternion forwardRotation = Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0);
        Vector3 forwardVector = forwardRotation * Vector3.forward * offSetForwardCollider;
        rotatedColliderSize = forwardRotation * colliderSize;
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider + forwardVector;
    }
    void Start()
    {
        SetCollider();
    }
    public void Update()
    {
        BaitAttack();
        LoseLife(Time.deltaTime);
    }
    public void BaitAttack()
    {
        Enemies = Physics.OverlapBox(colliderCenter, rotatedColliderSize, Quaternion.identity, ennemisMask);
        if(Enemies.Length > 0)
        {
            foreach(Collider e in Enemies)
            {
                //Attarct Enemy
            }
        }
    }
}