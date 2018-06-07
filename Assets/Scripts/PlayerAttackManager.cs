using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackManager : SpecialCollisionHandler
{
    public int attackDamage = 1;
    
    private Animator anim;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    public override void HandleCollision(GameObject gameObject)
    {
        GeneralZombieBehavior script = gameObject.GetComponentInParent<GeneralZombieBehavior>();
        script.DamageZombie(attackDamage); 
    }
}
