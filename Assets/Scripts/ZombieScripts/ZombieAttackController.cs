using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackController : MonoBehaviour
{
    public float damageDelay;

    private float damageDelayCopy;

    [Range(0, 180)]
    public float facingPlayerAngleThreshold;

    public string TagOfTarget;

    private ZombieHealth zombieHealth;
    

    //private NavMeshAgent navMeshAgent;
    public int damagePlayerTakes = 5;
   
    private Animator anim;

    private bool currentlyAttacking = false;


    public bool CurrentlyAttacking
    {
        get
        {
            return currentlyAttacking;
        }

        set
        {
            currentlyAttacking = value;
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        zombieHealth = GetComponent<ZombieHealth>();
        anim = GetComponent<Animator>();
        damageDelayCopy = damageDelay;

    }

    void Update()
    {
        if (damageDelay > 0)
            damageDelay -= Time.deltaTime;

  
    }
       
    private void LateUpdate()
    {
            anim.SetBool("Attacking", false);

        
    }

    void HandleCollision(GameObject gameObject)
    {
        //checks if the gameobject the zombie collided with is the player
        if (!(gameObject.CompareTag(TagOfTarget)))
            return;

        CheckForAnimation attackAnimationCheckScript = gameObject.GetComponent<CheckForAnimation>();
        if (attackAnimationCheckScript != null && gameObject.GetComponent<CheckForAnimation>().CheckForAttackAnimation())
        {
            zombieHealth.TakeDamage();
            return;
        }
        Debug.Log(gameObject.name + " collided with " + name);

        Health playersHealth = gameObject.GetComponentInParent<Health>();

        //deal damage to the player only if this zombie is not being attacked by the player, this zombie is not dead (aka killed by the player already),
        //and is mostly facing the player
        if (!zombieHealth.CurrentlyBeingAttacked && !zombieHealth.IsDead() && IsFacingPlayer(gameObject))
        {
            
            anim.SetBool("Attacking", true);
            CurrentlyAttacking = true;
            if (damageDelay <= 0) 
            { 
               // Debug.Log("damaged " + playersHealth.name);

                playersHealth.DealDamage(damagePlayerTakes);
                damageDelay = damageDelayCopy;
            }
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
    



    //Should just care if zombie is just facing player
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
