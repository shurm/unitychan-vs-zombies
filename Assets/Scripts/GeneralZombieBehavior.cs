using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeneralZombieBehavior : MonoBehaviour
{
    public Transform target;

    public Radar radar;

    public int hitsItCanTake = 1;

    private static readonly float time = 3;

    private NavMeshAgent agent;
    private Animator anim;

    private bool dead;

    private float timeLeft = 0;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        dead = false;
        /*
        if (target == null)
            target = GameObject.Find("unitychan").transform;
        if(radar == null)
            radar = GameObject.Find("Radar").GetComponent<Radar>();
       */
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (!dead)
        {
            agent.SetDestination(target.position);
            if (agent.remainingDistance < 1.0f)
                transform.LookAt(target.position);
            else
                transform.LookAt(agent.nextPosition);
        }
        else
            agent.isStopped = true;
          */  
    }

    public bool IsDead()
    {
        return dead;
    }

    private void RemoveZombie()
    {
        radar.RemoveRaderObject(gameObject);
        Destroy(gameObject);
    }

    public void DamageZombie(int damage)
    {
        hitsItCanTake -= damage;

        if(hitsItCanTake<=0)
        {
            anim.Play("back_fall");
            dead = true;
            //director.GetComponent<director>().zombieDead();
            //target.GetComponent<player>().increaseScore();
            Invoke("RemoveZombie", 1.2f);
        }
    }

}
