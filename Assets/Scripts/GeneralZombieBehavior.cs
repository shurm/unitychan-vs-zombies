using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeneralZombieBehavior : MonoBehaviour
{
    public Transform target;

    public RadarData radar;

    public Director director;

    public int hitsItCanTake;

    public float turningSpeed;

    public float sensingDistance = 1.0f;

    private NavMeshAgent agent;

    private Animator anim;

    private bool dead;

   
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        anim = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (!dead)
        {
            //agent.SetDestination(target.position);
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

    public bool IsDead()
    {
        return dead;
    }

    private void RemoveZombie()
    {
        radar.RemoveRadarObject(gameObject);
        director.NextLevelCheck();
        Destroy(gameObject);
    }

    public bool DamageZombie(int damage)
    {
        hitsItCanTake -= damage;

        if(hitsItCanTake<=0)
        {
            anim.Play("back_fall");
            dead = true;
            Invoke("RemoveZombie", 1.2f);
            return true;
        }
        return false;
    }

}
