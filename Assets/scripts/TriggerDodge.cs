using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDodge : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;
    [SerializeField] private string otherObjectTag;
    [SerializeField] private AvatarController _avatarController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(otherObjectTag))
        {
            _avatarController.ProcessBallHit();
            animator.SetTrigger(animationTrigger);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
