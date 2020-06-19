using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingJackZone : MonoBehaviour
{
    public bool handsInZone;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("glove"))
        {
            handsInZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("glove"))
        {
            handsInZone = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("glove"))
        {
            handsInZone = true;
        }
    }
}
