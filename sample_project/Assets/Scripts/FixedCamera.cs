using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public GameObject sphere;
    void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = sphere.transform.position + new Vector3(0,2,5);        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = sphere.transform.position + new Vector3(0,2,5);
    }
}
