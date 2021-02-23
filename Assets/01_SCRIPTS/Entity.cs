using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityStatus status;
    public CorpoType corpo;
    public int healthMax;
    public float moveSpeed;
    public float playerDetectionRadius;
    
    //Spawn Neutral
    //status = EntityStatus.Neutral
    //Spawn Enemy
    //status = EntityStatus.Enemy
    //Spawn Ally
    //status = EntityStatus.Ally
}