using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : Entity
{
    EntityMovement entityMvt;

    float timerCooldownAttack;

    void Update()
    {
        if (entityMvt.CloseToTarget() == true)
        {
            switch (status)
            {
                case EntityStatus.Enemy:
                    if (timerCooldownAttack > 0)
                    {
                        timerCooldownAttack -= Time.deltaTime;
                    }
                    else if (timerCooldownAttack <= 0 && PlayerInAttackRange(targetTreshold, GameManager.Instance.player.transform.position))
                    {
                        timerCooldownAttack = entityDamageCooldown;
                        AttacksPlayer(damageEnm);
                    }
                    break;

                case EntityStatus.Ally:
                    if (timerCooldownAttack > 0)
                    {
                        timerCooldownAttack -= Time.deltaTime;
                    }
                    else if (timerCooldownAttack <= 0 && FirmeInAttackRange(targetTreshold, gameObject.GetComponent<EntityMovement>().destination))//link between destination and target
                    {
                        timerCooldownAttack = entityDamageCooldown;
                        AttacksFirme(damageAlly);
                    }
                    break;
            }
        }
    }

    public void AttacksPlayer(int damagesLOCAL)
    {
        Debug.Log("attacks player void " + damagesLOCAL);
    }

    public void AttacksFirme(int damagesLOCAL)
    {
        Debug.Log("attacks firme void " + damagesLOCAL);
    }

    public bool PlayerInAttackRange(float attackDistance, Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, playerPosition) <= attackDistance)
        {
            Debug.Log("player in attack range void");
            return true;
        }
        else
            return false;
    }

    public bool FirmeInAttackRange(float attackDistance, Vector3 firmePosition)
    {
        if (Vector3.Distance(transform.position, firmePosition) <= attackDistance)
        {
            Debug.Log("firme in attack range void");
            return true;
        }
        else
            return false;
    }
}