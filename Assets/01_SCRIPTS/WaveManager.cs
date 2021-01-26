using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager
{
    public bool startWave;

    public int nbAllyEntities;
    public int nbNeutralEntities;
    public int nbEnemyEntities;

    public void AddRemoveEntity(EntityStatus status, bool addOrRemove)
    {
        if(addOrRemove == true)
        {
            switch (status)
            {
                case EntityStatus.Neutral:
                    nbNeutralEntities += 1;
                    break;
                case EntityStatus.Enemy:
                    nbEnemyEntities += 1;
                    break;
                case EntityStatus.Ally:
                    nbAllyEntities += 1;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (status)
            {
                case EntityStatus.Neutral:
                    nbNeutralEntities -= 1;
                    break;
                case EntityStatus.Enemy:
                    nbEnemyEntities -= 1;
                    break;
                case EntityStatus.Ally:
                    nbAllyEntities -= 1;
                    break;
                default:
                    break;
            }
        }
    }
}
