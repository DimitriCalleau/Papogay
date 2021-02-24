using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Entity : MonoBehaviour
{
    public EntityStatus status;
    public BaitType type;

    public int health, healthMaxALLY, healthMinENM;
    public float currentSpeed, baseSpeed, playerDetectionRadius, targetTreshold, amazoonSpeedFactor;

    public float randomSelectorRadius,minWaitingTime, maxWaitingTime, waitingDelay;
    //[HideInInspector]
    public bool noEnmLeft;

    GameObject[] possibleTargets;
    [HideInInspector]
    public GameObject target;//Cible a attaquer

    [HideInInspector]
    public NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    [HideInInspector]
    public Vector3 destination;//Position a atteindre


    public bool testenemyAlly;
    void Start()
    {
        if (testenemyAlly == true)
        {
            Init(100);

        }
        else 
            Init(0);
    }
    public void Init(int lifePoints)
    {
        baseSpeed = currentSpeed;
        health = lifePoints;
        ChangeEntityStatus();
        if (targetTreshold == 0f)
        {
            targetTreshold = 1.5f;
            Debug.Log("target treshold set to " + targetTreshold + " by default");
        }//security just in case

        possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");

        entityNavMeshAgent = GetComponent<NavMeshAgent>();
        entityNavMeshAgent.speed = currentSpeed;

        destination = entityNavMeshAgent.destination;
        destination.y = destination.y + entityNavMeshAgent.baseOffset;

        GetComponent<EntityAttack>().entity = this;
    }

    /*void FixedUpdate()
      {
          if (status == EntityStatus.Enemy)
          {
              switch (type)
              {
                  case BaitType.Amazoon:
                      entityNavMeshAgent.speed = moveSpeed * amazoonSpeedFactor;
                      break;
                  case BaitType.Threadmill:
                      entityNavMeshAgent.speed = baseSpeed;
                      break;
              }
          }

      }*/
    void Update()
    {
        if(entityNavMeshAgent != null)
        {
            entityNavMeshAgent.stoppingDistance = targetTreshold;
        }
        /*switch (CloseToTarget())
        {
            case true:
                waitingDelay -= Time.deltaTime;
                break;

            case false:
                entityNavMeshAgent.destination = destination;
                destination.y = destination.y + entityNavMeshAgent.baseOffset;
                break;
        }*/

        switch (status)
        {
            #region neutral
            case EntityStatus.Neutral:

                if(waitingDelay <= 0)
                {
                    destination = transform.position + Random.insideUnitSphere * randomSelectorRadius;
                    if (NavMesh.SamplePosition(destination, out navMeshHit, randomSelectorRadius, NavMesh.AllAreas))
                    {
                        waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                        destination = navMeshHit.position;
                    }
                }
                if(entityNavMeshAgent.remainingDistance <= targetTreshold)
                {
                    waitingDelay -= Time.deltaTime;
                }
                break;
            #endregion

            #region enemy
            case EntityStatus.Enemy:
                possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");
                if (PlayerInRange() == true)
                {
                    target = GameManager.Instance.player;
                    destination = target.transform.position;
                }
                else if (possibleTargets.Length == 0 || possibleTargets == null)
                {
                    target = null;
                    if (waitingDelay <= 0)
                    {
                        destination = transform.position + Random.insideUnitSphere * randomSelectorRadius;
                        if (NavMesh.SamplePosition(destination, out navMeshHit, randomSelectorRadius, NavMesh.AllAreas))
                        {
                            waitingDelay = Random.Range(minWaitingTime, maxWaitingTime);
                            destination = navMeshHit.position;
                        }
                    }
                    if (entityNavMeshAgent.remainingDistance <= targetTreshold)
                    {
                        waitingDelay -= Time.deltaTime;
                    }
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
                            target = possibleTargets[i].gameObject;
                        }
                    }
                }
                break;
            #endregion

            #region ally
            case EntityStatus.Ally:
                float shortestDistance = Mathf.Infinity;
                if (GameManager.Instance.builder.allFirmesLocations != null)
                {
                    Transform tempFirme = null;
                    for (int i = 0; i < GameManager.Instance.builder.allFirmesLocations.Count; i++)
                    {
                        if(GameManager.Instance.builder.allFirmesLocations[i] != null)
                        {
                            float distance = Vector3.Distance(transform.position, GameManager.Instance.builder.allFirmesLocations[i].position);
                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                tempFirme = GameManager.Instance.builder.allFirmesLocations[i];
                            }
                        }

                    }

                    if(tempFirme != null)
                    {
                        destination = tempFirme.position;
                        target = tempFirme.gameObject;
                        Debug.Log(target);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
                #endregion
        }
        if (destination != null)
        {
            entityNavMeshAgent.destination = destination;
        }
    }
    public bool PlayerInRange()
    {
        if (GameManager.Instance.player.activeInHierarchy == true && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= playerDetectionRadius)
        {
            return true;
        }
        else
            return false;
    }
    public void DamageEntity(int _damage, bool healOrDamage)//true -> heals, false -> deals _damages
    {
        switch (healOrDamage)
        {
            case true:
                health += _damage;
                break;
            case false:
                break;
        }
        ChangeEntityStatus();
    }
    public void ChangeEntityStatus()
    {
        if (health <= healthMaxALLY)
        {
            status = EntityStatus.Ally;
            gameObject.tag = "TargetForEnemyEntity";
        }
        else if (health >= healthMinENM)
        {
            status = EntityStatus.Enemy;
            gameObject.tag = "Untagged";
        }
        else if (health < healthMinENM && health > healthMaxALLY)
        {
            status = EntityStatus.Neutral;
            gameObject.tag = "TargetForEnemyEntity";
        }
    }
    public void Dead()
    {
        Destroy(this);
    }
}