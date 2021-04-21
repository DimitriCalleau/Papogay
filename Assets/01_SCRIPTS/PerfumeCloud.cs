using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfumeCloud : MonoBehaviour
{
    public LayerMask contaminableLayer;
    public int damages;
    public float transmissionRange, intervalBetweenDamagesTime, heightOffset;
    float perfumedTime, timer, damageTimer, timeForNextTransmition;
    bool hasTransmited;
    Entity attachedEntity;

    GameObject perfumeParticles;
    public void Init(float _parfumedTime, int _damages)
    {
        attachedEntity = GetComponentInParent<Entity>();
        transform.position = attachedEntity.transform.position + Vector3.up * heightOffset;
        attachedEntity.perfumed = true;
        Debug.Log(attachedEntity.name);
        perfumedTime = _parfumedTime;
        damages = _damages;
        timeForNextTransmition = perfumedTime / 2;

        timer = perfumedTime;
        damageTimer = intervalBetweenDamagesTime;
        perfumeParticles = transform.GetChild(0).gameObject;
        perfumeParticles.transform.localScale = Vector3.one * transmissionRange;
        perfumeParticles.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        if (timer > 0)
        {

            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                attachedEntity.DamageEntity(damages, true);
                damageTimer = intervalBetweenDamagesTime;
            }

            timer -= Time.deltaTime;
        }
        else
        {
            DestroyPerfume();
        }

        if(timeForNextTransmition > intervalBetweenDamagesTime && hasTransmited == false)
        {
            TransmitPerfume();
        }
    }
    void TransmitPerfume()
    {
        Collider[] EntityToTransmit = Physics.OverlapSphere(transform.position, transmissionRange, contaminableLayer);
        GameObject transmissionTarget = null;
        float mindist = Mathf.Infinity;
        if (EntityToTransmit.Length > 0)
        {
            for (int i = 0; i < EntityToTransmit.Length; i++)
            {
                if(EntityToTransmit[i].GetComponent<Entity>().perfumed == false)
                {
                    float dist = Vector3.Distance(attachedEntity.transform.position, EntityToTransmit[i].transform.position);
                    if (dist < mindist)
                    {
                        mindist = dist;
                        transmissionTarget = EntityToTransmit[i].gameObject;
                    }
                }
            }
        }

        if(transmissionTarget != null)
        {
            GameObject newCloud = Instantiate(UIManager.Instance.PerfumeCloudPrefab, transmissionTarget.transform);
            newCloud.GetComponent<PerfumeCloud>().Init(timeForNextTransmition, damages);
            hasTransmited = true;
        }
    }

    void DestroyPerfume()
    {
        attachedEntity.perfumed = false;
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, transmissionRange);
    }
}
