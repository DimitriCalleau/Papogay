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
    BusState state;
    public float departureTime, arrivingTime, waitingTime, travelingTime, timeBetweenEntityRespawns;
    float entitySpawnTimer, departureTimer, arrivingTimer, waitingTimer, travelingTimer;
    public GameObject busGameobject, respawnPoint;
    Animator busAnimator;
    Material busMaterial;
    public void SetCollider()
    {
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider;
    }
    void Start()
    {
        SetCollider();
        busMaterial = busGameobject.GetComponent<MeshRenderer>().material;
        Fade(0);
        state = BusState.isWaiting;
        waitingTimer = waitingTime;
        arrivingTimer = arrivingTime;
    }
    public void Update()
    {
        Debug.Log(entityCount);
        switch (state)
        {
            case BusState.isTraveling:
                Debug.Log("is traveling");

                if (travelingTimer > 0)
                {
                    travelingTimer -= Time.deltaTime;
                }
                else
                {
                    arrivingTimer = 0;
                    state = BusState.isArriving;
                }
                break;
            case BusState.isArriving:
                if (arrivingTimer < arrivingTime)
                {
                    arrivingTime += Time.deltaTime;
                    Fade(arrivingTimer / arrivingTime);
                }
                else
                {
                    Fade(1);
                    state = BusState.isSpawning;
                }
                break;
            case BusState.isGoing:
                Debug.Log("is going");

                if (departureTimer > 0)
                {
                    departureTimer -= Time.deltaTime;
                    Fade(departureTimer / departureTime);
                }
                else
                {
                    Fade(0);
                    state = BusState.isTraveling;
                    travelingTimer = travelingTime;
                }
                break;
            case BusState.isWaiting:
                Debug.Log("is waiting");
                if (arrivingTimer < arrivingTime)
                {
                    arrivingTimer += Time.deltaTime;
                    Fade(arrivingTimer / arrivingTime);
                }
                else
                {
                    Fade(1);
                    BaitAttack();
                }

                if(waitingTimer > 0)
                {
                    waitingTimer -= Time.deltaTime;
                }
                else
                {
                    LoseLife(1000);
                }
                break;

            case BusState.isSpawning:
                Debug.Log("is spawning");
                if (entityCount > 0)
                {
                    if (entitySpawnTimer <= 0)
                    {
                        GameObject newAlly = Instantiate(GameManager.Instance.builder.entityPrefab, respawnPoint.transform.position, Quaternion.identity);
                        newAlly.GetComponent<Entity>().Init(100);
                        entitySpawnTimer = timeBetweenEntityRespawns;
                    }
                    else
                    {
                        entitySpawnTimer -= Time.deltaTime;
                    }
                }
                else
                {
                    LoseLife(1000);
                }
                break;
        }
    }
    public void BaitAttack()
    {
        Enemies = Physics.OverlapSphere(colliderCenter, range, ennemisMask);
        if (Enemies.Length > 0)
        {
            entityCount = Enemies.Length;
            foreach(Collider enm in Enemies)
            {
                EntityStatus enmstatus = enm.GetComponent<Entity>().status;
                Destroy(enm);
                GameManager.Instance.waveManager.AddRemoveEntity(enmstatus, false);
            }
            state = BusState.isGoing;
            departureTimer = departureTime;
            arrivingTimer = 0;
            Update();
        }
    }

    void Fade(float _timer)
    {
        Debug.Log(busMaterial);
        Color fadecolor = busMaterial.color;
        fadecolor.a = _timer;
        busMaterial.color = fadecolor;
    }
}