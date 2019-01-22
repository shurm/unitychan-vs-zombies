using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForAnimation : MonoBehaviour
{
    public string[] attackingStateNames;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    internal bool CheckForAttackAnimation()
    {
        AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        foreach (string stateName in attackingStateNames)
        {
            if (currentAnimatorStateInfo.IsName(stateName))
            {
                Debug.Log("returned true");
                return true;
            }
        }
        Debug.Log("returned false");
        return false;
    }
}
