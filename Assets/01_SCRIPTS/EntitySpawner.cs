using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    WaveManager waveManager;
    GameObject entityToSpawn;
    GameManager gamemanager;

    [Header("Delay")]
    public float timerSpawn;

    [HideInInspector]
    public BaitType[] corpoType = new BaitType[8];

    void SpawnEntity(Vector3 _spawnPoint)
    {
        gamemanager.anm.SetTrigger("Spawn");
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
}