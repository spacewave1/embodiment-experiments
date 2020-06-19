using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDodge : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            animator.SetTrigger(animationTrigger);
        }
    }
}
