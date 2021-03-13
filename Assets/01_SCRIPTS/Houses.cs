using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houses : MonoBehaviour
{
    public string houseTag;
    public int zone;
    public float timeBeforeRecall;//Equals to firme's timeBeforeDeath
    Animator anmHouse;
    float recallTimer;
    bool isRecalling;
    public void ActivateTag(int _zone)
    {
        if(zone == _zone)
        {
            gameObject.tag = houseTag;
        }
    }

    void Update()
    {
        if(recallTimer <= 0 && isRecalling == true)
        {
            RecallHouse();
        }
        else if (isRecalling == true && recallTimer > 0)
        {
            recallTimer -= Time.deltaTime;
        }
    }

    public void Unactivate()
    {
        gameObject.tag = "Untagged";
        gameObject.SetActive(true);
    }

    public void StartRecallingHouse()
    {
        recallTimer = timeBeforeRecall;
        isRecalling = true;

        GameObject childHouse = transform.GetChild(0).gameObject;

        anmHouse = childHouse.GetComponent<Animator>();
        anmHouse.SetBool("Recall", true);
    }

    void RecallHouse()
    {
        if(anmHouse != null)
        {
            anmHouse.SetBool("Recall", false);
        }
        isRecalling = false;
    }
}
