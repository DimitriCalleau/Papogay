using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FirmeBuilder
{
    BaitType _corpoType;

    int corpoNb, destroyedCorpo;
    Vector3[] corpoPerWave;
    GameObject[] modifiedHouses, housesTabl;
    public GameObject[] allCorpo;
    public GameObject pfb_Shop;

    public List<GameObject> bigFirmes;
    public List<GameObject> mediumFirmes;
    public List<GameObject> smallFirmes;

    public List<GameObject> smallHouses;
    public List<GameObject> bigHouses;
    public List<Transform> firmeLocation;

    public int nbFirmesMax;
    public int destroyedFirmes;

    public void CountDestroyedFirms(Transform destroyedFirmeTransform)
    {
        destroyedCorpo += 1;
        firmeLocation.Add(destroyedFirmeTransform);
    }
    
    public void ReplaceHousesByFirme(int _waveIndex)
    {
        destroyedCorpo = 0;

        housesTabl = GameObject.FindGameObjectsWithTag("Maisons");

        int nbSmallCorp = Mathf.RoundToInt(corpoPerWave[_waveIndex].x);
        int nbMediumCorp = Mathf.RoundToInt(corpoPerWave[_waveIndex].y);
        int nbBigCorp = Mathf.RoundToInt(corpoPerWave[_waveIndex].z);

        int corpoNb = Mathf.RoundToInt(nbSmallCorp + nbMediumCorp + nbBigCorp);
        modifiedHouses = new GameObject[corpoNb];

        int indexChangedHouses = 0;
        int _houseIndex = Random.Range(0, housesTabl.Length - 1);

        if (nbSmallCorp > 0)
        {
            for (int _x = 0; _x < nbSmallCorp; _x++)
            {
                if (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
                {
                    while (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
                    {
                        _houseIndex = Random.Range(0, housesTabl.Length - 1);
                    }
                }

                modifiedHouses[indexChangedHouses] = housesTabl[_houseIndex];
                modifiedHouses[indexChangedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexChangedHouses].SetActive(false);

                GameObject corpoToInstanciate = allCorpo[0];
                GameObject corpoInstance = GameObject.Instantiate(corpoToInstanciate, modifiedHouses[indexChangedHouses].transform.position, modifiedHouses[indexChangedHouses].transform.rotation);
                indexChangedHouses += 1;
            }
        }

        if (nbMediumCorp > 0)
        {
            if (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
            {
                while (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
                {
                    _houseIndex = Random.Range(0, housesTabl.Length - 1);
                }
            }

            for (int _y = 0; _y < nbSmallCorp; _y++)
            {
                modifiedHouses[indexChangedHouses] = housesTabl[_houseIndex];
                modifiedHouses[indexChangedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexChangedHouses].SetActive(false);

                GameObject corpoToInstanciate = allCorpo[1];
                GameObject corpoInstance = GameObject.Instantiate(corpoToInstanciate, modifiedHouses[indexChangedHouses].transform.position, modifiedHouses[indexChangedHouses].transform.rotation);
                indexChangedHouses += 1;
            }
        }

        if (nbBigCorp > 0)
        {
            if (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
            {
                while (housesTabl[_houseIndex].tag.Equals("ModifiedHouse"))
                {
                    _houseIndex = Random.Range(0, housesTabl.Length - 1);
                }
            }

            for (int _z = 0; _z < nbSmallCorp; _z++)
            {
                modifiedHouses[indexChangedHouses] = housesTabl[_houseIndex];
                modifiedHouses[indexChangedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexChangedHouses].SetActive(false);

                GameObject corpoToInstanciate = allCorpo[2];
                GameObject corpoInstance = GameObject.Instantiate(corpoToInstanciate, modifiedHouses[indexChangedHouses].transform.position, modifiedHouses[indexChangedHouses].transform.rotation);
                indexChangedHouses += 1;
            }
        }
    }

    public void RecallModifiedHouses(int _index)
    {
        destroyedCorpo += 1;
        if (destroyedCorpo < corpoNb)
        {
            modifiedHouses[_index].SetActive(true);
            modifiedHouses[_index].tag = "Maisons";
            modifiedHouses[_index] = null;
        }
        else
        {
            //_corpoType = ;
            GameObject shop = GameObject.Instantiate(pfb_Shop, modifiedHouses[_index].transform.position, modifiedHouses[_index].transform.rotation);
            UIManager.Instance.reward.AddLootType(_corpoType);
            GameObject.Destroy(modifiedHouses[_index]);
        }
    }
}