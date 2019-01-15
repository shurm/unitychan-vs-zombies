using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCollisionHandler : MonoBehaviour
{
    public float damageDelay;

    private float damageDelayCopy;

    [Range(0,180)]
    public float facingPlayerAngleThreshold;

    public string TagOfTarget;

    private GeneralZombieBehavior zombieBehavior;

    //private NavMeshAgent navMeshAgent;
    public int damagePlayerTakes = 5;

    private bool currentlyBeingAttacked = false;

    private Animator anim;

    public bool CurrentlyBeingAttacked
    {
        get
        {
            return currentlyBeingAttacked;
        }

        set
        {
            currentlyBeingAttacked = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        zombieBehavior = GetComponent<GeneralZombieBehavior>();
        anim = GetComponent<Animator>();
        damageDelayCopy = damageDelay;
        damageDelay = 0;
    }

    void Update()
    {
        if (damageDelay > 0)
            damageDelay -= Time.deltaTime;
    }

    void HandleCollision(GameObject gameObject)
    {
        if (!(gameObject.CompareTag(TagOfTarget)))
            return;

        Health playersHealth = gameObject.GetComponentInParent<Health>();
       
        if (!currentlyBeingAttacked && !zombieBehavior.IsDead() && damageDelay <= 0 && IsFacingPlayer(gameObject))
        {
           anim.Play("attack");
           Debug.Log("damaged "+playersHealth.name);

           playersHealth.DealDamage(damagePlayerTakes);
           damageDelay = damageDelayCopy;
           
            // Debug.Log("colliding!!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    void OnTriggerStay(Collider other)
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

    //Should just care if zombie is facing player
    private bool IsFacingPlayer(GameObject player)
    {
   
        
        float facingAngleBetweenThisZombieAndPlayer = Vector3.Angle(transform.forward, player.transform.position - transform.position);

        //Debug.Log(facingPlayerAngleThreshold);
        if (facingAngleBetweenThisZombieAndPlayer < facingPlayerAngleThreshold)
        {
            return true;
        }
       
        return false;

    }
    
	
	
}
