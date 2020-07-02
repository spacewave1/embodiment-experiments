using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class CustomPointer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public float lineLength = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit, lineLength, LayerMask.GetMask("UI")))
        {
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("button"))
            {
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) ||
                   OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    hit.collider.GetComponent<ButtonFunctions>().call();
                } else if (Input.GetKey(KeyCode.Space))
                {
                    hit.collider.GetComponent<ButtonFunctions>().call();
                }
            }
                
        }
        else
        {
            _lineRenderer.SetPosition(1,  transform.position + transform.forward * lineLength);
        }
        
        _lineRenderer.SetPosition(0, transform.position);
    }
}
