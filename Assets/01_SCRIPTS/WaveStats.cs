using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveStats 
{
    public int nbFirmesThisWave;
    public List<FirmeType> typesDeFirmes;
    public List<FirmeSize> firmeSize0Small1Medium2Big;
    public List<GameObject> firmeToSpawnPrefab;
    public int nbNeutralEntities;
    public int nbMaxEntity; 
}
