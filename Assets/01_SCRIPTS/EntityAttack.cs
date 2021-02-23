using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [HideInInspector]
    public Entity entity;
    float timerCooldownAttack;
    public int allyAttackDamages, enemyAttackDamages; 
    public float entityDamageCooldown, entityAttackRange;
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
                    if (entity.target != null  && Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange)
                    {
                        Debug.Log(entity.target);
                        if (entity.target == GameManager.Instance.player)
                        {
                            GameManager.Instance.playerStats.DamagePlayer(enemyAttackDamages);
                            timerCooldownAttack = entityDamageCooldown;
                        }
                        else
                        {
                            entity.target.GetComponent<Entity>().DamageEntity(enemyAttackDamages, true);
                            timerCooldownAttack = entityDamageCooldown;
                        }
                    }

                }
                break;

            case EntityStatus.Ally:
                if (timerCooldownAttack > 0)
                {
                    timerCooldownAttack -= Time.deltaTime;
                }
                else if (timerCooldownAttack <= 0 && entity.target != null)//link between destination and target
                {
                    if (Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange)
                    {
                        timerCooldownAttack = entityDamageCooldown;
                        entity.target.GetComponent<Firme>().DamageFirme(allyAttackDamages);
                    }
                }
                break;
        }
    }
}