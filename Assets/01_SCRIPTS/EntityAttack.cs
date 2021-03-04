using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [HideInInspector]
    public Entity entity;
    float timerCooldownAttack, timerDamageAnticipation;
    public int allyAttackDamages, enemyAttackDamages; 
    public float entityDamageCooldown, entityAttackRange, entityStratAttackingRange, enemyDamageAnticipationDuration, allyDamageAnticipationDuration;
    bool enemyIsAttacking, allyIsAttacking;
    public LayerMask allyAttackLayer = -1;
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
                    if (entity.target != null && enemyIsAttacking != true && Vector3.Distance(transform.position, entity.target.transform.position) <= entityStratAttackingRange)
                    {
                        if (entity.target == GameManager.Instance.player)
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            //entity.anm.SetTrigger("Attack");
                            entity.anm.SetBool("isAttacking", true);
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = enemyDamageAnticipationDuration;
                            enemyIsAttacking = true;
                        }
                        else
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            //entity.anm.SetTrigger("Attack");
                            entity.anm.SetBool("isAttacking", true);
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = enemyDamageAnticipationDuration;
                            enemyIsAttacking = true;
                        }
                    }

                }

                if(timerDamageAnticipation >= 0)
                {
                    timerDamageAnticipation -= Time.deltaTime;
                    Debug.Log(timerDamageAnticipation);
                }
                else
                {
                    if (enemyIsAttacking == true)
                    {
                        entity.entityNavMeshAgent.isStopped = false;

                        if (entity.target != null && Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange)
                        {
                            if (entity.target == GameManager.Instance.player)
                            {
                                entity.anm.SetBool("isAttacking", false);
                                GameManager.Instance.playerStats.DamagePlayer(enemyAttackDamages);
                                enemyIsAttacking = false;
                            }
                            else
                            {
                                entity.anm.SetBool("isAttacking", false);
                                entity.target.GetComponent<Entity>().DamageEntity(enemyAttackDamages, false);
                                enemyIsAttacking = false;
                            }
                        }
                    }
                }
                break;

            case EntityStatus.Ally:

                Collider[] firme = Physics.OverlapSphere(transform.position + Vector3.up, entityAttackRange, allyAttackLayer);

                if (entity.target != null)//link between destination and target
                {
                    if(firme.Length != 0 && allyIsAttacking == false)
                    {
                        for (int i = 0; i < firme.Length; i++)
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            //entity.anm.SetTrigger("Attack");
                            entity.anm.SetBool("isAttacking", true);
                            timerDamageAnticipation = allyDamageAnticipationDuration;
                            allyIsAttacking = true;
                        }
                    }
                    if (timerDamageAnticipation >= 0)
                    {
                        timerDamageAnticipation -= Time.deltaTime;
                        Debug.Log(timerDamageAnticipation);
                    }
                    else
                    {
                        if (allyIsAttacking == true)
                        {
                            entity.entityNavMeshAgent.isStopped = false;
                            if (firme.Length != 0)
                            {
                                for (int i = 0; i < firme.Length; i++)
                                {
                                    firme[i].GetComponent<Firme>().DamageFirme(allyAttackDamages);
                                    entity.anm.SetBool("isAttacking", false);
                                    allyIsAttacking = false;
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
        Gizmos.DrawWireSphere(transform.position + Vector3.up, entityAttackRange);
    }
}