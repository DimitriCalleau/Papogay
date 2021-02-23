using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : Entity
{
    public void AttacksPlayer(int damages)
    {
        Debug.Log("attacks player void");
    }

    public void PlayerInAttackRange(float attackDistance, Vector3 playerPosition)
    {
        Debug.Log("player in attack range void");
    }
}