using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : Entity
{
    void Update()
    {
        Move(moveSpeed, EntityStatus.Enemy, PlayerInRange(playerDetectionRadius, GameManager.Instance.player.transform.position));
    }

    public void Move(float moveSpeed, EntityStatus status, bool attacksPlayer)
    {
        
    }

    public bool PlayerInRange(float detectionDistance, Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, playerPosition) <= detectionDistance)
        {
            return true;
        }
        else
            return false;
    }
}