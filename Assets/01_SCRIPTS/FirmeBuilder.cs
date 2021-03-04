using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class FirmeBuilder
{
    public List<WaveStats> waveStats;
    public GameObject pfb_Small_Shop, pfb_Big_Shop;

    public GameObject[] smallHouses;
    public GameObject[] bigHouses;
    [HideInInspector]
    public List<int> modifiedBigHouses; //Sauvegarde les index de bighouse des maisons modifiées
    [HideInInspector]
    public List<int> modifiedSmallHouses; //Sauvegarde les index de bighouse des maisons modifiées
    [HideInInspector]
    public List<GameObject> allShopsOnMap; //Sauvegarde les index de bighouse des maisons modifiées
    [HideInInspector]
    public List<Transform> allFirmesLocations;
    Vector3 firmeLocation, firmeRotation;
    int changeHouseIndex;

    int nbDestroyedFirmes, nbFirmesThisWave, smallFirmeSpawnIndex, bigFirmeSpawnIndex;

    public List<GameObject> entityPrefabs;
    public int nbEntityMaxThisWave;
    public void ReplaceHousesBycorporations(int _waveIndex)
    {
        int currentWaveIndex = GameManager.Instance.waveManager.waveindex;

        //Reset Arrays
        bigHouses = new GameObject[0];
        smallHouses = new GameObject[0];
        modifiedBigHouses.Clear();
        modifiedSmallHouses.Clear();
        allFirmesLocations.Clear();
        nbDestroyedFirmes = 0;
        smallFirmeSpawnIndex = 0;
        bigFirmeSpawnIndex = 0;

        //Find Houses

        bigHouses = GameObject.FindGameObjectsWithTag("BigHouse");
        smallHouses = GameObject.FindGameObjectsWithTag("SmallHouse");
        if(waveStats.Count > 0) {
            nbFirmesThisWave = waveStats[currentWaveIndex].nbFirmesThisWave;
            nbEntityMaxThisWave = waveStats[currentWaveIndex].nbMaxEntity;
        }


        for (int i = 0; i < nbFirmesThisWave; i++)
        {
            GameObject firmeToInstanciate = waveStats[currentWaveIndex].firmeToSpawnPrefab[i];
            FirmeType firmeType = waveStats[currentWaveIndex].typesDeFirmes[i];
            FirmeSize firmeSize = waveStats[currentWaveIndex].firmeSize0Small1Medium2Big[i];

            if(firmeSize == FirmeSize.Big)//BigFirme
            {
                changeHouseIndex = Random.Range(0, bigHouses.Length);
                if (modifiedBigHouses.Contains(changeHouseIndex))
                {
                    while (modifiedBigHouses.Contains(changeHouseIndex))
                    {
                        changeHouseIndex = Random.Range(0, bigHouses.Length);
                    }
                }
                firmeLocation = bigHouses[changeHouseIndex].transform.position;
                firmeRotation = bigHouses[changeHouseIndex].transform.eulerAngles;
                bigHouses[changeHouseIndex].SetActive(false);
                GameObject newFirme = GameObject.Instantiate(firmeToInstanciate, firmeLocation, Quaternion.Euler(firmeRotation));
                newFirme.GetComponent<Firme>().InitFirme(firmeType, firmeSize, bigFirmeSpawnIndex);
                bigFirmeSpawnIndex += 1;
                allFirmesLocations.Add(newFirme.transform);
                modifiedBigHouses.Add(changeHouseIndex);
            }
            else
            {
                changeHouseIndex = Random.Range(0, smallHouses.Length);
                if (modifiedSmallHouses.Contains(changeHouseIndex))
                {
                    while (modifiedSmallHouses.Contains(changeHouseIndex))
                    {
                        changeHouseIndex = Random.Range(0, smallHouses.Length);
                    }
                }
                firmeLocation = smallHouses[changeHouseIndex].transform.position;
                firmeRotation = smallHouses[changeHouseIndex].transform.eulerAngles;
                smallHouses[changeHouseIndex].SetActive(false);
                GameObject newFirme = GameObject.Instantiate(firmeToInstanciate, firmeLocation, Quaternion.Euler(firmeRotation));
                newFirme.GetComponent<Firme>().InitFirme(firmeType, firmeSize, smallFirmeSpawnIndex);
                smallFirmeSpawnIndex += 1;
                allFirmesLocations.Add(newFirme.transform);
                modifiedSmallHouses.Add(changeHouseIndex);
            }
        }

        //Spawn neutrals
        for (int i = 0; i < waveStats[currentWaveIndex].nbNeutralEntities; i++)
        {
            //Spawn Neutrals
            Vector3 spawnPoint = Vector3.zero;
            Vector3 randomDirection = Random.insideUnitSphere * 1000;
            NavMeshHit hit;
            Vector3 entitySpawnPoint = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, 1000, NavMesh.AllAreas))
            {
                spawnPoint = hit.position;
            }
            GameObject newNeutral = GameObject.Instantiate(GameManager.Instance.neutralEntityPrefab, spawnPoint, Quaternion.identity);
            newNeutral.GetComponent<Entity>().Init(50);
        }
    }

    public void RecallModifiedHouses(int _index, FirmeSize _size)
    {
        nbDestroyedFirmes += 1;
        if (nbDestroyedFirmes < nbFirmesThisWave)
        {
            if(_size == FirmeSize.Big)
            {
                bigHouses[modifiedBigHouses[_index]].SetActive(true);
            }
            else
            {
                smallHouses[modifiedSmallHouses[_index]].SetActive(true);
            }
        }
        else
        {
            if (GameManager.Instance.waveManager.waveindex >= (waveStats.Count - 1))
            {
                GameManager.Instance.EventWin();
            }
            if (_size == FirmeSize.Big)
            {
                GameObject shop = GameObject.Instantiate(pfb_Big_Shop, bigHouses[modifiedBigHouses[_index]].transform.position, bigHouses[modifiedBigHouses[_index]].transform.rotation);
                allShopsOnMap.Add(shop.gameObject);
                //GameObject.Destroy(bigHouses[modifiedBigHouses[_index]]);
            }
            else
            {
                GameObject shop = GameObject.Instantiate(pfb_Small_Shop, smallHouses[modifiedSmallHouses[_index]].transform.position, smallHouses[modifiedSmallHouses[_index]].transform.rotation);
                allShopsOnMap.Add(shop.gameObject);
                //GameObject.Destroy(smallHouses[modifiedSmallHouses[_index]]);
            }
            UIManager.Instance.shop.hasNewBaitToAdd = true;
            GameManager.Instance.EventEndWave();
        }
    }

    public GameObject SelectEntity(FirmeType whichType)
    {
        GameObject selection = null;
        bool found = false;
        for (int b = 0; b < entityPrefabs.Count; b++)
        {
            if (entityPrefabs[b].GetComponent<Entity>().type == whichType)
            {
                selection = entityPrefabs[b];
                found = true;
                break;
            }
        }
        if (found == true)
        {
            return selection;
        }
        else
            return null;
    }

    public void ResetShops()
    {
        foreach(GameObject shop in allShopsOnMap)
        {
            GameObject.Destroy(shop);
        }
    }
}