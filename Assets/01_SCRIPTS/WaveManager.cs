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

    public GameObject[] zones;
    public GameObject[] blocageZones;
    public Vector3[] playerStartPositions;
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
                    break;            }
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
            }
        }

        if (nbEntityInShops > 0)
        {
            float floatAlly = nbEntityInShops;

            float floatMinAlly = GameManager.Instance.builder.waveStats[waveindex].nbMinAllyEntityInShop;

            UIManager.Instance.allyEntityBar.fillAmount = floatAlly / floatMinAlly;
            CheckEntityRatio();
        }
        else
        {
            UIManager.Instance.allyEntityBar.fillAmount = 0;
        }

        if(nbEnemyEntities > 0)
        {
            float floatEnm = nbEnemyEntities;

            float floatMaxEnemy = GameManager.Instance.builder.waveStats[waveindex].nbMaxEnemyEntityOnMap;

            UIManager.Instance.enemyEntityBar.fillAmount = floatEnm / floatMaxEnemy;

            CheckEntityRatio();
        }
        else
        {
            UIManager.Instance.enemyEntityBar.fillAmount = 0;
        }
    }
    public void StartWave()
    {
        nbEntityInShops = 0;
        nbEnemyEntities = 0;

        UIManager.Instance.enemyEntityBar.fillAmount = 0;
        UIManager.Instance.allyEntityBar.fillAmount = 0;

        GameManager.Instance.playerStartPosition = playerStartPositions[waveindex];

        if (zones[waveindex] != null)
        {

            for (int i = 0; i < zones.Length; i++)
            {
                if(i == waveindex)
                {
                    zones[i].SetActive(true);
                }
                else
                {
                    zones[i].SetActive(false); 
                }
            }
        }

        if(blocageZones[waveindex] != null)
        {
            for (int i = 0; i < blocageZones.Length; i++)
            {
                if (i == waveindex)
                {
                    if (blocageZones[i] != null)
                    {
                        blocageZones[i].SetActive(false);
                    }
                }
                else
                {
                    if (blocageZones[i] != null)
                    {
                        blocageZones[i].SetActive(true);
                    }
                }
            }
        }
        GameManager.Instance.builder.SpawnSpawner();
        GameManager.Instance.builder.ReplaceShopByCorpo();
    }

    public void Reset()
    {
        waveindex = 0;

        UIManager.Instance.waveIndexIndicator.text = "Vague " + 1;
    }

    public void IncreaseWaveIndex()
    {
        waveindex += 1;
        UIManager.Instance.waveIndexIndicator.text = "Vague " + (waveindex + 1);
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
            UIManager.Instance.shop.AllShopsDetection();
            foreach (Collider shopinou in UIManager.Instance.shop.allShops)
            {
                shopinou.gameObject.GetComponent<Artisan>().UnactivateShop();
            }
            GameManager.Instance.EventLose();
        }
    }
}
