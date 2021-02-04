using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firme : MonoBehaviour
{
    public Animator anm;

    public BaitType firmeType;
    float health;
    int firmeIndex;

    public void DAmageFirme(int _damages)
    {
        health -= _damages;
    }

    void StartFirmeDestruction()
    {
        anm.SetBool("Destroy", true);
        UIManager.Instance.reward.AddLootType(firmeType);
    }

    void DestroyFirme()
    {
        Destroy(this.gameObject);
    }

    public void InitializeFirmeTest(int _type, int _index, WaveManager _waveManager, FirmeBuilder _testBuilder)
    {

    }
}