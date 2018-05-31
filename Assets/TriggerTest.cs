using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float currentSpeed = Input.GetAxis("Horizontal");

        
        transform.position += transform.right * currentSpeed * Time.deltaTime;
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " is touching " + other.name);

        //other.is
    }

    void OnCollisionEnter(Collision other)
    {
       // Debug.Log(gameObject.name + " is collising " + other.gameObject.name);

        //other.is
    }
    */
}
