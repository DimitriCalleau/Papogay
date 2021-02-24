using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bar : Baits
{
    public float range;
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
        Enemies = Physics.OverlapSphere(colliderCenter, range, ennemisMask);
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                e.gameObject.GetComponent<Entity>().DamageEntity(damages[upgradeIndex], true);
                countdown = 0;
                ui_cooldownImage.fillAmount = 0;
            }
        }
    }
}