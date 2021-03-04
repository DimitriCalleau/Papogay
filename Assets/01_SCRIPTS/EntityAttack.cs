using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [HideInInspector]
    public Entity entity;
    float timerCooldownAttack, timerDamageAnticipation;
    public int allyAttackDamages, enemyAttackDamages; 
    public float entityDamageCooldown, entityAttackRange, entityStratAttackingRange, damageAnticipationDuration;
    bool isAttacking;
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
                    if (entity.target != null  && Vector3.Distance(transform.position, entity.target.transform.position) <= entityStratAttackingRange)
                    {
                        if (entity.target == GameManager.Instance.player)
                        {
                            entity.anm.SetTrigger("Attack");
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = damageAnticipationDuration;
                            isAttacking = true;
                        }
                        else
                        {
                            entity.anm.SetTrigger("Attack");
                            timerCooldownAttack = entityDamageCooldown;
                            timerDamageAnticipation = damageAnticipationDuration;
                            isAttacking = true;
                        }
                    }

                }

                if(timerDamageAnticipation >= 0)
                {
                    timerDamageAnticipation -= Time.deltaTime;
                }
                else
                {
                    if (isAttacking == true)
                    {
                        if (entity.target != null && Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange)
                        {
                            if (entity.target == GameManager.Instance.player)
                            {
                                GameManager.Instance.playerStats.DamagePlayer(enemyAttackDamages);
                                isAttacking = false;
                            }
                            else
                            {
                                entity.target.GetComponent<Entity>().DamageEntity(enemyAttackDamages, false);
                                isAttacking = false;
                            }
                        }
                    }
                }
                break;

            case EntityStatus.Ally:
                if (entity.target != null)//link between destination and target
                {
                    Collider[] firme = Physics.OverlapSphere(transform.position + Vector3.up, entityAttackRange, allyAttackLayer);
                    if(firme.Length != 0)
                    {
                        for (int i = 0; i < firme.Length; i++)
                        {
                            entity.Dead();
                            firme[i].GetComponent<Firme>().DamageFirme(allyAttackDamages);
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