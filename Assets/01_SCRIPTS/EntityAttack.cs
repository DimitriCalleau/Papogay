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
                    if (entity.target != null  && Vector3.Distance(transform.position, entity.target.transform.position) <= entityAttackRange)
                    {
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
                Debug.Log(entity.target);
                Debug.Log(entityAttackRange);
                if (entity.target != null)//link between destination and target
                {
                    Collider[] firme = Physics.OverlapSphere(transform.position + Vector3.up, entityAttackRange, allyAttackLayer);
                    if(firme.Length != 0)
                    {
                        for (int i = 0; i < firme.Length; i++)
                        {
                            Debug.Log(firme[i]);
                            firme[i].GetComponent<Firme>().DamageFirme(allyAttackDamages);
                            Destroy(gameObject);
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