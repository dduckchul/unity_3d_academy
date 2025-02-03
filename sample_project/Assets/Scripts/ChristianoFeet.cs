using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianoFeet : MonoBehaviour
{
    public GameObject christiano;
    
    private void OnCollisionEnter(Collision other)
    {
        // StopCoroutine(christiano.GetComponent<TreeInteraction>().movingToFeetCorutine);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(500, other.gameObject.transform.position, 30, 0, ForceMode.Impulse);
        }
    }
}
