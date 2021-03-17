using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveStats
{
    public int nbMinAllyEntityInShop;
    public int nbMaxEnemyEntityOnMap;
    public int nbFirmesThisWave;
    public List<FirmeType> typesDeFirmes;
}
