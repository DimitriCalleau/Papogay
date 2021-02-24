using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveStats 
{
    public int nbFirmesThisWave;
    public List<BaitType> typesDeFirmes;
    public List<int> firmeSize;
    public List<GameObject> firmeToSpawnPrefab;
    public int nbMaxEntity; 
}
