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
        GeneralZombieBehavior script = gameObject.GetComponentInParent<GeneralZombieBehavior>();
        ZombieCollisionHandler zombieAttackScript = gameObject.GetComponentInParent<ZombieCollisionHandler>();
        zombieAttackScript.CurrentlyBeingAttacked = true;


        NavMeshAgent navMeshAgentScript = gameObject.GetComponentInParent<NavMeshAgent>();
        navMeshAgentScript.isStopped = true;

        bool zombieDestroyed = script.DamageZombie(attackDamage);

        if (zombieDestroyed)
        {
            score.UpdateScore(zombiePoints);
        }
    }

    public override void HandleEndOfCollision(GameObject gameObject)
    {
        NavMeshAgent navMeshAgentScript = gameObject.GetComponentInParent<NavMeshAgent>();
        navMeshAgentScript.isStopped = false;

        ZombieCollisionHandler zombieAttackScript = gameObject.GetComponentInParent<ZombieCollisionHandler>();
        zombieAttackScript.CurrentlyBeingAttacked = false;
    }
}
