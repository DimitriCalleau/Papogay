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
                            entity.anm.SetTrigger("Attack"); 
                            Debug.Log("attaque");
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = enemyDamageAnticipationDuration;
                            enemyIsAttacking = true;
                        }
                        else
                        {
                            entity.entityNavMeshAgent.isStopped = true;
                            entity.anm.SetTrigger("Attack");
                            Debug.Log("attaque");
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = enemyDamageAnticipationDuration;
                            enemyIsAttacking = true;
                        }
                    }

                }

                if(timerDamageAnticipation >= 0)
                {
                    timerDamageAnticipation -= Time.deltaTime;
                }
                else
                {
                    if (enemyIsAttacking == true)
                    {
                        if (entity.target != null && Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange +1)
                        {
                            if (entity.target == GameManager.Instance.player)
                            {
                                GameManager.Instance.playerStats.DamagePlayer(enemyAttackDamages);
                            }
                            else
                            {
                                entity.target.GetComponent<Entity>().DamageEntity(enemyAttackDamages, false);
                            }
                        }
                        entity.entityNavMeshAgent.isStopped = false;
                        enemyIsAttacking = false;
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
                            entity.anm.SetTrigger("Attack");
                            timerDamageAnticipation = allyDamageAnticipationDuration;
                            allyIsAttacking = true;
                        }
                    }
                    if (timerDamageAnticipation >= 0)
                    {
                        timerDamageAnticipation -= Time.deltaTime;
                    }
                    else
                    {
                        if (allyIsAttacking == true)
                        {

                            entity.entityNavMeshAgent.isStopped = false;
                            allyIsAttacking = false;
                            if (firme.Length != 0)
                            {
                                for (int i = 0; i < firme.Length; i++)
                                {
                                    firme[i].GetComponent<Firme>().DamageFirme(allyAttackDamages);
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