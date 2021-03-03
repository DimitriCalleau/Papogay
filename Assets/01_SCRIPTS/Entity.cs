using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(NavMeshAgent))]

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public EntityStatus status, previousStatus;
    public FirmeType type;

    public bool isAttracted, attrationStopped;
    public float health, maxHealth;
    public int healthMinAlly, healthMaxEnm;
    public float currentSpeed, baseSpeed, playerDetectionRadius, targetTreshold, amazoonSpeedFactor;

    public float randomSelectorRadius,minWaitingTime, maxWaitingTime, waitingDelay;

    float timerSlow, attractingTimer;

    GameObject[] possibleTargets;
    [HideInInspector]
    public GameObject target;//Cible a attaquer

    [HideInInspector]
    public NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    [HideInInspector]
    public Vector3 destination;//Position a atteindre

    [Header("UI")]
    public Image AllyProgressBar;
    public Gradient StateColors;
    public Image StateIndicator;
    public Image mapIndicator;

    public void Init(int lifePoints)
    {
        health = lifePoints;

        if (type == FirmeType.Amazoon)
        {
            baseSpeed = baseSpeed * amazoonSpeedFactor;
        }
        currentSpeed = baseSpeed;

        AddEntity();
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

    void Update()
    {
        if(entityNavMeshAgent != null)
        {
            entityNavMeshAgent.stoppingDistance = targetTreshold;
        }
        if(isAttracted == false)
        {
            switch (status)
            {
                #region neutral
                case EntityStatus.Neutral:

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

                    if (timerSlow > 0)
                    {
                        timerSlow -= Time.deltaTime;
                    }
                    else
                    {
                        currentSpeed = baseSpeed;
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
                            if (GameManager.Instance.builder.allFirmesLocations[i] != null)
                            {
                                float distance = Vector3.Distance(transform.position, GameManager.Instance.builder.allFirmesLocations[i].position);
                                if (distance < shortestDistance)
                                {
                                    shortestDistance = distance;
                                    tempFirme = GameManager.Instance.builder.allFirmesLocations[i];
                                }
                            }

                        }

                        if (tempFirme != null)
                        {
                            destination = tempFirme.position;
                            target = tempFirme.gameObject;
                        }
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    break;
                    #endregion
            }
            if (destination != null && GameManager.Instance.gameState.start == true)
            {
                entityNavMeshAgent.speed = currentSpeed;
                entityNavMeshAgent.destination = destination;
            }
        }

        if(attractingTimer <= 0 && attrationStopped == true)
        {
            isAttracted = false;
            attrationStopped = false;
        }
        if(attractingTimer > 0)
        {
            attractingTimer -= Time.deltaTime;
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
    public void DamageEntity(int _damage, bool damageOrHeal)//true -> heal, false -> damage
    {
        switch (damageOrHeal)
        {
            case true:
                health += _damage;
                break;
            case false:
                health -= _damage;
                break;
        }
        ChangeEntityStatus();
    }
    public void ChangeEntityStatus()
    {
        if (health >= healthMinAlly)
        {
            status = EntityStatus.Ally;

            AllyProgressBar.fillAmount = health / maxHealth;
            if(health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }

            StateIndicator.color = StateColors.colorKeys[0].color;
            mapIndicator.color = StateColors.colorKeys[0].color;
            currentSpeed = baseSpeed;
            gameObject.tag = "TargetForEnemyEntity";
            if (previousStatus == EntityStatus.Neutral)
            {
                //Debug.Log("neutral => ally");
                previousStatus = EntityStatus.Ally;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, false);
            }
            if (previousStatus == EntityStatus.Enemy)
            {
                //Debug.Log("enemy => ally");
                previousStatus = EntityStatus.Ally;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
            }
        }
        else if (health <= healthMaxEnm)
        {
            status = EntityStatus.Enemy;

            AllyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }
            StateIndicator.color = StateColors.colorKeys[2].color;
            mapIndicator.color = StateColors.colorKeys[2].color;
            gameObject.tag = "Untagged";
            if (previousStatus == EntityStatus.Neutral)
            {
                //Debug.Log("neutral => enemy");
                previousStatus = EntityStatus.Enemy;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, false);
            }
            if (previousStatus == EntityStatus.Ally)
            {
                //Debug.Log("ally => enemy");
                previousStatus = EntityStatus.Enemy;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, false);
            }
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            status = EntityStatus.Neutral;

            AllyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }
            StateIndicator.color = StateColors.colorKeys[1].color;
            mapIndicator.color = StateColors.colorKeys[1].color;
            currentSpeed = baseSpeed;
            gameObject.tag = "TargetForEnemyEntity";

            if (previousStatus == EntityStatus.Enemy)
            {
                //Debug.Log("enemy => neutral");
                previousStatus = EntityStatus.Neutral;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
            }
            if (previousStatus == EntityStatus.Ally)
            {
                //Debug.Log("ally => neutral");
                previousStatus = EntityStatus.Neutral;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, false);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
            }
        }
    }
    void AddEntity()
    {
        if (health >= healthMinAlly)
        {
            previousStatus = EntityStatus.Ally;
            GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
        }
        else if (health <= healthMaxEnm)
        {
            previousStatus = EntityStatus.Enemy;
            GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            previousStatus = EntityStatus.Neutral;
            GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
        }
    }
    public void ChangeEntitySpeed(float slowFactor, float duration)
    {
        if (type != FirmeType.NormalGym)//les Entités de ce groupe sont immunisées au controles
        {
            currentSpeed = baseSpeed * slowFactor;
            timerSlow = duration;
        }
    }
    public void AttractEntity(Vector3 attractingPoint)
    {
        isAttracted = true;
        entityNavMeshAgent.destination = attractingPoint;
    }
    public void StopAttraction()
    {
        attractingTimer = 1.2f;
        attrationStopped = true;
    }
    public void Dead()
    {
        switch (status)
        {
            case EntityStatus.Neutral:
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, false);
                break;
            case EntityStatus.Enemy:
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
                break;
            case EntityStatus.Ally:
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, false);
                break;
            default:
                break;
        }
        Destroy(this.gameObject);
    }

    void OnEnable()
    {
        GameManager.Instance.EndWave += Dead;
    }
    void OnDisable()
    {
        GameManager.Instance.EndWave -= Dead;
    }
}