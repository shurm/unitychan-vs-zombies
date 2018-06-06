using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    //public Dictionary<string,string> attackStateNames = [];
    public int attackDamage = 1;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void HandleCollision(GameObject gameObject)
    {

        //if player is attacking
        if(IsAttacking())
        {
            GeneralZombieBehavior script = gameObject.GetComponentInParent<GeneralZombieBehavior>();
            script.DamageZombie(attackDamage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }


    public bool IsAttacking()
    {
        AnimatorClipInfo animatorStateInfo = anim.GetCurrentAnimatorClipInfo(0)[0];
        string AnimatorClipName = animatorStateInfo.clip.name;
        
        if (AnimatorClipName.StartsWith("Punch") || AnimatorClipName.StartsWith("Kick"))
            return true;
        return false;
    }


}
