using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveManager
{
    [HideInInspector]
    public bool startWave;

    [HideInInspector]
    public int nbEntityInShops;
    [HideInInspector]
    public int nbEnemyEntities;
    [HideInInspector]
    public int nbEntities;

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
                case EntityStatus.Enemy:
                    nbEnemyEntities += 1;
                    break;
                case EntityStatus.Ally:
                    nbEntityInShops += 1;
                    break;
                default:
                    break;
            }
        }
        else if (addOrRemove == false)
        {
            switch (status)
            {
                case EntityStatus.Enemy:
                    nbEnemyEntities -= 1;
                    break;
                case EntityStatus.Ally:
                    nbEntityInShops -= 1;
                    break;
                default:
                    break;
            }
        }
        if (nbEntities > 0)
        {
            float floatEnm = nbEnemyEntities;
            float floatAlly = nbEntityInShops;
            float floatMaxEnemy = GameManager.Instance.builder.waveStats[waveindex].nbMaxEnemyEntityOnMap;
            float floatMinAlly = GameManager.Instance.builder.waveStats[waveindex].nbMinAllyEntityInShop;

            UIManager.Instance.allyEntityBar.fillAmount = floatAlly / floatMaxEnemy;
            UIManager.Instance.enemyEntityBar.fillAmount = floatEnm / floatMinAlly;
            CheckEntityRatio();
        }
        else
        {
            UIManager.Instance.allyEntityBar.fillAmount = 0;
            UIManager.Instance.enemyEntityBar.fillAmount = 0;
        }
    }
    public void StartWave()
    {
        if(locationZones[waveindex] != null)
        {
            zoneIndex += 1;
            if (blocageZones[waveindex] != null)
            {
                blocageZones[waveindex].SetActive(false);
            }
            UIManager.Instance.shop.AllShopsDetection();
            foreach (Collider shopinou in UIManager.Instance.shop.allShops)
            {
                shopinou.gameObject.GetComponent<Artisan>().ActivateShop(zoneIndex);
            }
        }
        GameManager.Instance.builder.SpawnSpawner();
        GameManager.Instance.builder.ReplaceShopByCorpo();
    }

    public void Reset()
    {
        for (int i = 0; i < locationZones.Length; i++)
        {
            if (locationZones[i] != null)
            {
                locationZones[i].SetActive(false);
                if (blocageZones[i] != null)
                {
                    blocageZones[i].SetActive(true);
                }
            }
        }
        waveindex = 0;
        zoneIndex = 0;
        nbEntityInShops = 0;
        nbEnemyEntities = 0;
        UIManager.Instance.waveIndexIndicator.text = "Vague " + 1;
    }

    public void IncreaseWaveIndex()
    {
        waveindex += 1;
        UIManager.Instance.waveIndexIndicator.text = "Vague " + waveindex + 1;
    }

    public void CheckEntityRatio()
    {
        if( nbEntityInShops == GameManager.Instance.builder.waveStats[waveindex].nbMinAllyEntityInShop)
        {
            if(waveindex >= GameManager.Instance.builder.waveStats.Count)
            {
                GameManager.Instance.EventWin();
            }
            else
            {
                GameManager.Instance.builder.RecallModifiedShops();
                GameManager.Instance.EventEndWave();
            }
        }


        if (nbEnemyEntities == GameManager.Instance.builder.waveStats[waveindex].nbMaxEnemyEntityOnMap)
        {
            GameManager.Instance.EventLose();
        }
    }
}
