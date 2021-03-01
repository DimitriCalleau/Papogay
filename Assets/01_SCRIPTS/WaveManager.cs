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

    public LayerMask entitylayers = -1;

    [HideInInspector]
    public int waveindex;
    [HideInInspector]
    public int zoneIndex = 0;
    public GameObject[] locationZones;
    public GameObject[] blocageZones;
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

    public void RemoveCurrentEntities()
    {
        Collider[] currentEntitiesInScene = Physics.OverlapSphere(Vector3.zero, 1000, entitylayers);
        if(currentEntitiesInScene.Length != 0)
        {
            foreach(Collider e in currentEntitiesInScene)
            {
               GameObject.Destroy(e.gameObject);
            }
        }
        nbAllyEntities = 0;
        nbEnemyEntities = 0;
        nbNeutralEntities = 0;
        nbEntities = 0;
    }


    public void StartWave()
    {
        if(locationZones[waveindex] != null)
        {
            zoneIndex += 1;
            locationZones[waveindex].SetActive(true);
            for (int i = 0; i < GameManager.Instance.houseFolder.transform.childCount; i++)
            {
                Transform tempSlect = GameManager.Instance.houseFolder.transform.GetChild(i);
                tempSlect.GetComponent<Houses>().ActivateTag(zoneIndex);
            }
            if (blocageZones[waveindex] != null)
            {
                blocageZones[waveindex].SetActive(false);
            }
        }
        GameManager.Instance.builder.ReplaceHousesBycorporations(waveindex);
    }

    public void Reset()
    {
        waveindex = 0;
        zoneIndex = 0;
        nbAllyEntities = 0;
        nbNeutralEntities = 0;
        nbEnemyEntities = 0;
    }

    public void IncreaseWaveIndex()
    {
        waveindex += 1;
    }

    public void CheckEntityRatio()
    {
        if(nbEntities == nbEnemyEntities)
        {
            GameManager.Instance.EventLose();
        }
    }
}
