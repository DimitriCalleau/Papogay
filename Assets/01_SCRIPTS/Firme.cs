using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firme : MonoBehaviour
{
    Animator anm;
    WaveManager waveManager;
    public BaitType corpoType;
    public float defaultHealth;
    float health;
    int firmeIndex;
    bool destruction;

    void Awake()
    {
        anm = GetComponentInChildren<Animator>();
        health = defaultHealth;
    }

    public void DamageFirme(int _damages)
    {
        health -= _damages;

        if (health <= 0)
        {
            StartCorpoDestruction();
        }
    }

    void StartCorpoDestruction()
    {
        anm.SetBool("Destroy", true);
        UIManager.Instance.reward.AddLootType(corpoType);
        if (anm.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))//check if this is good when the corporation thing is set, need animator called "Destroy"
        {
            DestroyCorpo();
        }
    }

    void DestroyCorpo()
    {
        Destroy(this.gameObject);
    }
}