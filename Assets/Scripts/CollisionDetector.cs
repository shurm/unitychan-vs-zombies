using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    public string TagOfTarget;

    private SpecialCollisionHandler specialCollisionHandler;
	// Use this for initialization
	void Start ()
    {
        specialCollisionHandler = GetComponentInParent<SpecialCollisionHandler>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision();
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision();
        }
    }
}
