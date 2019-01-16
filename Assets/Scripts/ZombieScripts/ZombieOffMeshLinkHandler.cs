using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Animator))]
public class ZombieOffMeshLinkHandler : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private float height = 3.0f;
    private float duration = 1f;
    //float normalizedTime = 0.0f;


    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                
                yield return StartCoroutine(JumpingMovementAndAnimation());
               
                agent.CompleteOffMeshLink();
                
            }
            yield return null;
        }
    }

    IEnumerator JumpingMovementAndAnimation()
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        animator.SetBool("Jumping", true);
        float ratioBeginning = (75.0f / 95.0f);
       
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < ratioBeginning)
        {
           // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }

        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        animator.SetBool("Jumping", false);

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("jumping") || animator.IsInTransition(0))
        {
            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }
    }

}
