using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDisplay : MonoBehaviour
{
    public bool cum = false;
    public float Timer;
    public GameObject damagedObj;
    float timer;

    public Shader hitShader;
    Shader defaultShader;

    void Awake()
    {
        defaultShader = damagedObj.GetComponent<MeshRenderer>().material.shader;
    }

    void Update()
    {
        if (cum == true)
        {
            timer -= Time.deltaTime;
            damagedObj.GetComponent<MeshRenderer>().material.shader = hitShader;

            if (timer <= 0)
            {
                damagedObj.GetComponent<MeshRenderer>().material.shader = defaultShader;
                timer = Timer;
                cum = false;
            }
        }
    }
}
