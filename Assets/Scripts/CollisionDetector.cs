using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public string[] attackingStateNames;
    
    private SpecialCollisionHandler specialCollisionHandler;

    private Animator animator;

    private bool collidedProperly = false;

	// Use this for initialization
	void Start ()
    {
        specialCollisionHandler = GetComponentInParent<SpecialCollisionHandler>();
        animator = GetComponentInParent<Animator>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(specialCollisionHandler.TagOfTarget))
        {
            //Debug.Log(gameObject.name + " colliding with " + other.gameObject.name);
            AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            foreach (string stateName in attackingStateNames)
            {
                if(currentAnimatorStateInfo.IsName(stateName))
                {
                    specialCollisionHandler.HandleStartOfCollision(other.gameObject);
                    collidedProperly = true;
                    return;
                }
            }      
        }
    }
   

        private void OnTriggerExit(Collider other)
    {
        if(collidedProperly)
        {
            specialCollisionHandler.HandleEndOfCollision(other.gameObject);
            collidedProperly = false;
        }
    }

}
