using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughAdaptation : MonoBehaviour
{
    public static int shaderPosID = Shader.PropertyToID("_Position");
    public static int shaderSizeID = Shader.PropertyToID("_Size");

    Material goMaterial;
    public Camera mainCamera;
    GameObject seeThroughSphere;
    public LayerMask seeThroughLayer;

    void Update()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(mainCamera.transform.position, (transform.position - mainCamera.transform.position).normalized, out raycastHit, Mathf.Infinity, seeThroughLayer))
        {
            if (raycastHit.collider.gameObject)
            {
                goMaterial = raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material;
                goMaterial.SetFloat(shaderSizeID, 1.5f);
            }
            else
            {
                goMaterial = raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material;
                goMaterial.SetFloat(shaderSizeID, 0);
            }
        }

        var view = mainCamera.WorldToViewportPoint(transform.position);
        goMaterial.SetVector(shaderPosID, view);
    }
}
