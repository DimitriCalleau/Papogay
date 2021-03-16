using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerNeutrals : MonoBehaviour
{
    public GameObject neutralEntities;
    public Transform spawnPosition;
    public float timeBetweenSpawns, timeBeforeDestruction;
    float spawnTimer;
    bool canSpawn;
    Animator neutralAnimator;

    public void InitSpawn()
    {
        canSpawn = true;
        neutralAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canSpawn)
        {
            if(spawnTimer <= 0)
            {
                Spawn();
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
    void Spawn()
    {
        GameObject newEntity = GameObject.Instantiate(neutralEntities, spawnPosition.position, Quaternion.identity);
        newEntity.GetComponent<Entity>().Init(50);//0 = enm, 100 = ally, 50 = neutral
        spawnTimer = timeBetweenSpawns;
    }

    void StartDestruction()
    {
        Destroy(this.gameObject, timeBeforeDestruction);
    }

    void OnEnable()
    {
        GameManager.Instance.EndWave += StartDestruction;
    }

    void OnDisable()
    {
        GameManager.Instance.EndWave -= StartDestruction;
    }
}
