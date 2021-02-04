using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityStatus status;
    public int healthMax, damageAlly, damageEnm;
    public float moveSpeed, playerDetectionRadius, targetTreshold, RandomSelectorRadius, entityDamageCooldown;

    //Spawn Neutral
    //status = EntityStatus.Neutral
    //Spawn Enemy
    //status = EntityStatus.Enemy
    //Spawn Ally
    //status = EntityStatus.Ally
}