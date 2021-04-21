using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Militant : Baits
{
    public LayerMask obstacleLayer = -1;
    public Vector3 colliderSize;
    Vector3 rotatedColliderSize, movementDirection, forwardVector;
    public float range;
    bool backOrForth;
    float baitRotation;
    public float speed;
    public void SetCollider()
    {
        colliderCenter = transform.position + Vector3.up * offsetHeightCollider;
    }
    public void SetObstacleCollider()
    {
        baitRotation = UIManager.Instance.baitManager.baitRotation;
        Debug.Log(baitRotation);
        offsetHeightCollider = colliderSize.y / 2;
        float offsetForwardAndHalf = offsetForwardCollider + (colliderSize.z / 2);
        Quaternion forwardRotation = Quaternion.Euler(0, baitRotation, 0);
        forwardVector = forwardRotation * Vector3.forward * offsetForwardAndHalf;
        movementDirection = forwardRotation * Vector3.forward;
        rotatedColliderSize = forwardRotation * colliderSize;
        rotatedColliderSize = new Vector3(Mathf.Sqrt(rotatedColliderSize.x * rotatedColliderSize.x), Mathf.Sqrt(rotatedColliderSize.y * rotatedColliderSize.y), Mathf.Sqrt(rotatedColliderSize.z * rotatedColliderSize.z));
        colliderCenter = transform.position + (Vector3.up * (rotatedColliderSize.y / 2)) + forwardVector; 
    }

    void TurnMilitant()
    {
        baitRotation += 180;

        offsetHeightCollider = colliderSize.y / 2;
        float offsetForwardAndHalf = offsetForwardCollider + (colliderSize.z / 2);
        Quaternion forwardRotation = Quaternion.Euler(0, baitRotation, 0);
        forwardVector = forwardRotation * Vector3.forward * offsetForwardAndHalf;
        movementDirection = forwardRotation * Vector3.forward;
        rotatedColliderSize = forwardRotation * colliderSize;
        rotatedColliderSize = new Vector3(Mathf.Sqrt(rotatedColliderSize.x * rotatedColliderSize.x), Mathf.Sqrt(rotatedColliderSize.y * rotatedColliderSize.y), Mathf.Sqrt(rotatedColliderSize.z * rotatedColliderSize.z));
        colliderCenter = transform.position + (Vector3.up * (rotatedColliderSize.y / 2)) + forwardVector;

        backOrForth = !backOrForth;
    }

    void Start()
    {
        SetCollider();
        SetObstacleCollider();
    }
    public void Update()
    {
        Detector();
        BackAndForthMovement();
        if (countdown >= cooldown[upgradeIndex])
        {
            BaitAttack();
        }
        else
        {
            countdown += Time.deltaTime;
        }
        LoseLife(Time.deltaTime);
    }
    public void BaitAttack()
    {
        Collider[] Enemies = Physics.OverlapSphere(colliderCenter, range, ennemisMask);
        if (Enemies.Length > 0)
        {
            foreach (Collider e in Enemies)
            {
                e.gameObject.GetComponent<Entity>().DamageEntity(damages[upgradeIndex], true);
                countdown = 0;
            }
        }
    }
    void Detector()
    {
        Collider[] Obstacle = Physics.OverlapBox(colliderCenter, rotatedColliderSize / 2, Quaternion.identity, obstacleLayer);
        if (Obstacle.Length > 0)
        {
            Debug.Log("nique");
            TurnMilitant();
        }
    }
    void BackAndForthMovement()
    {
        transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        colliderCenter = transform.position + (Vector3.up * ((rotatedColliderSize.y / 2) + 1)) + forwardVector;
    }

   
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(colliderCenter, rotatedColliderSize);
        Gizmos.color = Color.red;
        Vector3 billy = transform.position + movementDirection;
        Gizmos.DrawLine(transform.position, billy);
    }
}