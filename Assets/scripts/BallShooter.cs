﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Random = System.Random;

 public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector2 spreadVector2; 
        
    [SerializeField] private float shootForce = 1f;
    [SerializeField] private float shootIntervallInSeconds;
    [SerializeField] private float startOffsetInSeconds;
    [SerializeField] private int numberOfRepetitions;
    [SerializeField] private Transform target;
    [SerializeField] private Transform lauf;
    [SerializeField] private Vector3 pointAtOffset;
    [SerializeField] private GameObject ballsParent;

    public void Start()
    {
        StartCoroutine(en());
    }

    public void Update()
    {
        lauf.LookAt(target.position + pointAtOffset);
    }

    public void Shoot()
    {
        UnityEngine.Random rand = new UnityEngine.Random();
        float spreadX  = UnityEngine.Random.Range(-spreadVector2.x, spreadVector2.x);
        float spreadY  = UnityEngine.Random.Range(-spreadVector2.y, spreadVector2.y);
            
        GameObject go = Instantiate(ballPrefab,spawnPoint.transform.position, spawnPoint.transform.rotation, ballsParent.transform);
        go.GetComponent<Rigidbody>().AddForce((go.transform.forward + new Vector3(spreadX, spreadY)) * shootForce, ForceMode.VelocityChange);
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("Generate Nodes"))
        {
            Shoot();
        }
    }

    IEnumerator en()
    {
        yield return new WaitForSeconds(startOffsetInSeconds);
        while (numberOfRepetitions > 0)
        {
            Shoot();
            numberOfRepetitions--;
            yield return new WaitForSeconds(shootIntervallInSeconds);
        }
        Procedure.Instance.disableState();
    }
}
