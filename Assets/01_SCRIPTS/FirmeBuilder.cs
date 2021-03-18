using System.Collections;
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

    public GameObject entityPrefab; 
    public GameObject spawnerPrefab;

    public void SpawnSpawner()
    {
        spawnerLocations = GameObject.FindGameObjectsWithTag("SpawnerLocation");
        if (spawnerLocations.Length > 0)
        {
            for (int i = 0; i < spawnerLocations.Length; i++)
            {
                GameObject newSpawner = GameObject.Instantiate(spawnerPrefab, spawnerLocations[i].transform.position, spawnerLocations[i].transform.rotation);
                newSpawner.GetComponent<SpawnerNeutrals>().InitSpawn();
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