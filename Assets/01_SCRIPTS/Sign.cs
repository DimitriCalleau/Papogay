using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : Baits
{
    public Vector3 colliderSize, gatheringPoint;
    Vector3 turnedGatheringPoint;

    [HideInInspector]
    public Vector3 rotatedColliderSize;
    Collider[] Enemies;
    public void SetCollider()
    {
        offsetHeightCollider = colliderSize.y / 2;
        Quaternion forwardRotation = Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0);
        turnedGatheringPoint = transform.position + forwardRotation * gatheringPoint;
        Vector3 forwardVector = forwardRotation * Vector3.forward * offsetForwardCollider;
        rotatedColliderSize = forwardRotation * colliderSize;
        rotatedColliderSize = new Vector3(Mathf.Sqrt(rotatedColliderSize.x * rotatedColliderSize.x), Mathf.Sqrt(rotatedColliderSize.y * rotatedColliderSize.y), Mathf.Sqrt(rotatedColliderSize.z * rotatedColliderSize.z));
        gatheringPoint = transform.position + forwardRotation * gatheringPoint;
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
        Enemies = Physics.OverlapBox(colliderCenter, rotatedColliderSize / 2, Quaternion.identity, ennemisMask);
        if(Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                if(e.GetComponent<Entity>().isAttracted == false)
                {
                    e.GetComponent<Entity>().AttractEntity(gatheringPoint, usure);
                }
            }
        }
    }
}