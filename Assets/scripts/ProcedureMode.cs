using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProcedureMode : MonoBehaviour
{
    public GameObject avatar;
    public Vector3 offset;
    

    private void SetThirdPerson()
    {
        avatar.transform.position = new Vector3(avatar.transform.position.x + offset.x, avatar.transform.position.y + offset.z,
            avatar.transform.position.z + offset.z);
    }
}


public enum Perspective
{
    FIRST_PERSON,
    THIRD_PERSON
}

public enum Control
{
    IK_HANDS,
    TRIGGER_ANIMS
}

public enum Pose
{
    STATIC_POSE,
    DYNAMIC_POSES
}