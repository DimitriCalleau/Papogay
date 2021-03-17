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

    public bool isAttracted;
    Vector3 tempAttractingPoint;
    public float health, maxHealth;
    public int healthMinAlly, healthMaxEnm;
    public float currentSpeed, enemySpeed, neutralAllySpeed, playerDetectionRadius, targetTreshold, amazoonSpeedFactor;

    public float randomSelectorRadius,minWaitingTime, maxWaitingTime, waitingDelay;

    float timerSlow, attractingTimer;

    GameObject[] possibleTargets;
    [HideInInspector]
    public GameObject target;//Cible a attaquer

    [HideInInspector]
    public NavMeshAgent entityNavMeshAgent;
    NavMeshHit navMeshHit;
    [HideInInspector]
    public Vector3 destination;//Position a atteind

    [Header("Visual Informations")]
    public GameObject skin;
    public SkinnedMeshRenderer rnd;
    public Material[] stateMats;//Enemy = 0, Neutral = 1, Ally = 2
    public Animator anm;
    public GameObject particlesParent;
    public ParticleSystem[] particlePrefabs;

    [Header("UI")]
    public Gradient stateColors;
    public Image allyProgressBar;
    public Image progressBar;
    public float progressBarFadingTime, timeBeforeBarFading;
    float progressBarFadingTimer, beforeFadingTimer;

    public Image mapIndicator;
    public Image focusIndicator;
    public Sprite[] focusImages;//0 player, 1 ally, 2 neutral, 3 enemy
    public void Init(int lifePoints)
    {
        health = lifePoints;

        anm = skin.GetComponent<Animator>();

        AddEntity();
        ChangeEntityStatus();

        if (targetTreshold == 0f)
        {
            targetTreshold = 1.5f;
        }//security just in case

        possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");

        entityNavMeshAgent = GetComponent<NavMeshAgent>();
        entityNavMeshAgent.speed = currentSpeed;

        destination = entityNavMeshAgent.destination;
        destination.y = destination.y + entityNavMeshAgent.baseOffset;

        GetComponent<EntityAttack>().entity = this;

        progressBarFadingTime -= Time.deltaTime;
        Color allyBarColor = allyProgressBar.color;
        Color barColor = progressBar.color;
        allyBarColor.a = 0;
        barColor.a = 0;
        allyProgressBar.color = allyBarColor;
        progressBar.color = barColor;
    }

    void Update()
    {
        if (entityNavMeshAgent != null)
        {
            entityNavMeshAgent.stoppingDistance = targetTreshold;
        }
        if(isAttracted == false)
        {
            ChangeDestination();
        }
        else
        {
            entityNavMeshAgent.destination = tempAttractingPoint;
        }

        if(status != EntityStatus.Neutral)
        {
            if (entityNavMeshAgent.destination != null || entityNavMeshAgent.isStopped == false)
            {
                anm.SetFloat("WalkIdle", 1);
            }
            else
            {
                anm.SetFloat("WalkIdle", 0);
            }
        }

        if (attractingTimer > 0)
        {
            attractingTimer -= Time.deltaTime;
        }
        else
        {
            isAttracted = false;
        }

        if(beforeFadingTimer <= 0)
        {
            if (progressBarFadingTimer > 0)
            {
                progressBarFadingTime -= Time.deltaTime;
                Color allyBarColor = allyProgressBar.color;
                Color barColor = progressBar.color;
                allyBarColor.a = progressBarFadingTime / progressBarFadingTimer;
                barColor.a = progressBarFadingTime / progressBarFadingTimer;
                allyProgressBar.color = allyBarColor;
                progressBar.color = barColor;
            }
        }
        else
        {
            beforeFadingTimer -= Time.deltaTime;
        }
    }

    void ChangeDestination()
    {
        switch (status)
        {
            #region neutral
            case EntityStatus.Neutral:

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
                        focusIndicator.sprite = focusImages[1];
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            #endregion

            #region enemy
            case EntityStatus.Enemy:

                possibleTargets = GameObject.FindGameObjectsWithTag("TargetForEnemyEntity");

                if (possibleTargets.Length == 0 || possibleTargets == null)
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
                            focusIndicator.sprite = focusImages[2];
                        }
                    }
                }

                if (timerSlow > 0)
                {
                    timerSlow -= Time.deltaTime;
                }
                else
                {
                    currentSpeed = enemySpeed;
                }

                break;
            #endregion

            #region ally
            case EntityStatus.Ally:

                float minDist = Mathf.Infinity;
                if (GameManager.Instance.builder.allShopsLocations != null)
                {
                    Transform tempShop = null;
                    for (int i = 0; i < GameManager.Instance.builder.allFirmesLocations.Count; i++)
                    {
                        if (GameManager.Instance.builder.allShopsLocations[i] != null)
                        {
                            float distance = Vector3.Distance(transform.position, GameManager.Instance.builder.allShopsLocations[i].transform.position);
                            if (distance < minDist)
                            {
                                minDist = distance;
                                tempShop = GameManager.Instance.builder.allShopsLocations[i].transform;
                            }
                        }
                    }

                    if (tempShop != null)
                    {
                        destination = tempShop.position;
                        target = tempShop.gameObject;
                        focusIndicator.sprite = focusImages[0];
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
    public void DamageEntity(int _damage, bool damageOrHeal)//true -> heal, false -> damage
    {
        anm.SetTrigger("Hit");

        switch (damageOrHeal)
        {
            case true:
                health += _damage;
                GameObject allyDamageParticles = Instantiate(particlePrefabs[4].gameObject, transform.position, Quaternion.identity);
                allyDamageParticles.transform.parent = particlesParent.transform;
                Destroy(allyDamageParticles, allyDamageParticles.GetComponent<ParticleSystem>().main.duration);
                break;
            case false:
                health -= _damage;
                if(health < healthMaxEnm)
                {
                    health = healthMaxEnm + 1;
                }
                GameObject enemyDamageParticles = Instantiate(particlePrefabs[3].gameObject, transform.position, Quaternion.identity);
                enemyDamageParticles.transform.parent = particlesParent.transform;
                Destroy(enemyDamageParticles, enemyDamageParticles.GetComponent<ParticleSystem>().main.duration);
                break;
        }

        Color allyBarColor = allyProgressBar.color;
        Color barColor = progressBar.color;
        allyBarColor.a = 1;
        barColor.a = 1;
        allyProgressBar.color = allyBarColor;
        progressBar.color = barColor;
        beforeFadingTimer = timeBeforeBarFading;
        progressBarFadingTimer = progressBarFadingTime;

        ChangeEntityStatus();
    }
    public void ChangeEntityStatus()
    {
        if (health >= healthMinAlly)
        {
            status = EntityStatus.Ally;
            this.gameObject.layer = 12;//allyLayer;
            gameObject.tag = "TargetForEnemyEntity";
            anm.SetInteger("Status", 2);

            allyProgressBar.fillAmount = health / maxHealth;
            if(health == 0)
            {
                allyProgressBar.fillAmount = 0;
            }

            mapIndicator.color = stateColors.colorKeys[2].color;
            currentSpeed = neutralAllySpeed;

            rnd.material = stateMats[2];

            GameObject convertingParticles = Instantiate(particlePrefabs[2].gameObject, transform.position, Quaternion.identity);
            convertingParticles.transform.parent = particlesParent.transform;
            Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            if (previousStatus == EntityStatus.Enemy)
            {
                Debug.Log("converted");
                previousStatus = EntityStatus.Ally;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
            }
        }
        else if (health <= healthMaxEnm)
        {
            status = EntityStatus.Enemy;
            this.gameObject.layer = 10;// enemyLayer;
            gameObject.tag = "Untagged";
            anm.SetInteger("Status", 0);

            allyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                allyProgressBar.fillAmount = 0;
            } 
            mapIndicator.color = stateColors.colorKeys[0].color;
            currentSpeed = enemySpeed;

            if(previousStatus != EntityStatus.Enemy)
            {
                previousStatus = EntityStatus.Enemy;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
                rnd.material = stateMats[0];
            }

            GameObject convertingParticles = Instantiate(particlePrefabs[0].gameObject, transform.position, Quaternion.identity);
            convertingParticles.transform.parent = particlesParent.transform;
            Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            status = EntityStatus.Neutral;
            this.gameObject.layer = 11;//neutralLayer;
            gameObject.tag = "Untagged";
            anm.SetInteger("Status", 1);

            allyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                allyProgressBar.fillAmount = 0;
            }
            mapIndicator.color = stateColors.colorKeys[1].color;
            currentSpeed = neutralAllySpeed;

            rnd.material = stateMats[1];

            GameObject convertingParticles = Instantiate(particlePrefabs[1].gameObject, transform.position, Quaternion.identity);
            convertingParticles.transform.parent = particlesParent.transform;
            Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            if (previousStatus == EntityStatus.Enemy)
            {
                previousStatus = EntityStatus.Neutral;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
            }
        }
    }
    void AddEntity()
    {
        if (health >= healthMinAlly)
        {
            previousStatus = EntityStatus.Ally;
            rnd.material = stateMats[2];
        }
        else if (health <= healthMaxEnm)
        {
            previousStatus = EntityStatus.Enemy;
            rnd.material = stateMats[0];
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            previousStatus = EntityStatus.Neutral;
            rnd.material = stateMats[1];
        }
    }
    public void ChangeEntitySpeed(float slowFactor, float duration)
    {
        currentSpeed = enemySpeed * slowFactor;
        timerSlow = duration;
    }
    public void AttractEntity(Vector3 attractingPoint, float attrationTime)
    {
        isAttracted = true;
        tempAttractingPoint = attractingPoint;
        entityNavMeshAgent.destination = tempAttractingPoint;
        attractingTimer = attrationTime;
    }

    void DestroyEntity()
    {
        Destroy(this.gameObject);
    }
    void OnEnable()
    {
        GameManager.Instance.EndWave += DestroyEntity;
    }
    void OnDisable()
    {
        GameManager.Instance.EndWave -= DestroyEntity;
    }
}