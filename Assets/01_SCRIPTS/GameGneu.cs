using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGneu : MonoBehaviour
{
    public List<GneuGneu> waveData;
    void Start()
    {
        for (int i = 0; i < waveData.Count; i++)
        {
            for (int f = 0; f < waveData[i].nbFirmesThisWave; f++)
            {
                Debug.Log("Firme de type : " + waveData[i].typesDeFirmes[f].ToString() + "de taille : " + waveData[i].firmeSize[f] + " et le max d'ennemi de la wave est de : " + waveData[i].nbMaxEntity);
            }
        }
    }
}
