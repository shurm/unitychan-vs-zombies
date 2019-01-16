using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Animator))]
public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private ZombieHealth currentZombieHealth;

    private Transform target;
    private SceneDirector director;

    public float turningSpeed;
    public float sensingDistance;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentZombieHealth = GetComponent<ZombieHealth>();

        ZombieEnvironmentVariables vars = GetComponent<ZombieEnvironmentVariables>();
        target = vars.target;
        director = vars.director;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!currentZombieHealth.IsDead())
        {
            agent.SetDestination(target.position);

          

            if (agent.remainingDistance <= sensingDistance)
            {

                Vector3 distance = target.position - transform.position;

                // The step size is equal to speed times frame time.
                float step = turningSpeed * Time.deltaTime;

                Vector3 dummyPosition = transform.position + (transform.forward * distance.magnitude);
                Vector3 targetPosition = Vector3.Lerp(dummyPosition, target.position, step);
                targetPosition.y = this.transform.position.y;


                // Move our position a step closer to the target.
                transform.LookAt(targetPosition);

            }
            else
                transform.LookAt(agent.nextPosition);
        }
        else
            agent.isStopped = true;
    }
}
