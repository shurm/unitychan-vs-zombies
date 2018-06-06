using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCollisionHandler : MonoBehaviour
{
    public int angleFaceing = 40;
    public string TagOfTarget;


    private NavMeshAgent navMeshAgent;
    private int damage = 1;
    void HandleCollision(GameObject gameObject)
    {
        if (!(gameObject.CompareTag(TagOfTarget)))
            return;

        Health playersHealth = gameObject.GetComponentInParent<Health>();
       
        if (!navMeshAgent.isStopped && facingEachOther(gameObject))
        {
            //anim.Play("attack");
           Debug.Log("damaged "+playersHealth.name);

           playersHealth.DealDamage(damage);
            //timeLeft = time;
            // Debug.Log("colliding!!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        HandleCollision(other.gameObject);
    }

    void OnCollisionStay(Collision other)
    {
        HandleCollision(other.gameObject);
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
