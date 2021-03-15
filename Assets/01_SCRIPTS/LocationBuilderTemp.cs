using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuilderTemp : MonoBehaviour
{
    public GameObject prefabLocation;
    public Vector2 basePosition;
    public Vector2 nbLigneEtColonnes;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        float xTransform = basePosition.x;
        float yTransform = basePosition.y;

        Vector3 instanceTransform;

        for (int i = 0; i < nbLigneEtColonnes.x; i++)
        {
            for (int j = 0; j < nbLigneEtColonnes.y; j++)
            {
                instanceTransform = new Vector3(xTransform, 0, yTransform);
                GameObject newLocation = Instantiate(prefabLocation, instanceTransform, Quaternion.identity);
                newLocation.transform.SetParent(this.transform);
                xTransform += offset;
            }
            xTransform = basePosition.x;
            yTransform += offset;
        }
    }
}
