using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    EntityMovement entityMvt;

    public EntityStatus status;
    public BaitType type;
    public int health, healthMaxALLY, healthMinENM, damageAlly, damageEnm;
    public float moveSpeed, playerDetectionRadius, targetTreshold, RandomSelectorRadius, entityDamageCooldown, amazoonSpeedFactor;

    void Awake()
    {
        if (health <= healthMaxALLY)
        {
            status = EntityStatus.Ally;
        }
        else if (health >= healthMinENM)
        {
            status = EntityStatus.Enemy;
        }
        else if (health < healthMinENM && health > healthMaxALLY)
        {
            status = EntityStatus.Neutral;
        }
    }

    void FixedUpdate()
    {
        if (status == EntityStatus.Enemy)
        {
            switch (type)
            {
                case BaitType.Perfume:

                    break;
                case BaitType.Antenna:
                    break;
                case BaitType.Bar:
                    break;
                case BaitType.Threadmill:
                    break;
                case BaitType.Amazoon:
                    entityMvt.entityNavMeshAgent.speed = moveSpeed * amazoonSpeedFactor;
                    break;
            }
        }
    }

    public void DamageEntity(int _damage)
    {
        if (DamageTheEmn() == true)
        {
            health -= _damage;
        }
        else if (DamageTheEmn() == false)
        {
            health += _damage;
        }

        ChangeEntityStatus();
    }

    public bool DamageTheEmn()//if an enemy entity is attacked then this is true
    {
        if (status == EntityStatus.Enemy)
        {
            return true;
        }
        else
            return false;
    }

    public void ChangeEntityStatus()
    {
        if (health <= healthMaxALLY)
        {
            status = EntityStatus.Ally;
        }
        else if (health >= healthMinENM)
        {
            status = EntityStatus.Enemy;
        }
        else if (health < healthMinENM && health > healthMaxALLY)
        {
            status = EntityStatus.Neutral;
        }
    }

    public void Dead()
    {
        Destroy(this);
    }

    //Spawn Neutral
    //status = EntityStatus.Neutral
    //Spawn Enemy
    //status = EntityStatus.Enemy
    //Spawn Ally
    //status = EntityStatus.Ally
}