﻿using System.Collections;
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

    public GameObject zoneFolder;
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
        GameManager.Instance.player.GetComponent<PlayerMovementController>().mustMovePlayer = true;

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

        foreach (GameObject noBaitZone in GameManager.Instance.builder.allNoBaitZones)
        {
            GameObject.Destroy(noBaitZone);
        }
        GameManager.Instance.builder.allNoBaitZones.Clear();

        GameManager.Instance.builder.SpawnSpawner();
        GameManager.Instance.builder.ReplaceShopByCorpo();

        if(UIManager.Instance.inventoryOpened == false && GameManager.Instance.builder.allNoBaitZones.Count > 0)
        {
            foreach (GameObject noBaitZone in GameManager.Instance.builder.allNoBaitZones)
            {
                noBaitZone.SetActive(false);
            }
        }
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

    void CheckEntityRatio()
    {
        if (nbEnemyEntities == GameManager.Instance.builder.waveStats[waveindex].nbMaxEnemyEntityOnMap)
        {
            for (int i = 0; i < zoneFolder.transform.childCount; i++)
            {
                zoneFolder.transform.GetChild(i).gameObject.SetActive(true);
            }

            UIManager.Instance.shop.AllShopsDetection();
            foreach (Collider shopinou in UIManager.Instance.shop.allShops)
            {
                shopinou.gameObject.GetComponent<Artisan>().UnactivateShop();
            }
            GameManager.Instance.EventLose();
        }

        if ( nbEntityInShops == GameManager.Instance.builder.waveStats[waveindex].nbMinAllyEntityInShop)
        {
            if ((waveindex + 1) >= GameManager.Instance.builder.waveStats.Count)
            {
                GameManager.Instance.EventWin();
            }
            else
            {
                UIManager.Instance.shop.hasNewBaitToAdd = true;
                GameManager.Instance.builder.RecallModifiedShops();
                GameManager.Instance.EventEndWave();
            }
        }
    }
}
