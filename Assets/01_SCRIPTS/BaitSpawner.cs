using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpawner : MonoBehaviour
{
    GameObject prefabToSpawn;
    Mesh baitMesh;
    Location locationToSpawn;
    float timeBeforeSpawn;
    float spawnTimer;
    bool hasSpawned;
    public void InitSpawn(GameObject _baitprefab, Location _location)
    {
        prefabToSpawn = _baitprefab;
        locationToSpawn = _location;
        baitMesh = prefabToSpawn.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<MeshFilter>().mesh = baitMesh;
        timeBeforeSpawn = prefabToSpawn.GetComponent<Baits>().timeBeforeSpawn;
        spawnTimer = timeBeforeSpawn;
        hasSpawned = false;
    }

    void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            if(hasSpawned != true)
            {
                SpawnFinalBait();
            }
        }
    }

    public void SpawnFinalBait()
    {
        GameObject bait = GameObject.Instantiate(prefabToSpawn, locationToSpawn.transform.position, Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0));
        bait.GetComponent<Baits>().InitBait();
        bait.GetComponent<Baits>().location = locationToSpawn;
        hasSpawned = true;
        Destroy(this);
    }
}
