using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackManager : SpecialCollisionHandler
{
    public int zombiePoints = 10;

    public int attackDamage = 1;

    public DisplayScore score;
    

    public override void HandleStartOfCollision(GameObject gameObject)
    {
        ZombieHealth script = gameObject.GetComponentInParent<ZombieHealth>();
        ZombieAttackController zombieAttackScript = gameObject.GetComponentInParent<ZombieAttackController>();
        zombieAttackScript.CurrentlyBeingAttacked = true;


        NavMeshAgent navMeshAgentScript = gameObject.GetComponentInParent<NavMeshAgent>();
        navMeshAgentScript.isStopped = true;

        bool zombieDestroyed = script.TakeDamage(attackDamage);

        if (zombieDestroyed)
        {
            score.UpdateScore(zombiePoints);
        }
    }

    public override void HandleEndOfCollision(GameObject gameObject)
    {
        NavMeshAgent navMeshAgentScript = gameObject.GetComponentInParent<NavMeshAgent>();
        navMeshAgentScript.isStopped = false;

        ZombieAttackController zombieAttackScript = gameObject.GetComponentInParent<ZombieAttackController>();
        zombieAttackScript.CurrentlyBeingAttacked = false;
    }
}
