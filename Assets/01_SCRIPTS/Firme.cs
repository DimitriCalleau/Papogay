using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Firme : MonoBehaviour
{
    Animator anm;
    FirmeType corpoType;

    public int modifiedShopIndex;

    [Header("Spawner")]
    public int nbEntityToSpawn;
    GameObject entityToSpawn;
    public Image entityBar;

    public float timeBetweenSpawn, spawnRadius;
    float timerSpawn;
    bool canSpawn;

    [Header("Death")]
    public float timeBeforeDeath;

    public void InitFirme(FirmeType _firmeType, int _index)
    {
        corpoType = _firmeType;
        modifiedShopIndex = _index;

        anm = GetComponentInChildren<Animator>();

        canSpawn = true;
        timerSpawn = timeBetweenSpawn;

        nbEntityToSpawn = 0;
        entityToSpawn = GameManager.Instance.builder.entityPrefab;
    }
    void Update()
    {
        if (nbEntityToSpawn > 0 && entityToSpawn != null && canSpawn == true && timerSpawn <= 0)
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
        Vector3 entitySpawnPoint = new Vector3(0, -15.5f, -35);
        if (NavMesh.SamplePosition(randomDirection, out hit, _radius, NavMesh.AllAreas))
        {
            entitySpawnPoint = hit.position;
        }
        return entitySpawnPoint;
    }
    void SpawnEntity(Vector3 _spawnPoint)
    {
        GameObject newEntity = GameObject.Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
        newEntity.GetComponent<Entity>().Init(0);//0 = enm, 100 = ally, 50 = neutral
        timerSpawn = timeBetweenSpawn;
        anm.SetTrigger("Spawn");
        nbEntityToSpawn -= 1;
    }
    void StartCorpoDestruction()
    {
        canSpawn = false;
        GameManager.Instance.builder.allFirmesLocations.Remove(this.transform);
        anm.SetBool("Destroy", true);
        Destroy(this.gameObject, timeBeforeDeath);
    }

    public void GetNewEntity()
    {
        nbEntityToSpawn += 1;
    }

    void OnEnable()
    {
        GameManager.Instance.EndWave += StartCorpoDestruction;
    }   
    void OnDisable()
    {
        GameManager.Instance.EndWave -= StartCorpoDestruction;
    }
}