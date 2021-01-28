using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EntityMovement : Entity
{
    //[HideInInspector]
    public float targetTreshold, RandomSelectorRadius;
    //[HideInInspector]
    public float minWaitingTime, maxWaitingTime, waitingDelay;
    //[HideInInspector]
    public bool gotATarget;

    GameObject[] possibleTargets;

    NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    Vector3 destination;

    void Start()
    {
        if (targetTreshold == 0f)
        {
            targetTreshold = 0.1f;
            Debug.Log("target treshold set to 0.1f by default");
        }//security just in case
        possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");

        Target(status);

        entityNavMeshAgent = GetComponent<NavMeshAgent>();
        destination = entityNavMeshAgent.destination;
    }

    void Update()
    {
        if (gotATarget == false)
        {
            Target(status);

            Debug.Log("looking for target");
        }
        else if (Vector3.Distance(destination, Target(status)) > targetTreshold)
        {
            destination = Target(status);
            entityNavMeshAgent.destination = destination;

            Debug.Log("target acquired");
        }/*
        else if (Vector3.Distance(destination, Target(status)) <= targetTreshold)
        {
            Debug.Log("no target");
            gotATarget = false;
        }*/
    }

    public Vector3 Target(EntityStatus status)
    {
        #region
        /*
        switch (status)
        {
            case EntityStatus.Neutral:
                waitingDelay -= Time.deltaTime;
                if (waitingDelay <= 0)
                {
                    waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                    destination = transform.position + Random.insideUnitSphere * neutralRandomPointSelectorRadius;
                    if (NavMesh.SamplePosition(transform.position, out navMeshHit, neutralRandomPointSelectorRadius, NavMesh.AllAreas))
                    {
                        destination = navMeshHit.position;
                    }
                }
                break;

            case EntityStatus.Enemy:
                waitingDelay -= Time.deltaTime;
                if (PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position) == true)
                {
                    destination = GameManager.Instance.player.transform.position;
                }
                else if (possibleTargets != null)
                {
                    possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");
                    float shortDistance = Mathf.Infinity;
                    for (int i = 0; i < possibleTargets.Length; i++)
                    {
                        float distance = Vector3.Distance(transform.position, possibleTargets[i].transform.position);
                        if (distance < shortDistance)
                        {
                            shortDistance = distance;
                            destination = possibleTargets[i].transform.position;
                        }
                    }
                }
                else if (waitingDelay <= 0 && possibleTargets == null)
                {
                    waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                    destination = transform.position + Random.insideUnitSphere * neutralRandomPointSelectorRadius;
                    if (NavMesh.SamplePosition(transform.position, out navMeshHit, neutralRandomPointSelectorRadius, NavMesh.AllAreas))
                    {
                        destination = navMeshHit.position;
                    }
                }
                break;

            case EntityStatus.Ally:
                float shortestDistance = Mathf.Infinity;
                if (GameManager.Instance.builder.firmeLocation != null)
                {
                    for (int i = 0; i < GameManager.Instance.builder.firmeLocation.Count; i++)
                    {
                        float distance = Vector3.Distance(transform.position, GameManager.Instance.builder.firmeLocation[i].position);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            destination = GameManager.Instance.builder.firmeLocation[i].position;
                        }
                    }
                }
                else if (GameManager.Instance.builder.firmeLocation == null)
                {
                    Debug.Log("no corporation");  
                }
                break;
        }*/
        #endregion
        switch (status)
        {
            case EntityStatus.Neutral:
                Debug.Log(status);
                gotATarget = true;
                break;

            case EntityStatus.Enemy:
                Debug.Log(status);
                gotATarget = true;

                waitingDelay -= Time.deltaTime;
                if (PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position) == true)
                {
                    destination = GameManager.Instance.player.transform.position;
                }
                else if (possibleTargets != null)
                {
                    possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");
                    float shortDistance = Mathf.Infinity;
                    for (int i = 0; i < possibleTargets.Length; i++)
                    {
                        float distance = Vector3.Distance(transform.position, possibleTargets[i].transform.position);
                        if (distance < shortDistance)
                        {
                            shortDistance = distance;
                            destination = possibleTargets[i].transform.position;
                        }
                    }
                }
                else if (waitingDelay <= 0 && possibleTargets == null)
                {
                    waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                    destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                    if (NavMesh.SamplePosition(transform.position, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                    {
                        destination = navMeshHit.position;
                    }
                }
                break;

            case EntityStatus.Ally:
                Debug.Log(status);
                gotATarget = true;
                break;
        }
        return destination;
    }

    public bool PlayerInRange(float detectionDistance, Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, playerPosition) <= detectionDistance)
        {
            return true;
        }
        else
            return false;
    }
}