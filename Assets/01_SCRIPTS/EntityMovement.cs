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
    public bool closeToTarget, noEnmLeft;

    GameObject[] possibleTargets;

    NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    Vector3 destination;

    void Start()
    {
        if (targetTreshold == 0f)
        {
            targetTreshold = 1.5f;
            Debug.Log("target treshold set to " + targetTreshold + " by default");
        }//security just in case

        possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");

        entityNavMeshAgent = GetComponent<NavMeshAgent>();
        destination = entityNavMeshAgent.destination;
        destination.y = entityNavMeshAgent.baseOffset;
    }

    void Update()
    {
        if (CloseToTarget() == true)
        {
            waitingDelay -= Time.deltaTime;
        }

        if (status != EntityStatus.Enemy)
        {
            if (Vector3.Distance(transform.position, destination) <= targetTreshold && waitingDelay < 0)
            {
                waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                Target(status);
            }
        }
        else if (status == EntityStatus.Enemy && waitingDelay < 0)
        {
            if (Vector3.Distance(transform.position, destination) <= targetTreshold)
            {
                Target(status);

                if (noEnmLeft = false && (possibleTargets == null || possibleTargets.Length == 0))
                {
                    noEnmLeft = true;
                }
            }

            if (noEnmLeft == true)
            {
                Debug.Log("cum");
            }
        }

        destination.y = entityNavMeshAgent.baseOffset;
        entityNavMeshAgent.destination = destination;
    }

    public Vector3 Target(EntityStatus status)
    {
        switch (status)
        {
#region
            case EntityStatus.Neutral:
                destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                if (NavMesh.SamplePosition(destination, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                {
                    destination = navMeshHit.position;
                }
                break;
#endregion

#region
            case EntityStatus.Enemy:
                if (PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position) == true)
                {
                    destination = GameManager.Instance.player.transform.position;
                }
                else if (possibleTargets != null || possibleTargets.Length != 0)
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
                            noEnmLeft = false;
                        }
                    }
                }
                else//s'active pas 
                {
                    destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                    if (NavMesh.SamplePosition(destination, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                    {
                        destination = navMeshHit.position;
                    }
                }
                break;
#endregion

#region
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
                    //Destroy(gameObject);
                }
                break;
#endregion
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

    public bool CloseToTarget()
    {
        if (Vector3.Distance(transform.position, destination) <= targetTreshold)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;//delete
            return true;
        }
        else
            GetComponent<MeshRenderer>().material.color = Color.red;//delete
            return false;
    }
}

/*
     void Update()
{
    destination = Target(status);

    waitingDelay -= Time.deltaTime;
    entityNavMeshAgent.destination = destination;

    if (Vector3.Distance(entityNavMeshAgent.transform.position, destination) >= targetTreshold && gotATarget == true && waitingDelay < 0)
    {
        waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
        gotATarget = false;
    }
    else if (Vector3.Distance(entityNavMeshAgent.transform.position, destination) <= targetTreshold)
    {
        Debug.Log("cum");
    }
}

public Vector3 Target(EntityStatus status)
{
    switch (status)
    {
        #region
        case EntityStatus.Neutral:
            Debug.Log(Vector3.Distance(entityNavMeshAgent.transform.position, destination));

            if (waitingDelay <= 0 && Vector3.Distance(entityNavMeshAgent.transform.position, destination) >= targetTreshold)
            {
                destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                destination.y = entityNavMeshAgent.baseOffset;
                if (NavMesh.SamplePosition(destination, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                {
                    destination = navMeshHit.position;
                    gotATarget = true;
                }

                if (Vector3.Distance(entityNavMeshAgent.transform.position, destination) <= targetTreshold)
                {
                    Debug.Log("cum");
                }
            }
            break;
        #endregion

        #region
        case EntityStatus.Enemy:
            if (PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position) == true)
            {
                destination = GameManager.Instance.player.transform.position;
            }
            else if (possibleTargets != null && possibleTargets.Length != 0)
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
            else if (waitingDelay <= 0 && possibleTargets.Length == 0 && Vector3.Distance(entityNavMeshAgent.transform.position, destination) >= targetTreshold)
            {
                destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                destination.y = transform.position.y;
                if (NavMesh.SamplePosition(destination, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                {
                    destination = navMeshHit.position;
                    gotATarget = true;
                }
            }
            break;
        #endregion

        #region
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
                Debug.Log("no corporation");//de-pop??? -> oui
            }
            break;
        #endregion
    }

    return destination;
}

 */