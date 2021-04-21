using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaitSpawner : MonoBehaviour
{
    GameObject prefabToSpawn;
    Mesh baitMesh;
    Location locationToSpawn;
    float timeBeforeSpawn;
    float spawnTimer;
    float timerPercentage;
    bool hasSpawned;

    public Image timerImage;
    public TextMeshProUGUI timerText;
    public MeshFilter previewMeshFilter;
    public void InitSpawn(GameObject _baitprefab, Location _location)
    {
        prefabToSpawn = _baitprefab;
        locationToSpawn = _location;
        baitMesh = prefabToSpawn.GetComponent<MeshFilter>().sharedMesh;
        previewMeshFilter.mesh = baitMesh;
        timeBeforeSpawn = prefabToSpawn.GetComponent<Baits>().timeBeforeSpawn;
        spawnTimer = timeBeforeSpawn;
        hasSpawned = false;

        timerPercentage = spawnTimer;

        timerText.text = timerPercentage.ToString();
        timerImage.fillAmount = timerPercentage;
    }

    void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            timerPercentage = spawnTimer / timeBeforeSpawn;
            timerText.text = Mathf.CeilToInt(spawnTimer).ToString();
            timerImage.fillAmount = timerPercentage;
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
        bait.GetComponent<Baits>().location = locationToSpawn;
        hasSpawned = true;
        Destroy(this.gameObject);
    }
}
