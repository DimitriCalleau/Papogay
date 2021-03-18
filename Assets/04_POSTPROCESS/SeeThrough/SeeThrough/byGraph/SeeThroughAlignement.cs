using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughAlignement : MonoBehaviour
{
    public static int shaderPosID = Shader.PropertyToID("_Position");
    public static int shaderSizeID = Shader.PropertyToID("_Size");

    public Material mat;
    public Camera _camera;
    public LayerMask rayMaskLayer;
    RaycastHit raycastHit;

    void Update()
    {
        var dir = _camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        
        //if (Physics.Raycast(ray, Mathf.Infinity, rayMaskLayer))
        if (Physics.Raycast(_camera.transform.position, (transform.position - _camera.transform.position).normalized, Mathf.Infinity, rayMaskLayer))
        {
            mat.SetFloat(shaderSizeID, 1.5f);
        }
        else
        {
            mat.SetFloat(shaderSizeID, 0);
        }

        var view = _camera.WorldToViewportPoint(transform.position);
        mat.SetVector(shaderPosID, view);
    }
}