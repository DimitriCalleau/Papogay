using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bus : Baits
{
    public float range;
    Collider[] Enemies;
    int entityCount;
    public float departureTime, arrivingTime, travelingTime;
    bool isTraveling, hasArrived;
    public float timeBetweenSpawns;
    float timer;
    public void SetCollider()
    {
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider;
    }
    void Start()
    {
        SetCollider();
    }
    public void Update()
    {
        if (isTraveling)
        {
            if(timer < travelingTime)
            {
                timer += Time.deltaTime;
            }
        }
        if(isTraveling == false)
        {
            BaitAttack();

            LoseLife(Time.deltaTime);
        }
    }
    public void BaitAttack()
    {
        Enemies = Physics.OverlapSphere(colliderCenter, range, ennemisMask);
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                e.gameObject.GetComponent<Entity>().DamageEntity(damages[upgradeIndex], true);
            }
        }
    }
}