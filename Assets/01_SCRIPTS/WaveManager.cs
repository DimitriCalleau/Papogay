using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public int nbEntities;

    [HideInInspector]
    public int waveindex;

    public GameObject[] locationZones;
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

        nbEntities = nbAllyEntities + nbNeutralEntities + nbEnemyEntities;
    }

    public void StartWave()
    {
        GameManager.Instance.builder.ReplaceHousesBycorporations(waveindex);
        if(locationZones[waveindex] != null)
        {
            locationZones[waveindex].SetActive(true);
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
