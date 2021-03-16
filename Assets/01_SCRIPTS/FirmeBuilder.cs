using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class FirmeBuilder
{
    public List<WaveStats> waveStats;
    [HideInInspector]
    public GameObject[] allShopsLocations;
    [HideInInspector]
    public GameObject[] spawnerLocations;

    public List<Transform> allFirmesLocations;
    public List<int> modifiedShops;
    Vector3 firmeLocation, firmeRotation;

    int nbFirmesThisWave, changeShopIndex;

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
        modifiedShops.Clear();
        int firmeSpawnIndex = 0;

        //Find Houses

        if (waveStats.Count > 0)
        {
            nbFirmesThisWave = waveStats[currentWaveIndex].nbFirmesThisWave;
        }
        allShopsLocations = GameObject.FindGameObjectsWithTag("Shop");

        for (int i = 0; i < nbFirmesThisWave; i++)
        {
            GameObject firmeToInstanciate = waveStats[currentWaveIndex].firmeToSpawnPrefab[i];
            FirmeType firmeType = waveStats[currentWaveIndex].typesDeFirmes[i];

            changeShopIndex = Random.Range(0, allShopsLocations.Length);
            if (modifiedShops.Contains(changeShopIndex))
            {
                while (modifiedShops.Contains(changeShopIndex))
                {
                    changeShopIndex = Random.Range(0, allShopsLocations.Length);
                }
            }
            firmeLocation = allShopsLocations[changeShopIndex].transform.position;
            firmeRotation = allShopsLocations[changeShopIndex].transform.eulerAngles;
            allShopsLocations[changeShopIndex].SetActive(false);
            GameObject newFirme = GameObject.Instantiate(firmeToInstanciate, firmeLocation, Quaternion.Euler(firmeRotation));
            newFirme.GetComponent<Firme>().InitFirme(firmeType, firmeSpawnIndex);
            firmeSpawnIndex += 1;
            allFirmesLocations.Add(newFirme.transform);
            modifiedShops.Add(changeShopIndex);
        }

    }

    public void RecallModifiedShops()
    {
        for (int i = 0; i < allShopsLocations.Length; i++)
        {
            allShopsLocations[i].SetActive(true);
        }
        UIManager.Instance.shop.hasNewBaitToAdd = true;
    }
}