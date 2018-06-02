using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCollisionHandler : SpecialCollisionHandler
{
    public readonly int angleFaceing = 40;

    private NavMeshAgent navMeshAgent;
    private int damage = 4;
    public override void HandleCollision(GameObject gameObject)
    {
        gameObject.GetComponent<Health>();
        if (!navMeshAgent.isStopped && facingEachOther(gameObject))
        {
            //anim.Play("attack");

            gameObject.GetComponent<Health>().DealDamage(damage);
            //timeLeft = time;
            // Debug.Log("colliding!!");
        }
    }

    private bool facingEachOther(GameObject gameObject)
    {
        if (Mathf.Abs(Vector3.Angle(transform.forward, gameObject.transform.forward) - 180) < angleFaceing)
            return true;
        return false;

    }
    // Use this for initialization
    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
