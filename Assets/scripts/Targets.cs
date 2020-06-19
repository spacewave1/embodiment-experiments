using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using experiments;
using OVR.OpenVR;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Targets : MonoBehaviour
{
    public List<PunchingTarget> targets;

    [SerializeField] private float shootIntervallInSeconds;
    [SerializeField] private float startOffsetInSeconds;

    public float durancy;

    [SerializeField] private float targetActiveCount;
    [SerializeField] private float taskCount;

    void Start()
    {
        taskCount = 0;
        targetActiveCount = 0;
        targetActiveCount -= startOffsetInSeconds;
        targetActiveCount += shootIntervallInSeconds;
        taskCount -= startOffsetInSeconds;
        
    }

    private void Update()
    {
        targetActiveCount += Time.deltaTime;
        taskCount += Time.deltaTime;


        if (targetActiveCount > shootIntervallInSeconds)
        {
            flicker();
        }
        
        if (taskCount > durancy)
        {
            targets.ForEach(target => target.gameObject.SetActive(false));
            Procedure.Instance.disableState();
        }
    }

    public void flicker()
    {
        targets.ForEach(o => o.gameObject.SetActive(false));
        int rand = Random.Range(0, targets.Count);
        targets[rand].gameObject.SetActive(true);
        targets[rand].IsHit = false;
        targetActiveCount = 0;
    }
}