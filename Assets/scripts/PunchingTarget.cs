using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace experiments
{
 
    public class PunchingTarget : MonoBehaviour
    {
        public String animationTriggerName;
        public Targets targets;
        private bool isHit = false;
        public Animator _animator;
        public AvatarController AvatarController;

        public bool IsHit
        {
            get { return isHit; }
            set { isHit = value; }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("glove"))
            {
                AvatarController.ProcessBoxHit(this);
                targets.flicker();
                if (_animator != null)
                {
                    _animator.SetTrigger(animationTriggerName);
                }
                else
                {
                    Debug.Log("Animator of target is null");
                }
            }
        }
    }
   

}