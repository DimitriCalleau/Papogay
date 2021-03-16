using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [HideInInspector]
    public Entity entity;
    float timerCooldownAttack, timerAnticipation;
    public int enemyAttackDamages; 
    bool enemyIsAttacking;
    public float entityDamageCooldown, entityRange, entityStartAttackingRange, enemyDamageAnticipationDuration, allyEntersShopTime, neutralEntersFirmeTime;
    bool allyIsEntering, neutralIsEntering;
    public LayerMask allyDetectionLayer = -1;
    public LayerMask neutralDetectionLayer = -1;
    void Update()
    {
        switch (entity.status)
        {
            case EntityStatus.Enemy:
                if (timerCooldownAttack > 0)
                {
                    timerCooldownAttack -= Time.deltaTime;
                }
                else 
                {
                    entity.entityNavMeshAgent.isStopped = false;
                    if (entity.target != null && enemyIsAttacking != true && Vector3.Distance(transform.position, entity.target.transform.position) <= entityStartAttackingRange)
                    {
                        entity.entityNavMeshAgent.isStopped = true;
                        entity.anm.SetTrigger("Attack");
                        timerCooldownAttack = entityDamageCooldown;
                        timerAnticipation = enemyDamageAnticipationDuration;
                        enemyIsAttacking = true;
                    }

                }

                if(timerAnticipation >= 0)
                {
                    timerAnticipation -= Time.deltaTime;
                }
                else
                {
                    if (enemyIsAttacking == true)
                    {
                        if (entity.target != null && Vector3.Distance(transform.position, entity.target.transform.position) <= entityRange +1)
                        {
                            entity.target.GetComponent<Entity>().DamageEntity(enemyAttackDamages, false);
                        }
                    }
                    enemyIsAttacking = false;
                }
                break;

            case EntityStatus.Neutral:
                Collider[] firmes = Physics.OverlapSphere(transform.position + Vector3.up, entityRange, neutralDetectionLayer);
                if (entity.target != null)//link between destination and target
                {
                    if (firmes.Length != 0 && neutralIsEntering == false)
                    {
                        for (int i = 0; i < firmes.Length; i++)
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            entity.anm.SetTrigger("Attack");
                            timerAnticipation = neutralEntersFirmeTime;
                            neutralIsEntering = true;
                        }
                    }

                    if (timerAnticipation >= 0)
                    {
                        timerAnticipation -= Time.deltaTime;
                    }
                    else
                    {
                        if (neutralIsEntering == true)
                        {

                            entity.entityNavMeshAgent.isStopped = false;
                            neutralIsEntering = false;
                            if (firmes.Length != 0)
                            {
                                for (int i = 0; i < firmes.Length; i++)
                                {
                                    firmes[i].GetComponent<Firme>().GetNewEntity();
                                    entity.Dead();
                                }
                            }
                        }
                    }
                }
                break;

            case EntityStatus.Ally:

                Collider[] artisan = Physics.OverlapSphere(transform.position + Vector3.up, entityRange, allyDetectionLayer);

                if (entity.target != null)//link between destination and target
                {
                    if(artisan.Length != 0 && allyIsEntering == false)
                    {
                        for (int i = 0; i < artisan.Length; i++)
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            entity.anm.SetTrigger("Attack");
                            timerAnticipation = allyEntersShopTime;
                            allyIsEntering = true;
                        }
                    }

                    if (timerAnticipation >= 0)
                    {
                        timerAnticipation -= Time.deltaTime;
                    }
                    else
                    {
                        if (allyIsEntering == true)
                        {

                            entity.entityNavMeshAgent.isStopped = false;
                            allyIsEntering = false;
                            if (artisan.Length != 0)
                            {
                                for (int i = 0; i < artisan.Length; i++)
                                {
                                    GameManager.Instance.waveManager.AddRemoveEntity(EntityStatus.Ally, true);
                                    entity.Dead();
                                }
                            }
                        }
                    }
                }
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, entityRange);
    }
}