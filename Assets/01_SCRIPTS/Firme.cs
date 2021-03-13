using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Firme : MonoBehaviour
{
    Animator anm;
    FirmeType corpoType;
    FirmeSize firmeSize;
    public float defaultHealth;
    float health;
    public Image healthbar;

    public int modifiedHouseIndex;

    [Header("Spawner")]
    GameObject entityToSpawn;
    public float timeBetweenSpawn, spawnRadius;
    float timerSpawn;
    bool canSpawn;

    [Header("Death")]
    public float timeBeforeDeath;

    [Header("VFX")]
    public ParticleSystem damagedParticles;
    public void InitFirme(FirmeType _firmeType, FirmeSize _size, int _index)
    {
        corpoType = _firmeType;
        firmeSize = _size;
        modifiedHouseIndex = _index;

        anm = GetComponentInChildren<Animator>();
        health = defaultHealth;
        healthbar.fillAmount = (health / defaultHealth);
        timerSpawn = timeBetweenSpawn;
        entityToSpawn = GameManager.Instance.builder.SelectEntity(corpoType);
        canSpawn = true;
    }
    void Update()
    {
        if (timerSpawn <= 0 && entityToSpawn != null && canSpawn == true)
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
        if(GameManager.Instance.waveManager.nbEntities < GameManager.Instance.builder.nbEntityMaxThisWave)
        {
            GameObject newEntity = GameObject.Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
            newEntity.GetComponent<Entity>().Init(0);//0 = enm, 100 = ally, 50 = neutral
            timerSpawn = timeBetweenSpawn;
            anm.SetTrigger("Spawn");
        }
    }
    public void DamageFirme(int _damages)
    {
        health -= _damages;
        healthbar.fillAmount = (health / defaultHealth);
        anm.SetTrigger("Damages");
        damagedParticles.Play();
        if (health <= 0 && canSpawn == true)
        {
            StartCorpoDestruction();
        }
    }

    void StartCorpoDestruction()
    {
        canSpawn = false;
        GameManager.Instance.builder.allFirmesLocations.Remove(this.transform);
        anm.SetBool("Destroy", true);
        Destroy(this.gameObject, timeBeforeDeath);
        GameManager.Instance.builder.RecallModifiedHouses(modifiedHouseIndex, firmeSize);
    }

    //for safety
    void DestroyCorpo()
    {
        Destroy(this.gameObject);
    }

    void OnEnable()
    {
        GameManager.Instance.CleanMap += DestroyCorpo;
    }   
    void OnDisable()
    {
        GameManager.Instance.CleanMap -= DestroyCorpo;
    }
}