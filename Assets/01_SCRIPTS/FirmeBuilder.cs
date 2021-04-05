﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class FirmeBuilder
{
    public List<GameObject> allBigFirmes;
    public List<GameObject> allSmallFirmes;

    public List<WaveStats> waveStats;
    public GameObject[] allShopsLocations;
    public GameObject[] buildShop;
    [HideInInspector]
    public GameObject[] spawnerLocations;

    public List<Transform> allFirmesLocations;
    Vector3 firmeLocation, firmeRotation;

    int nbFirmesThisWave;

    public List<GameObject> allNoBaitZones;
    public LayerMask locationLayer = -1;

    public GameObject entityPrefab; 
    public GameObject spawnerPrefab;
    public GameObject noBaitZonePrefab;

    public void SpawnSpawner()
    {
        spawnerLocations = GameObject.FindGameObjectsWithTag("SpawnerLocation");
        if (spawnerLocations.Length > 0)
        {
            for (int i = 0; i < spawnerLocations.Length; i++)
            {
                GameObject newSpawner = GameObject.Instantiate(spawnerPrefab, spawnerLocations[i].transform.position, spawnerLocations[i].transform.rotation);
                newSpawner.GetComponent<SpawnerNeutrals>().InitSpawn();
                GameObject newNoBaitZone = GameObject.Instantiate(noBaitZonePrefab, spawnerLocations[i].transform.position, Quaternion.Euler(new Vector3(noBaitZonePrefab.transform.eulerAngles.x, spawnerLocations[i].transform.eulerAngles.y, spawnerLocations[i].transform.eulerAngles.z)));
                newNoBaitZone.transform.localScale = newSpawner.GetComponent<SpawnerNeutrals>().noBaitZoneSize;
                allNoBaitZones.Add(newNoBaitZone);

                Collider[] closeLocations = Physics.OverlapBox(newNoBaitZone.transform.position, (newNoBaitZone.transform.localScale / 2), newNoBaitZone.transform.rotation, locationLayer);
                if (closeLocations.Length > 0)
                {
                    foreach (Collider selectedLocation in closeLocations)
                    {
                        selectedLocation.GetComponent<Location>().state = LocationState.NoBait;
                    }
                }
            }
        }
    }
    public void ReplaceShopByCorpo()
    {
        int currentWaveIndex = GameManager.Instance.waveManager.waveindex;

        //Reset Arrays
        allFirmesLocations.Clear();
        int firmeSpawnIndex = 0;

        if (waveStats.Count > 0)
        {
            nbFirmesThisWave = waveStats[currentWaveIndex].nbFirmesThisWave;
        }
        buildShop = GameObject.FindGameObjectsWithTag("Shop");


        for (int i = 0; i < nbFirmesThisWave; i++)
        {
            FirmeType firmeType = waveStats[currentWaveIndex].typesDeFirmes[i];
            GameObject firmeToInstanciate = null;
            if (buildShop[i].GetComponent<Artisan>().size == BuildingSize.Big)
            {
                firmeToInstanciate = pickBigFirmePrefab(firmeType);
            }
            else if(buildShop[i].GetComponent<Artisan>().size == BuildingSize.Small)
            {
                firmeToInstanciate = pickSmallFirmePrefab(firmeType);
            }

            firmeLocation = buildShop[i].transform.position;
            firmeRotation = buildShop[i].transform.eulerAngles;
            buildShop[i].SetActive(false);
            GameObject newFirme = GameObject.Instantiate(firmeToInstanciate, firmeLocation, Quaternion.Euler(firmeRotation));
            newFirme.GetComponent<Firme>().InitFirme(); 
            allFirmesLocations.Add(newFirme.transform);
            firmeSpawnIndex += 1;

            GameObject newNoBaitZone = GameObject.Instantiate(noBaitZonePrefab, firmeLocation, Quaternion.Euler(new Vector3(noBaitZonePrefab.transform.eulerAngles.x, firmeRotation.y, firmeRotation.z)));
            newNoBaitZone.transform.localScale = newFirme.GetComponent<Firme>().noBaitZoneSize;
            allNoBaitZones.Add(newNoBaitZone);

            Collider[] closeLocations = Physics.OverlapBox(newNoBaitZone.transform.position, (newNoBaitZone.transform.localScale / 2), newNoBaitZone.transform.rotation, locationLayer);
            if (closeLocations.Length > 0)
            {
                foreach (Collider selectedLocation in closeLocations)
                {
                    selectedLocation.GetComponent<Location>().state = LocationState.NoBait;
                }
            }
        }

        UIManager.Instance.shop.AllShopsDetection();
        foreach (Collider shopinou in UIManager.Instance.shop.allShops)
        {
            shopinou.gameObject.GetComponent<Artisan>().ActivateShop();
        }
        allShopsLocations = GameObject.FindGameObjectsWithTag("Shop");

    }

    public void RecallModifiedShops()
    {
        for (int i = 0; i < allShopsLocations.Length; i++)
        {
            allShopsLocations[i].SetActive(true);
        }
        for (int i = 0; i < buildShop.Length; i++)
        {
            buildShop[i].SetActive(true);
        }
    }

    public void ResetWaveShops()
    {
        for (int i = 0; i < allShopsLocations.Length; i++)
        {
            allShopsLocations[i].SetActive(true);
        }
        for (int i = 0; i < buildShop.Length; i++)
        {
            buildShop[i].SetActive(true);
        }
        UIManager.Instance.shop.AllShopsDetection();
        foreach (Collider shopinou in UIManager.Instance.shop.allShops)
        {
            shopinou.gameObject.GetComponent<Artisan>().UnactivateShop();
        }
    }

    public void OpenCloseNoBaitZones(bool openClose)//open = true, close = false
    {
        switch (openClose)
        {
            case true:
                foreach (GameObject noBaitZone in allNoBaitZones)
                {
                    noBaitZone.SetActive(true);
                }
                GameManager.Instance.EventUpdateLocationState();
                break;
            case false:
                foreach (GameObject noBaitZone in allNoBaitZones)
                {
                    noBaitZone.SetActive(false);
                }
                break;
        }
    }

    GameObject pickBigFirmePrefab(FirmeType whatType)
    {
        GameObject firmePrefabToReturn = null;

        for (int i = 0; i < allBigFirmes.Count; i++)
        {
            if(allBigFirmes[i].GetComponent<Firme>().corpoType == whatType)
            {
                firmePrefabToReturn = allBigFirmes[i];
                break;
            }
        }
        return firmePrefabToReturn;
    }
    GameObject pickSmallFirmePrefab(FirmeType whatType)
    {
        GameObject firmePrefabToReturn = null;

        for (int i = 0; i < allSmallFirmes.Count; i++)
        {
            if (allSmallFirmes[i].GetComponent<Firme>().corpoType == whatType)
            {
                firmePrefabToReturn = allSmallFirmes[i];
                break;
            }
        }
        return firmePrefabToReturn;
    }
}