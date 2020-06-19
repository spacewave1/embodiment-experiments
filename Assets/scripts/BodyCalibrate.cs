using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCalibrate : MonoBehaviour
{
    public Transform headmountAnchor;
    public Transform leftAnchor;
    public Transform rightAnchor;
    public Transform leftShoulder;
    public Transform leftHand;
    public Transform rightShoulder;
    public Transform rightHand;

    public Transform head;

    private float rightHandShoulderRelation;
    private float leftHandShoulderRelation;

    private float rightShoulderAnchorRelation;
    private float leftShoulderAnchorRelation;

    public bool synchronizeShoulderLenght;

    public IKControl ikControl;
    public bool enableMovementByCenterCamera;

    public Renderer avatarBody;

    public Transform resetPosition;
    public Transform roomPosition;

    [SerializeField] private AvatarController _avatarController;

    [SerializeField] private Perspective _perspective;
    [SerializeField] private Pose _pose;
    [SerializeField] private Control _control;
    [SerializeField] private GameObject cameraRig;

    public Pose Pose => _pose;
    public Control Control => _control;
    public Perspective Perspective => _perspective;

    // Start is called before the first frame update
    void Start()
    {
        if (_perspective == Perspective.THIRD_PERSON)
        {
            cameraRig.transform.Translate(-cameraRig.transform.right);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (synchronizeShoulderLenght)
        {
            syncShoulderLength();
        }


        _avatarController.UpdatePosition(headmountAnchor, enableMovementByCenterCamera);


        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            avatarBody.enabled = !avatarBody.enabled;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            enableMovementByCenterCamera = !enableMovementByCenterCamera;
        }

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            ikControl.ikActive = !ikControl.ikActive;
        }

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            roomPosition.position = resetPosition.position;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ||
            OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) ||
            OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //synchronizeShoulderLenght = !synchronizeShoulderLenght;
        }


        if (OVRInput.Get(OVRInput.Button.DpadUp))
        {
        }

        if (OVRInput.Get(OVRInput.Button.DpadUp))
        {
        }

        if (OVRInput.Get(OVRInput.Button.DpadUp))
        {
        }

        if (OVRInput.Get(OVRInput.Button.DpadDown))
        {
        }
    }

    void syncShoulderLength()
    {
        rightHandShoulderRelation = (rightShoulder.position - rightHand.position).magnitude;
        leftHandShoulderRelation = (leftShoulder.position - leftHand.position).magnitude;

        rightShoulderAnchorRelation = (rightShoulder.position - rightAnchor.position).magnitude;
        leftShoulderAnchorRelation = (leftShoulder.position - leftAnchor.position).magnitude;


        rightShoulder.localScale = new Vector3(rightShoulder.localScale.x,
            rightShoulder.localScale.y * (rightShoulderAnchorRelation / rightHandShoulderRelation),
            rightShoulder.localScale.z);
        leftShoulder.localScale = new Vector3(leftShoulder.localScale.x,
            leftShoulder.localScale.y * (leftShoulderAnchorRelation / leftHandShoulderRelation),
            leftShoulder.localScale.z);
    }
}