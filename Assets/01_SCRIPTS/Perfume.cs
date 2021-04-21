using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Perfume : Baits
{
    public float range, perfumeDuration;
    Collider[] Enemies;
    public Image ui_cooldownImage;

    public void SetCollider()
    {
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider;
    }
    void Start()
    {
        SetCollider();
    }
    public void Update()
    {
        if (countdown >= cooldown[upgradeIndex])
        {
            BaitAttack();
        }
        else
        {
            countdown += Time.deltaTime;
            ui_cooldownImage.fillAmount = countdown / cooldown[upgradeIndex];
        }
        LoseLife(Time.deltaTime);
    }
    public void BaitAttack()
    {
        Collider[] EntityToTransmit = Physics.OverlapSphere(transform.position, range, ennemisMask);
        GameObject transmissionTarget = null;
        float mindist = Mathf.Infinity;
        if (EntityToTransmit.Length > 0)
        {
            for (int i = 0; i < EntityToTransmit.Length; i++)
            {
                if (EntityToTransmit[i].GetComponent<Entity>().perfumed == false)
                {
                    float dist = Vector3.Distance(transform.position, EntityToTransmit[i].transform.position);
                    if (dist < mindist)
                    {
                        mindist = dist;
                        transmissionTarget = EntityToTransmit[i].gameObject;
                    }
                }
            }
        }

        if (transmissionTarget != null)
        {
            GameObject newCloud = Instantiate(UIManager.Instance.PerfumeCloudPrefab, transmissionTarget.transform);
            newCloud.GetComponent<PerfumeCloud>().Init(perfumeDuration, damages[upgradeIndex]);
            countdown = 0;
            Debug.Log(transmissionTarget);
        }
    }
}