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
        else if (addOrRemove == false)
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

        /*Debug.Log("nb enemy : " + nbEnemyEntities);
        Debug.Log("nb ally : " + nbAllyEntities);
        Debug.Log("nb neutral : " + nbNeutralEntities);
        Debug.Log("nb entité totale : " + nbEntities);*/

        if (nbEntities > 0)
        {
            float floatEnm = nbEnemyEntities;
            float floatAlly = nbAllyEntities;
            float floatGeneral = GameManager.Instance.builder.nbEntityMaxThisWave;

            UIManager.Instance.allyEntityBar.fillAmount = floatAlly / floatGeneral;
            UIManager.Instance.enemyEntityBar.fillAmount = floatEnm / floatGeneral;
            CheckEntityRatio();
        }
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
        for (int i = 0; i < locationZones.Length; i++)
        {
            if (locationZones[i] != null)
            {
                locationZones[i].SetActive(false);
                for (int j = 0; j < GameManager.Instance.houseFolder.transform.childCount; j++)
                {
                    Transform tempSlect = GameManager.Instance.houseFolder.transform.GetChild(j);
                    tempSlect.GetComponent<Houses>().Unactivate();
                }
                if (blocageZones[i] != null)
                {
                    blocageZones[i].SetActive(true);
                }
            }
        }
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
        if(nbEnemyEntities == GameManager.Instance.builder.nbEntityMaxThisWave)
        {
            GameManager.Instance.EventLose();
        }
    }
}
