using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : SpecialCollisionHandler
{
    private PlayerMovementController playerMovementController;

    public int attackDamage = 1;

    // Use this for initialization
    void Start ()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    public override void HandleCollision(GameObject gameObject)
    {

        //if player is attacking
        if(playerMovementController.IsAttacking())
        {
            GeneralZombieBehavior script = gameObject.GetComponentInParent<GeneralZombieBehavior>();
            script.DamageZombie(attackDamage);
        }
    }

 
}
