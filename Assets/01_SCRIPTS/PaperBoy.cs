using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class PaperBoy : Baits
{
    public float detectionRange, attackRange;
    GameObject target;
    public NavMeshAgent paperboyNav;
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
        GetTarget();
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
    void GetTarget()
    {
        Enemies = Physics.OverlapSphere(colliderCenter, detectionRange, ennemisMask);
        float minDist = Mathf.Infinity;
        if (Enemies.Length > 0)
        {
            if (target == null)
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    float dist = Vector3.Distance(transform.position, Enemies[i].transform.position);
                    if (minDist > dist)
                    {
                        target = Enemies[i].gameObject;
                        minDist = dist;
                    }
                }
            }
        }

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > detectionRange || target.layer == 12)
            {
                target = null;
                paperboyNav.isStopped = true;
                return;
            }
            else
            {
                paperboyNav.destination = target.transform.position;
                paperboyNav.isStopped = false;
            }
        }
    }
    public void BaitAttack()
    {
        if (target != null)
        {
            if (target.GetComponent<Entity>().status == EntityStatus.Ally)
            {
                target = null;
                return;
            }
            if (Vector3.Distance(target.transform.position, paperboyNav.transform.position) <= attackRange)
            {
                target.GetComponent<Entity>().DamageEntity(damages[upgradeIndex], true);
                countdown = 0;
                ui_cooldownImage.fillAmount = 0;
            }
        }
    }
}