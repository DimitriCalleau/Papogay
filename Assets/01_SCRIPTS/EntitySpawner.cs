using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntitySpawner : MonoBehaviour
{
    WaveManager waveManager;
    GameObject entityToSpawn;

    [Header("FX Spawn")]
    public Animator anm;
    [Header("Delay")]
    public float timerSpawn;

    [HideInInspector]
    public BaitType[] corpoType = new BaitType[8];
    //amount of enemy by type
    [Header("      7 = ThreadMill")]
    [Header("      6 = Bar")]
    [Header("      5 = Antenna")]
    [Header("      4 = Perfume")]
    [Header("      3 = MarketStand")]
    [Header("      2 = Sign")]
    [Header("      1 = FruitBox")]
    [Header("      0 = PaperBoy")]
    [Header("CorpoType")]
    public int[] corpoNbPerType, entityNbPerType;

    void SpawnEntity(Vector3 _spawnPoint)
    {
        anm.SetTrigger("Spawn");
        waveManager.AddRemoveEntity(EntityStatus.Enemy, true);

        //make the entity spawner by selecting what amount of entityType spawn
        switch (GameManager.Instance.waveManager.waveindex)
        {
            case 1:

                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;
        }

        GameObject newEntity = GameObject.Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
    }

    Vector3 ChooseSpawnPoint(float _radius)
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * _radius;
        NavMeshHit hit;
        Vector3 spawnPoint = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, _radius, NavMesh.AllAreas))
        {
            spawnPoint = hit.position;
        }
        return spawnPoint;
    }
}