using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreviewBait
{
    GameObject preview_GO, rangePreview_GO;
    Mesh preview_Mesh, range_Mesh;
    public GameObject spherePrefab, cubePrefab;
    public Material previewMat, range_Mat;

    public void InitPreview()
    {
        preview_GO = new GameObject("Preview");
        rangePreview_GO = new GameObject("Range_Preview");

        preview_GO.AddComponent<MeshFilter>();
        preview_GO.AddComponent<MeshRenderer>();
        preview_GO.GetComponent<MeshRenderer>().material = previewMat;

        rangePreview_GO.AddComponent<MeshFilter>();
        rangePreview_GO.AddComponent<MeshRenderer>();
        rangePreview_GO.GetComponent<MeshRenderer>().material = range_Mat;
    }
    public void HidePreview(bool hideUnhide)
    {
        switch (hideUnhide)
        {
            case true:
                preview_GO.SetActive(true);
                break;
            case false:
                preview_GO.SetActive(false);
                break;
        }
    }
    public void MovePreview(Location location, int rotation, Mesh _previewMesh)
    {
        if (UIManager.Instance.inventory.selection != null)
        {
            preview_Mesh = _previewMesh;
            preview_GO.GetComponent<MeshFilter>().mesh = preview_Mesh;
        }
        if (location != null)
        {
            preview_GO.transform.position = location.transform.position;
            preview_GO.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
    public void SphereRangeDisplayer(Vector3 center, float range)
    {
        range_Mesh = spherePrefab.GetComponent<MeshFilter>().sharedMesh;
        rangePreview_GO.transform.position = center;
        rangePreview_GO.transform.localScale = Vector3.one * (range * 2);
        rangePreview_GO.GetComponent<MeshFilter>().mesh = range_Mesh;
    }
    public void BoxRangeDisplayer(Vector3 center, Vector3 boundaries)
    {
        range_Mesh = cubePrefab.GetComponent<MeshFilter>().sharedMesh;
        rangePreview_GO.transform.position = center;
        rangePreview_GO.transform.localScale = boundaries;
        rangePreview_GO.GetComponent<MeshFilter>().mesh = range_Mesh;
    }
}