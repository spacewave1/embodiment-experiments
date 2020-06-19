using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using experiments;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public Text boxScoreText;
    public Text ballsScoreText;
    public Text jJacksCyclesText;

    public int boxHits = 0;
    public int tennisHits = 0;
    public int jumpingJackCycles = 0;

    private BodyCalibrate _bodyCalibrate;

    private void Start()
    {
        _bodyCalibrate = GetComponent<BodyCalibrate>();
    }

    public void ReceiveBallHit()
    {
        tennisHits++;
        ballsScoreText.text = tennisHits.ToString();
    }

    public void ProcessBoxHit(PunchingTarget target)
    {
        if (!target.IsHit)
        {
            boxHits++;
            boxScoreText.text = boxHits.ToString();
            target.IsHit = true;
            target.gameObject.SetActive(false);
        }
    }

    public void ProcessJumpingJackCycle()
    {
        jumpingJackCycles++;
        jJacksCyclesText.text = jumpingJackCycles.ToString();
    }

    public void UpdatePosition(Transform headmountAnchor, bool enableCameraMovement)
    {
        if (enableCameraMovement)
        {
            transform.position =
                new Vector3(headmountAnchor.position.x, transform.position.y, headmountAnchor.position.z);
        }
        
        if (_bodyCalibrate.Perspective == Perspective.THIRD_PERSON)
        {
            transform.position += transform.forward;
        }
    }

    public void UpdateIKComponents(Transform headLookTo, Transform rightHandPointTo, Transform leftHandPointTo,
        Animator animator)
    {
        // Set the look __target position__, if one has been assigned
        if (headLookTo != null)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(headLookTo.position + headLookTo.forward * 0.5f );
        }

        // Set the right hand target position and rotation, if one has been assigned
        if (rightHandPointTo != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

            switch (_bodyCalibrate.Perspective)
            {
                case Perspective.FIRST_PERSON:
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPointTo.position);
                    break;
                
                case Perspective.THIRD_PERSON:
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPointTo.position + Vector3.left );
                    break;
            }

            animator.SetIKRotation(AvatarIKGoal.RightHand,
                Quaternion.Euler(rightHandPointTo.rotation.eulerAngles.x,
                    rightHandPointTo.rotation.eulerAngles.y, rightHandPointTo.rotation.eulerAngles.z - 90));
        }

        if (leftHandPointTo != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            
            switch (_bodyCalibrate.Perspective)
            {
                case Perspective.FIRST_PERSON:
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPointTo.position);
                    break;
                
                case Perspective.THIRD_PERSON:
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPointTo.position + Vector3.left );
                    break;
            }
            
            
            animator.SetIKRotation(AvatarIKGoal.LeftHand,
                Quaternion.Euler(leftHandPointTo.rotation.eulerAngles.x,
                    leftHandPointTo.rotation.eulerAngles.y, leftHandPointTo.rotation.eulerAngles.z + 90));
        }
    }
}