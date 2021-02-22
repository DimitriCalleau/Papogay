using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FirmeBuilder
{
    public List<WaveStats> waveStats;
    public GameObject pfb_Shop;

    GameObject[] smallHouses;
    GameObject[] bigHouses;
    [HideInInspector]
    public List<int> modifiedBigHouses; //Sauvegarde les index de bighouse des maisons modifiées
    [HideInInspector]
    public List<int> modifiedSmallHouses; //Sauvegarde les index de bighouse des maisons modifiées
    [HideInInspector]
    public List<Transform> allFirmesLocations;
    Vector3 firmeLocation, firmeRotation;
    int changeHouseIndex;

    int nbDestroyedFirmes, nbFirmesThisWave, firmeSpawnIndex;

    public List<GameObject> entityPrefabs;
    public int nbEntityMaxThisWave;
    public void ReplaceHousesBycorporations(int _waveIndex)
    {
        //Reset Arrays
        bigHouses = new GameObject[0];
        smallHouses = new GameObject[0];
        modifiedBigHouses.Clear();
        modifiedSmallHouses.Clear();
        allFirmesLocations.Clear();
        nbDestroyedFirmes = 0;
        firmeSpawnIndex = 0;

        //Find Houses
        int currentWaveIndex = GameManager.Instance.waveManager.waveindex;
        bigHouses = GameObject.FindGameObjectsWithTag("BigHouse");
        smallHouses = GameObject.FindGameObjectsWithTag("SmallHouse");
        nbFirmesThisWave = waveStats[currentWaveIndex].nbFirmesThisWave;
        nbEntityMaxThisWave = waveStats[currentWaveIndex].nbMaxEntity;

        for (int i = 0; i < nbFirmesThisWave; i++)
        {
            GameObject firmeToInstanciate = waveStats[currentWaveIndex].firmeToSpawnPrefab[i];
            BaitType firmeType = waveStats[currentWaveIndex].typesDeFirmes[i];
            int firmeSize = waveStats[currentWaveIndex].firmeSize[i];

            if(firmeSize == 2)//BigFirme
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
                firmeSpawnIndex += 1;
                newFirme.GetComponent<Firme>().corpoType = firmeType;
                newFirme.GetComponent<Firme>().modifiedHouseIndex = firmeSpawnIndex;
                newFirme.GetComponent<Firme>().firmeSize = firmeSize;
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
                firmeSpawnIndex += 1;
                newFirme.GetComponent<Firme>().corpoType = firmeType;
                newFirme.GetComponent<Firme>().modifiedHouseIndex = firmeSpawnIndex;
                newFirme.GetComponent<Firme>().firmeSize = firmeSize;
                allFirmesLocations.Add(newFirme.transform);
                modifiedSmallHouses.Add(changeHouseIndex);

            }
        }
    }

    public void RecallModifiedHouses(int _index, int _size)
    {
        nbDestroyedFirmes += 1;
        if (nbDestroyedFirmes < nbFirmesThisWave)
        {
            if(_size == 2)
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
            if (_size == 2)
            {
                GameObject shop = GameObject.Instantiate(pfb_Shop, bigHouses[modifiedBigHouses[_index]].transform.position, bigHouses[modifiedBigHouses[_index]].transform.rotation);
                GameObject.Destroy(bigHouses[modifiedBigHouses[_index]]);
                GameManager.Instance.EventEndWave();
            }
            else
            {
                GameObject shop = GameObject.Instantiate(pfb_Shop, smallHouses[modifiedSmallHouses[_index]].transform.position, smallHouses[modifiedSmallHouses[_index]].transform.rotation);
                GameObject.Destroy(smallHouses[modifiedSmallHouses[_index]]);
                GameManager.Instance.EventEndWave();
            }
        }
    }

    public GameObject SelectEntity(BaitType whichType)
    {
        GameObject selection = null;
        bool found = false;
        for (int b = 0; b < entityPrefabs.Count; b++)
        { 
            if(entityPrefabs[b].GetComponent<Entity>().type == whichType)
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
}