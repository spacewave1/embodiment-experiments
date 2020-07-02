using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : MonoBehaviour
{
    [SerializeField] private AvatarController _avatarController;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("incoming"))
        {
        }
    }
}
