
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EntityMovement : Entity
{
    //[HideInInspector]
    public float minWaitingTime, maxWaitingTime, waitingDelay;
    //[HideInInspector]
    public bool noEnmLeft;

    GameObject[] possibleTargets;

    NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    public Vector3 destination;

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
        destination.y = destination.y + entityNavMeshAgent.baseOffset;
    }

    void Update()
    {
        switch (CloseToTarget())
        {
            case true:
                waitingDelay -= Time.deltaTime;
                break;

            case false:
                entityNavMeshAgent.destination = destination;
                destination.y = destination.y + entityNavMeshAgent.baseOffset;
                break;
        }

        //not working, the idea behind is to make the entity stop when in targetTreshold
        /*
        if (CloseToTarget() == true)
        {
            destination = entityNavMeshAgent.transform.position;
            destination.y = destination.y + entityNavMeshAgent.baseOffset;
        }
        */

        if (status != EntityStatus.Enemy)
        {
            if (CloseToTarget() == true && waitingDelay < 0)
            {
                waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                Target(status);
            }
        }
        else if (status == EntityStatus.Enemy)
        {
            if (CloseToTarget() == true && (possibleTargets.Length == 0 || possibleTargets == null) && waitingDelay < 0)
            {
                waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                Target(status);
            }
            else if (possibleTargets != null && noEnmLeft == false)
            {
                Target(status);
            }
        }
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
                possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");
                if (PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position) == true)
                {
                    destination = GameManager.Instance.player.transform.position;
                }
                else if (possibleTargets.Length == 0 || possibleTargets == null)
                {
                    destination = transform.position + Random.insideUnitSphere * RandomSelectorRadius;
                    if (NavMesh.SamplePosition(destination, out navMeshHit, RandomSelectorRadius, NavMesh.AllAreas))
                    {
                        destination = navMeshHit.position;
                    }
                    noEnmLeft = true;
                }
                else
                {
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
                    Destroy(gameObject);
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
        if (Vector3.Distance(entityNavMeshAgent.transform.position, destination) <= targetTreshold)
        {
            //GetComponent<MeshRenderer>().material.color = Color.green;//used as a debug
            return true;
        }
        else
            //GetComponent<MeshRenderer>().material.color = Color.red;//used as a debug
            return false;
    }
}