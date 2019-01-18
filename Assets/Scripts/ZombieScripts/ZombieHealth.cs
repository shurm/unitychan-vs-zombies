using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{ 
    
    public int currentAttackTolerance;

    private Transform target;
    private SceneDirector director;

    private NavMeshAgent agent;
    private Animator anim;

    private bool wasAttacked = false;
    void OnEnable()
    {
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
            wasAttacked = true;
        }

        if (wasAttacked == true  && !anim.GetCurrentAnimatorStateInfo(0).IsName("stunned"))
        {
            agent.isStopped = false;
            wasAttacked = false;

        }
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
        Debug.Log("take Damage called");
        currentAttackTolerance -= damage;
        agent.isStopped = true;

        Debug.Log("currentAttackTolerance is "+ currentAttackTolerance);
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
