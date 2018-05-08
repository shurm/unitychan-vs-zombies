using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTriggerListener : MonoBehaviour
{
    GameObject unitychan;

	// Use this for initialization
	void Start ()
    {
        unitychan = GameObject.Find("unitychan");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("zombie"))
        {

            Debug.Log("zombie kicked");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COllision");
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
