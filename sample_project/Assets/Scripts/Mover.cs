using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Rigidbody rigid;
    public float movePower = 5;
    public float jumpPower = 5;
    
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(movePower * Vector3.right);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(movePower * Vector3.left);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(movePower * Vector3.back);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(movePower * Vector3.forward);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(jumpPower * Vector3.up, ForceMode.Impulse);
        }        
    }
}
