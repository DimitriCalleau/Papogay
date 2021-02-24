using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Firme : MonoBehaviour
{
    Animator anm;
    public BaitType corpoType;
    public float defaultHealth;
    float health;

    public int modifiedHouseIndex, firmeSize;

    [Header("Spawner")]
    GameObject entityToSpawn;
    public float timeBetweenSpawn, spawnRadius;
    float timerSpawn;
    public void InitFirme(BaitType _firmeType, int _size, int _index)
    {
        corpoType = _firmeType;
        firmeSize = _size;
        modifiedHouseIndex = _index;

        anm = GetComponentInChildren<Animator>();
        health = defaultHealth;
        timerSpawn = timeBetweenSpawn;
        entityToSpawn = GameManager.Instance.builder.SelectEntity(corpoType);
    }
    void Update()
    {
        if (timerSpawn <= 0 && entityToSpawn != null)
        {
            SpawnEntity(ChooseSpawnPointEntity(spawnRadius));
        }
        else
        {
            timerSpawn -= Time.deltaTime;
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
    void SpawnEntity(Vector3 _spawnPoint)
    {
        if(GameManager.Instance.waveManager.nbEntities < GameManager.Instance.builder.nbEntityMaxThisWave)
        {
            GameObject newEntity = GameObject.Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
            newEntity.GetComponent<Entity>().Init(100);//0 = ally, 100 = enm, 50 = neutral
            timerSpawn = timeBetweenSpawn;
            anm.SetTrigger("Spawn");
        }
    }
    public void DamageFirme(int _damages)
    {
        health -= _damages;
        anm.SetTrigger("Damages");
        if (health <= 0)
        {
            StartCorpoDestruction();
        }
    }

    void StartCorpoDestruction()
    {
        anm.SetBool("Destroy", true);
        //Should Wait For Destruction anim to end
        DestroyCorpo();
    }

    void DestroyCorpo()
    {
        GameManager.Instance.builder.RecallModifiedHouses(modifiedHouseIndex, firmeSize);
        Destroy(this.gameObject);
    }
}