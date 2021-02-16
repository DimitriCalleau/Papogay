using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MarketStand : Baits
{
    public float attackRange;
    public Vector3 colliderSize, gatheringPoint;
    [HideInInspector]
    public Vector3 rotatedColliderSize;
    Collider[] Enemies;
    public Image ui_cooldownImage;

    public void SetCollider()
    {
        offsetHeightCollider = colliderSize.y / 2;
        offSetForwardCollider = colliderSize.z / 2;
        Quaternion forwardRotation = Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0);
        Vector3 forwardVector = forwardRotation * Vector3.forward * offSetForwardCollider;
        rotatedColliderSize = forwardRotation * colliderSize;
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider + forwardVector;
    }
    void Start()
    {
        SetCollider();
    }
    public void Update()
    {
        Attract();
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
    public void Attract()
    {
        Enemies = Physics.OverlapBox(colliderCenter, rotatedColliderSize, Quaternion.identity, ennemisMask);
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                //Attarct Enemy
            }
        }
    }
    public void BaitAttack()
    {
        int nbTouchedEnemies = 0;
        foreach (Collider item in Enemies)
        {
            float enmDist = Vector3.Distance(gatheringPoint, item.transform.position);
            if(enmDist <= attackRange)
            {
                item.GetComponent<Entity>().DamageEntity(damages[upgradeIndex]);
                nbTouchedEnemies += 1;
            }
        }
        if(nbTouchedEnemies > 0)
        {
            countdown = 0;
            ui_cooldownImage.fillAmount = 0;
        }
    }
}