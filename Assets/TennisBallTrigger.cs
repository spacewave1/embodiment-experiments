using System;
using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class TennisBallTrigger : MonoBehaviour
{
    private GameObject triggerZone;
    public float distanceWhenToStart;
    public bool hasBeenTriggered = false;

    public void Start()
    {
        triggerZone = GameObject.Find("batHitZone");
    }

    public void Update()
    {
        if ((gameObject.transform.position - triggerZone.transform.position).magnitude < distanceWhenToStart)
        {
            if (!hasBeenTriggered)
            {
                triggerZone.GetComponent<Renderer>().enabled = true;
                triggerZone.GetComponent<Collider>().enabled = true;
                hasBeenTriggered = true;
            }
        }
        else
        {
            triggerZone.GetComponent<Renderer>().enabled = false;
            triggerZone.GetComponent<Collider>().enabled = false;
        }
    }
}
