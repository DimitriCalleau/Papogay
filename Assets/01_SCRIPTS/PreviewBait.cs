using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PreviewBait
{
    GameObject preview_GO, rangePreview_GO, direction_GO;
    Mesh preview_Mesh;
    //public Mesh circleRangeMesh, boxRangeMesh;
    public GameObject rangeMeshPrefab, ui_Preview_Stats;
    public float offsetHeightStatsPreview;
    TextMeshProUGUI nbBaitsPreview;
    public Material canPlaceBait, cantPlaceBait, range_Mat, box_range_Mat, directional_Arrow;

    public void InitPreview()
    {
        preview_GO = new GameObject("Preview");
        rangePreview_GO = new GameObject("Range_Preview");
        direction_GO = new GameObject("Direction_Preview");

        GameObject statsPreview = GameObject.Instantiate(ui_Preview_Stats, preview_GO.transform.position + Vector3.up * offsetHeightStatsPreview, preview_GO.transform.rotation);
        statsPreview.transform.SetParent(preview_GO.transform);
        nbBaitsPreview = statsPreview.GetComponentInChildren<TextMeshProUGUI>();

        preview_GO.AddComponent<MeshFilter>();
        preview_GO.AddComponent<MeshRenderer>();
        preview_GO.GetComponent<MeshRenderer>().material = canPlaceBait;

        rangePreview_GO.AddComponent<MeshFilter>();
        rangePreview_GO.AddComponent<MeshRenderer>();
        rangePreview_GO.GetComponent<MeshRenderer>().material = range_Mat;
        Mesh boxRangeMesh = rangeMeshPrefab.GetComponent<MeshFilter>().sharedMesh;
        rangePreview_GO.GetComponent<MeshFilter>().mesh = boxRangeMesh;

        direction_GO.AddComponent<MeshFilter>();
        direction_GO.GetComponent<MeshFilter>().mesh = boxRangeMesh;
        direction_GO.AddComponent<MeshRenderer>();
        direction_GO.GetComponent<MeshRenderer>().material = directional_Arrow;
        direction_GO.transform.localScale = Vector3.one * 5f;
        direction_GO.transform.position = Vector3.forward * 2.5f;

        HidePreview(false);
    }
    public void HidePreview(bool hideUnhide)//true = unhide
    {
        switch (hideUnhide)
        {
            case true:
                preview_GO.SetActive(true);
                rangePreview_GO.SetActive(true);
                break;
            case false:
                preview_GO.SetActive(false);
                rangePreview_GO.SetActive(false);
                break;
        }
    }
    public void MovePreview(Location location, int rotation, Mesh _previewMesh)
    {
        if (UIManager.Instance.inventory.selection != null)
        {
            preview_Mesh = _previewMesh;
            preview_GO.GetComponent<MeshFilter>().mesh = preview_Mesh;
            nbBaitsPreview.text = UIManager.Instance.inventory.selection.nbBaits.ToString();

            if(UIManager.Instance.inventory.selection.nbBaits <= 0)
            {
                nbBaitsPreview.color = Color.red;
            }
            else
            {
                nbBaitsPreview.color = Color.white;
            }
        }
        if (location != null)
        {
            preview_GO.transform.position = location.transform.position;
            preview_GO.transform.rotation = Quaternion.Euler(0, rotation, 0);
            direction_GO.transform.position = location.transform.position + (Quaternion.Euler(0, rotation, 0) * new Vector3(10,10,0.05f));
            direction_GO.transform.rotation = Quaternion.Euler(90, rotation, 0);

            if (location.state == LocationState.NoBait)
            {
                rangePreview_GO.SetActive(false);
                preview_GO.GetComponent<MeshRenderer>().material = cantPlaceBait;
            }
            else
            {
                rangePreview_GO.SetActive(true);
                preview_GO.GetComponent<MeshRenderer>().material = canPlaceBait; 
            }
        }
    }
   /* public void SphereRangeDisplayer(Vector3 center, float range)
    {
        rangePreview_GO.transform.position = center;
        rangePreview_GO.transform.localScale = Vector3.one * (range * 2) * 0.1f;
        rangePreview_GO.GetComponent<MeshFilter>().mesh = circleRangeMesh;
    }
    public void BoxRangeDisplayer(Vector3 center, Vector3 boundaries)
    {
        boxRangeMesh = cubePrefab.GetComponent<MeshFilter>().sharedMesh;
        rangePreview_GO.transform.position = center;
        rangePreview_GO.transform.localScale = boundaries;
        rangePreview_GO.GetComponent<MeshFilter>().mesh = boxRangeMesh;
    }*/
    public void RangeDisplayer(Vector3 position, Vector3 boundaries, bool circleOrBox, bool showDIrection)
    {
        rangePreview_GO.transform.position = position;
        rangePreview_GO.transform.localEulerAngles = new Vector3(90, 0, 0);   

        switch (circleOrBox)
        {
            case true:
                rangePreview_GO.transform.localScale = new Vector3(2 * boundaries.y, 2 * boundaries.y, 0.5f);
                rangePreview_GO.GetComponent<MeshRenderer>().material = range_Mat;
                break;
            case false:
                Vector3 goodCollider;
                Quaternion forwardRotation = Quaternion.Euler(-90, 0, 0);
                goodCollider = forwardRotation * boundaries;
                rangePreview_GO.transform.localScale = goodCollider;
                rangePreview_GO.GetComponent<MeshRenderer>().material = box_range_Mat;
                break;
        }
        switch (showDIrection)
        {
            case true:
                direction_GO.SetActive(true);
                break;
            case false:
                direction_GO.SetActive(false);
                break;
        }
    }
}