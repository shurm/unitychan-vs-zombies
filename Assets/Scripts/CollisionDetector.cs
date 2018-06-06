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
        //Debug.Log("collison detector for "+name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " colliding with " + other.gameObject.name);
        if (other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision(other.gameObject);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name +" colliding with "+other.gameObject.name);
        if (other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision(other.gameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        Debug.Log(gameObject.name + " colliding with " + other.gameObject.name);
        if (other.gameObject.CompareTag(TagOfTarget))
        {
            specialCollisionHandler.HandleCollision(other.gameObject);
        }
    }
    
}
