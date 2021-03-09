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

    public bool isAttracted;
    Vector3 tempAttractingPoint;
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
    public Vector3 destination;//Position a atteind

    [Header("VisualInformations")]
    public GameObject skin;
    public SkinnedMeshRenderer rnd;
    public Material[] stateMats;//Enemy = 0, Neutral = 1, Ally = 2
    public Animator anm;
    public GameObject particlesParent;
    public ParticleSystem[] particlePrefabs;

    [Header("UI")]
    public Image AllyProgressBar;
    public Gradient StateColors;
    public Image StateIndicator;
    public Image mapIndicator;

    public void Init(int lifePoints)
    {
        health = lifePoints;

        anm = skin.GetComponent<Animator>();

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
            ChangeDestination();
        }
        else
        {
            entityNavMeshAgent.destination = tempAttractingPoint;
        }

        if (entityNavMeshAgent.destination != null || entityNavMeshAgent.isStopped == false)
        {
            if (entityNavMeshAgent.remainingDistance <= targetTreshold)
            {
                anm.SetFloat("WalkIdle", 0);
            }
            else
                anm.SetFloat("WalkIdle", 1);
        }
        else
        {
            anm.SetFloat("WalkIdle", 0);
        }

        if (attractingTimer > 0)
        {
            attractingTimer -= Time.deltaTime;
        }
        else
        {
            isAttracted = false;
        }
    }

    void ChangeDestination()
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
    public bool PlayerInRange()
    {
        if (GameManager.Instance != null && GameManager.Instance.player.activeInHierarchy == true && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= playerDetectionRadius)
        {
            return true;
        }
        else
            return false;
    }
    public void DamageEntity(int _damage, bool damageOrHeal)//true -> heal, false -> damage
    {
        anm.SetTrigger("Hit");

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
            this.gameObject.layer = 12;//allyLayer;
            gameObject.tag = "TargetForEnemyEntity";
            anm.SetInteger("Status", 2);

            AllyProgressBar.fillAmount = health / maxHealth;
            if(health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }

            StateIndicator.color = StateColors.colorKeys[0].color;
            mapIndicator.color = StateColors.colorKeys[0].color;
            currentSpeed = baseSpeed;
            if (previousStatus == EntityStatus.Neutral)
            {
                previousStatus = EntityStatus.Ally;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, false);
                rnd.material = stateMats[2];

                GameObject convertingParticles = Instantiate(particlePrefabs[2].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
            if (previousStatus == EntityStatus.Enemy)
            {
                previousStatus = EntityStatus.Ally;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
                rnd.material = stateMats[2];

                GameObject convertingParticles = Instantiate(particlePrefabs[2].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
        }
        else if (health <= healthMaxEnm)
        {
            status = EntityStatus.Enemy;
            this.gameObject.layer = 10;// enemyLayer;
            gameObject.tag = "Untagged";
            anm.SetInteger("Status", 0);

            AllyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }
            StateIndicator.color = StateColors.colorKeys[2].color;
            mapIndicator.color = StateColors.colorKeys[2].color;
            if (previousStatus == EntityStatus.Neutral)
            {
                previousStatus = EntityStatus.Enemy;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, false);
                rnd.material = stateMats[0];

                GameObject convertingParticles = Instantiate(particlePrefabs[0].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
            if (previousStatus == EntityStatus.Ally)
            {
                previousStatus = EntityStatus.Enemy;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, false);
                rnd.material = stateMats[0];

                GameObject convertingParticles = Instantiate(particlePrefabs[0].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            status = EntityStatus.Neutral;
            this.gameObject.layer = 11;//neutralLayer;
            gameObject.tag = "TargetForEnemyEntity";
            anm.SetInteger("Status", 1);

            AllyProgressBar.fillAmount = health / maxHealth;
            if (health == 0)
            {
                AllyProgressBar.fillAmount = 0;
            }
            StateIndicator.color = StateColors.colorKeys[1].color;
            mapIndicator.color = StateColors.colorKeys[1].color;
            currentSpeed = baseSpeed;

            if (previousStatus == EntityStatus.Enemy)
            {
                previousStatus = EntityStatus.Neutral;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, false);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
                rnd.material = stateMats[1];

                GameObject convertingParticles = Instantiate(particlePrefabs[1].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
            if (previousStatus == EntityStatus.Ally)
            {
                previousStatus = EntityStatus.Neutral;
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, false);
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
                rnd.material = stateMats[1];

                GameObject convertingParticles = Instantiate(particlePrefabs[1].gameObject, transform.position, Quaternion.identity);
                convertingParticles.transform.parent = particlesParent.transform;
                Destroy(convertingParticles, convertingParticles.GetComponent<ParticleSystem>().main.duration);
            }
        }
    }
    void AddEntity()
    {
        if (health >= healthMinAlly)
        {
            previousStatus = EntityStatus.Ally;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
            }
            rnd.material = stateMats[2];
        }
        else if (health <= healthMaxEnm)
        {
            previousStatus = EntityStatus.Enemy;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Enemy, true);
            }
            rnd.material = stateMats[0];
        }
        else if (health > healthMaxEnm && health < healthMinAlly)
        {
            previousStatus = EntityStatus.Neutral;
            if(GameManager.Instance != null)
            {
                GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Neutral, true);
            }
            rnd.material = stateMats[1];
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
    public void AttractEntity(Vector3 attractingPoint, float attrationTime)
    {
        isAttracted = true;
        tempAttractingPoint = attractingPoint;
        entityNavMeshAgent.destination = tempAttractingPoint;
        attractingTimer = attrationTime;
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
        if(GameManager.Instance != null)
        {
            GameManager.Instance.EndWave += Dead;
        }
    }
    void OnDisable()
    {
        GameManager.Instance.EndWave -= Dead;
    }
}