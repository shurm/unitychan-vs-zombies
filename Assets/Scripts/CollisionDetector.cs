using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public string[] attackingStateNames;
    
    private SpecialCollisionHandler specialCollisionHandler;

    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        specialCollisionHandler = GetComponentInParent<SpecialCollisionHandler>();
        animator = GetComponentInParent<Animator>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " colliding with " + other.gameObject.name);
        if (other.gameObject.CompareTag(specialCollisionHandler.TagOfTarget))
        {
            AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            foreach (string stateName in attackingStateNames)
            {
                if(currentAnimatorStateInfo.IsName(stateName))
                {
                    specialCollisionHandler.HandleCollision(other.gameObject);
                    return;
                }
            }      
        }
    }
   
}
