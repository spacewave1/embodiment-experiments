using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCamera : MonoBehaviour
{
    [SerializeField] private Camera cameraToSyncWith;

    private Quaternion originRotation;

    [SerializeField] private X x_mapping;
    [SerializeField] private Y y_mapping;
    [SerializeField] private Z z_mapping;

    private void Start()
    {
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = originRotation.eulerAngles;
        
        switch (x_mapping)
        {
            case X.TO_X:
                newRotation += new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.x, 0, 0);
                break;
            
            case X.TO_Y:
                newRotation += new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.x, 0);
                break;

            case X.TO_Z:
                newRotation += new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.x);
                break;
            
            case X.TO_X_NEGATIVE:
                newRotation -= new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.x, 0,0);
                break;
            
            case X.TO_Y_NEGATIVE:
                newRotation -= new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.x, 0);
                break;

            case X.TO_Z_NEGATIVE:
                newRotation -= new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.x);
                break;
        }
        
        switch (y_mapping)
        {
            case Y.TO_X:
                newRotation += new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.y, 0, 0);
                break;
            
            case Y.TO_Y:
                newRotation += new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.y, 0);
                break;

            case Y.TO_Z:
                newRotation += new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.y);
                break;
            
            case Y.TO_X_NEGATIVE:
                newRotation -= new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.y, 0,0);
                break;
            
            case Y.TO_Y_NEGATIVE:
                newRotation -= new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.y, 0);
                break;

            case Y.TO_Z_NEGATIVE:
                newRotation -= new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.y);
                break;
        }
        
        switch (z_mapping)
        {
            case Z.TO_X:
                newRotation += new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.z, 0, 0);
                break;
            
            case Z.TO_Y:
                newRotation += new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.z, 0);
                break;

            case Z.TO_Z:
                newRotation += new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.z);
                break;
            
            case Z.TO_X_NEGATIVE:
                newRotation -= new Vector3(cameraToSyncWith.transform.localRotation.eulerAngles.z, 0,0);
                break;
            
            case Z.TO_Y_NEGATIVE:
                newRotation -= new Vector3(0, cameraToSyncWith.transform.localRotation.eulerAngles.z, 0);
                break;

            case Z.TO_Z_NEGATIVE:
                newRotation -= new Vector3(0, 0, cameraToSyncWith.transform.localRotation.eulerAngles.z);
                break;

        }

        transform.rotation = Quaternion.Euler(newRotation);
    }
}

enum X
{
    TO_X,
    TO_X_NEGATIVE,
    TO_Y,
    TO_Y_NEGATIVE,
    TO_Z,
    TO_Z_NEGATIVE
}

enum Y
{
    TO_X,
    TO_X_NEGATIVE,
    TO_Y,
    TO_Y_NEGATIVE,
    TO_Z,
    TO_Z_NEGATIVE
}

enum Z
{
    TO_X,
    TO_X_NEGATIVE,
    TO_Y,
    TO_Y_NEGATIVE,
    TO_Z,
    TO_Z_NEGATIVE
}
