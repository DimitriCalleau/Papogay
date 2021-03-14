using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntityBaseFX : MonoBehaviour
{
    public GameObject innerCircle, outerCircle;
    SpriteRenderer innerCircle_Sprite, outerCircle_Sprite;
    Vector3 innerScale, outerScale;
    float actualInnerScale, actualOuterScale;
    public Color innerAlpha;
    Color outerAlpha;
    
    public float alphaFactor, maxAlpha;

    [Header("Scale values")]
    public float innerScaleFactor;
    public float outerScaleFactor, minScale, maxScale;

    void Awake()
    {
        innerCircle_Sprite = innerCircle.GetComponent<SpriteRenderer>();
        outerCircle_Sprite = outerCircle.GetComponent<SpriteRenderer>();

        innerAlpha.a = 0;
        outerAlpha = innerAlpha;

        innerCircle_Sprite.color = innerAlpha;
        outerCircle_Sprite.color = outerAlpha;

        actualOuterScale = maxScale * 2;
    }

    void FixedUpdate()
    {
        innerAlpha = innerCircle_Sprite.color;
        outerAlpha = outerCircle_Sprite.color;
        
        if (outerAlpha.a <= maxAlpha && actualOuterScale > minScale)
        {
            innerAlpha.a = maxAlpha;
            outerAlpha.a += alphaFactor / 2;
        }
        else if (outerScale.x <= innerScale.x && innerAlpha.a <= 0.85f)
        {
            innerAlpha.a += alphaFactor / 16;
        }
        else if (innerAlpha.a >= 0.8f && actualOuterScale <= minScale)
        {
            outerAlpha.a -= alphaFactor;
        }

        innerCircle_Sprite.color = innerAlpha;
        outerCircle_Sprite.color = outerAlpha;

        if (actualOuterScale > minScale)
        {
            actualOuterScale -= outerScaleFactor;
        }

        innerCircle.transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        outerCircle.transform.localScale = new Vector3(actualOuterScale, actualOuterScale, actualOuterScale);
    }

    void DestroyAtEnd()
    {
        Destroy(innerCircle);
        Destroy(outerCircle);
    }
}