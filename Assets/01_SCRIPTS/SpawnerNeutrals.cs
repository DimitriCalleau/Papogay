using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerNeutrals : MonoBehaviour
{
    public GameObject neutralEntities;
    public Transform spawnPosition;
    public float spawnRange, timeBetweenSpawns, timeBeforeDestruction;
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
                Spawn(ChooseSpawnPointEntity(spawnRange));
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
    void Spawn(Vector3 _spawnPoint)
    {
        GameObject newEntity = GameObject.Instantiate(neutralEntities, _spawnPoint, Quaternion.identity);
        newEntity.GetComponent<Entity>().Init(60);//0 = enm, 100 = ally, 50 = neutral
        spawnTimer = timeBetweenSpawns;
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
        else
        {
            entitySpawnPoint = spawnPosition.position;
        }
        return entitySpawnPoint;
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