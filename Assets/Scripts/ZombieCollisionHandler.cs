using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollisionHandler : SpecialCollisionHandler
{
    public override void HandleCollision(GameObject gameObject)
    {
        gameObject.GetComponent<DamageManager>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
