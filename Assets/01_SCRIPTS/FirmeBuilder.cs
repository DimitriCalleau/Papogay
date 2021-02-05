using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirmeBuilder
{
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
        destroyedFirmes += 1;
        firmeLocation.Add(destroyedFirmeTransform);
    }
    
    public void ReplaceHouseByFirme(int waveIndex)
    {
    }


    public void RecallModifiedHouses(int index)
    {
        
    }
}
