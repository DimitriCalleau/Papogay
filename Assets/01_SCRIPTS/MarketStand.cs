using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MarketStand : Baits
{
    public float attackRange;
    public Vector3 colliderSize, gatheringPoint;
    Vector3 turnedGatheringPoint;
    [HideInInspector]
    public Vector3 rotatedColliderSize;
    Collider[] Enemies;
    public Image ui_cooldownImage;
    GameObject francis;
    public void SetCollider()
    {
        offsetHeightCollider = colliderSize.y / 2;
        float offsetForwardAndHalf = offsetForwardCollider + (colliderSize.z / 2);
        Quaternion forwardRotation = Quaternion.Euler(0, UIManager.Instance.baitManager.baitRotation, 0);
        turnedGatheringPoint = transform.position +  forwardRotation * gatheringPoint;
        Vector3 forwardVector = forwardRotation * Vector3.forward * offsetForwardAndHalf;
        rotatedColliderSize = forwardRotation * colliderSize;
        rotatedColliderSize = new Vector3(Mathf.Sqrt(rotatedColliderSize.x * rotatedColliderSize.x), Mathf.Sqrt(rotatedColliderSize.y * rotatedColliderSize.y), Mathf.Sqrt(rotatedColliderSize.z * rotatedColliderSize.z));
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
      Enemies = Physics.OverlapBox(colliderCenter, rotatedColliderSize / 2, Quaternion.identity, ennemisMask);
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                if (e.GetComponent<Entity>().isAttracted == false)
                {
                    e.GetComponent<Entity>().AttractEntity(turnedGatheringPoint, usure);
                }
            }
        }
    }
    public void BaitAttack()
    {
        int nbTouchedEnemies = 0;
        foreach (Collider item in Enemies)
        {
            float enmDist = Vector3.Distance(turnedGatheringPoint, item.transform.position);
            if(enmDist <= attackRange)
            {
                item.GetComponent<Entity>().DamageEntity(damages[upgradeIndex], true);
                Debug.Log("Dans ta race");
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