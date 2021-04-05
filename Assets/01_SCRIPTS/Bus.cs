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
    public TextMeshProUGUI timerText;
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
        switch (state)
        {
            case BusState.isTraveling:
                if (travelingTimer > 0)
                {
                    travelingTimer -= Time.deltaTime;
                    UpdateTimerText(travelingTimer);
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
                    arrivingTimer += Time.deltaTime;
                    Fade(arrivingTimer / arrivingTime);
                    UpdateTimerText(arrivingTimer);
                }
                else
                {
                    Fade(1);
                    state = BusState.isSpawning;
                }
                break;
            case BusState.isGoing:
                if (departureTimer > 0)
                {
                    departureTimer -= Time.deltaTime;
                    Fade(departureTimer / departureTime);
                    UpdateTimerText(departureTimer);
                }
                else
                {
                    Fade(0);
                    state = BusState.isTraveling;
                    travelingTimer = travelingTime;
                }
                break;
            case BusState.isWaiting:
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
                    UpdateTimerText(waitingTimer);
                }
                else
                {
                    LoseLife(1000);
                }
                break;

            case BusState.isSpawning:
                if (entityCount > 0)
                {
                    if (entitySpawnTimer <= 0)
                    {
                        GameObject newAlly = Instantiate(GameManager.Instance.builder.entityPrefab, respawnPoint.transform.position, Quaternion.identity);
                        newAlly.GetComponent<Entity>().Init(100);
                        entitySpawnTimer = timeBetweenEntityRespawns;
                        entityCount -= 1;
                    }
                    else
                    {
                        entitySpawnTimer -= Time.deltaTime;
                        UpdateTimerText(entitySpawnTimer);
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
                Destroy(enm.gameObject);
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
        Color fadecolor = busMaterial.color;
        fadecolor.a = _timer;
        busMaterial.color = fadecolor;
    }

    void UpdateTimerText(float _timer)
    {
        float minutesPart = Mathf.FloorToInt(_timer / 60);
        float secondsPart = Mathf.CeilToInt(_timer - (minutesPart * 60));
        timerText.text = minutesPart + ":" + secondsPart;
    }
}