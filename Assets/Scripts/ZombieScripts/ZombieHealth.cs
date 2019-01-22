using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{ 
    
    public int currentAttackTolerance;

    public float delayBetweenAttacksFromPlayer;

    private Transform target;
    private SceneDirector director;

    private NavMeshAgent agent;
    private Animator anim;

    private bool currentlyBeingAttacked = false;



    public float TimeSinceLastAttacked
    {
        get
        {
            return timeSinceLastAttacked;
        }

        set
        {
            timeSinceLastAttacked = value;
        }
    }

    private float timeSinceLastAttacked;
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

    void OnEnable()
    {
        timeSinceLastAttacked = 0;
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        ZombieEnvironmentVariables vars = GetComponent<ZombieEnvironmentVariables>();
        target = vars.target;
        director = vars.director;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("stunned"))
        {
            anim.SetBool("BeingAttacked", false);
            CurrentlyBeingAttacked = true;
        }

        if (CurrentlyBeingAttacked == true  && !anim.GetCurrentAnimatorStateInfo(0).IsName("stunned"))
        {
            agent.isStopped = false;
            CurrentlyBeingAttacked = false;

        }
        TimeSinceLastAttacked += Time.deltaTime;
    }
    public bool IsDead()
    {
        return currentAttackTolerance <= 0;
    }

    private void RemoveZombie()
    {
        director.RemoveRadarObject(gameObject);
        Destroy(gameObject);
    }

    public bool TakeDamage()
    {
        return TakeDamage(1);
    }

    public bool TakeDamage(int damage)
    {
        if (TimeSinceLastAttacked < delayBetweenAttacksFromPlayer)
            return false;

        //Debug.Log("take Damage called");
        currentAttackTolerance -= damage;
        agent.isStopped = true;
        CurrentlyBeingAttacked = true;
        timeSinceLastAttacked = 0;
        //Debug.Log("currentAttackTolerance is "+ currentAttackTolerance);
        if (currentAttackTolerance <= 0)
        {
            anim.SetBool("Dead", true);
            Invoke("RemoveZombie", 1.2f);
            return true;
        }
        else
        {
            anim.SetBool("BeingAttacked", true);
        }
        return false;
    }
}
