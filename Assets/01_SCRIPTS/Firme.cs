using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public Vector3 ChooseSpawnPointEntity(float _radius)
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * _radius;
        NavMeshHit hit;
        Vector3 entitySpawnPoint = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, _radius, NavMesh.AllAreas))
        {
            entitySpawnPoint = hit.position;
        }
        return entitySpawnPoint;
    }

    void StartCorpoDestruction()
    {
        anm.SetBool("Destroy", true);
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