using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughCulling : MonoBehaviour
{
    public GameObject mainCamera, seeThroughSphere;
    public LayerMask seeThroughLayer;
    public float seeThroughScalingSpeed, defaultScale, aimedMaxScale, aimedMinScale;

    void Update()
    {
        RaycastHit raycastHit;
        
        if (Physics.Raycast(mainCamera.transform.position, (seeThroughSphere.transform.position - mainCamera.transform.position).normalized, out raycastHit, Mathf.Infinity, seeThroughLayer))
        {
            if (raycastHit.collider.gameObject.tag == "SeeThroughSphereMask")
            {
                DoScale(defaultScale, aimedMinScale);
            }
            else
            {
                DoScale(defaultScale, aimedMaxScale);
            }
        }
    }

    Vector3 DoScale(float origin, float aimedScale)
    {
        float aimedResult;

        origin = seeThroughSphere.transform.localScale.x;

        if (origin <= aimedScale)
        {
            origin += seeThroughScalingSpeed;

            aimedResult = origin;
        }
        else return seeThroughSphere.transform.localScale;

        return new Vector3(aimedResult, aimedResult, aimedResult);
    }
}
