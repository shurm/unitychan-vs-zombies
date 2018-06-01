using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandleTest : SpecialCollisionHandler {
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void HandleCollision(GameObject gameObject)
    {
        Debug.Log("it works");
    }
}
