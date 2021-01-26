using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirmeBuilder
{
    public int nbFirmes;
    public int destroyedFirmes;
    public List<Transform> firmeLocation;
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
