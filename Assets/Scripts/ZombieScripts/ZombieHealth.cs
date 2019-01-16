using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{ 
    
    public int currentAttackTolerance;

    private Transform target;
    private SceneDirector director;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
       
        ZombieEnvironmentVariables vars = GetComponent<ZombieEnvironmentVariables>();
        target = vars.target;
        director = vars.director;
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
        currentAttackTolerance -= damage;

        if (currentAttackTolerance <= 0)
        {
            anim.SetBool("fall_back", true);
            Invoke("RemoveZombie", 1.2f);
            return true;
        }
        else
        {
            anim.SetBool("stunned", true);
        }
        return false;
    }
}
