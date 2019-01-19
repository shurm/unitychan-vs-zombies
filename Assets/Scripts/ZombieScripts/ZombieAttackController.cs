using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void OnEnable()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        zombieHealth = GetComponent<ZombieHealth>();
        anim = GetComponent<Animator>();
        damageDelayCopy = damageDelay;
        //damageDelay = 0;
    }

    void Update()
    {
        if (damageDelay > 0)
            damageDelay -= Time.deltaTime;
    }

    void HandleCollision(GameObject gameObject)
    {
        //checks if the game the zombie collided with is the player
        if (!(gameObject.CompareTag(TagOfTarget)))
            return;

        Health playersHealth = gameObject.GetComponentInParent<Health>();

        //deal damage to the player only if this zombie is not being attacked by the player, this zombie is not dead (aka killed by the player already),
        //and is mostly facing the player
        if (!currentlyBeingAttacked && !zombieHealth.IsDead() && IsFacingPlayer(gameObject))
        {
            
            anim.SetBool("Attacking", true);
            if(damageDelay <= 0) 
            { 
                Debug.Log("damaged " + playersHealth.name);

                playersHealth.DealDamage(damagePlayerTakes);
                damageDelay = damageDelayCopy;

               // Debug.Log("damageDelay "+damageDelay);
            }
        }
        else
        {
            anim.SetBool("Attacking", false);
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
