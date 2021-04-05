﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FruitBox : Baits
{
    public float slowFactor;
    public Vector3 colliderSize;
    [HideInInspector]
    public Vector3 rotatedColliderSize;
    Collider[] Enemies;
    public void SetCollider()
    {
        offsetHeightCollider = colliderSize.y / 2;
        Quaternion forwardRotation = Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0);
        Vector3 forwardVector = forwardRotation * Vector3.forward * offsetForwardCollider;
        rotatedColliderSize = forwardRotation * colliderSize;
        rotatedColliderSize = new Vector3(Mathf.Sqrt(rotatedColliderSize.x * rotatedColliderSize.x), Mathf.Sqrt(rotatedColliderSize.y * rotatedColliderSize.y), Mathf.Sqrt(rotatedColliderSize.z * rotatedColliderSize.z));
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
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                e.GetComponent<Entity>().ChangeEntitySpeed(slowFactor, 0.5f);
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(colliderCenter, colliderSize);
    }
}
