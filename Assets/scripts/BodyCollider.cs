using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    private AvatarController _avatarController;

    private void Start()
    {
        _avatarController = GetComponentInParent<AvatarController>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("incoming"))
        {
            other.gameObject.tag = "Untagged";
           //_avatarController.ReceiveBallHit();
        }
    }
}
