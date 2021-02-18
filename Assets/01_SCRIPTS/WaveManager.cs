using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager
{
    [HideInInspector]
    public bool startWave;

    [HideInInspector]
    public int nbAllyEntities;
    [HideInInspector]
    public int nbNeutralEntities;
    [HideInInspector]
    public int nbEnemyEntities;

    [HideInInspector]
    public int waveindex;
    [HideInInspector]
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

    public void StartWave()
    {
        switch (GameManager.Instance.waveManager.waveindex)
        {
            case 1:
                for (int i = 0; i < GameManager.Instance.entitySpawner.corpoType.Length; i++)
                {
                    /*
                    for (int i = 0; i < length; i++)
                    {
                        GameManager.Instance.builder.ReplaceHousesByCorpo(GameManager.Instance.waveManager.waveindex);
                    }
                    */
                }
                break;

            case 2:
                for (int i = 0; i < GameManager.Instance.entitySpawner.corpoType.Length; i++)
                {
                    //corpoNbPerType[i];
                }
                break;

            case 3:
                for (int i = 0; i < GameManager.Instance.entitySpawner.corpoType.Length; i++)
                {
                    //corpoNbPerType[i];
                }
                break;

            case 4:
                for (int i = 0; i < GameManager.Instance.entitySpawner.corpoType.Length; i++)
                {
                    //corpoNbPerType[i];
                }
                break;

            case 5:
                for (int i = 0; i < GameManager.Instance.entitySpawner.corpoType.Length; i++)
                {
                    //corpoNbPerType[i];
                }
                break;
        }
    }

    public void Reset()
    {
        waveindex = 0;
        nbAllyEntities = 0;
        nbNeutralEntities = 0;
        nbEnemyEntities = 0;
    }

    public void IncreaseWaveIndex()
    {
        waveindex += 1;
    }
}
