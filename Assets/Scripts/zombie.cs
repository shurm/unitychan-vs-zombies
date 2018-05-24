using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class zombie : MonoBehaviour
{
    private static readonly float time = 3;

    public Transform camera1;
    public Transform camera2;
    public Text message;

    public Image heathBar;
    public Text ratioText;

    public int damagePlayerTakes;

    public Transform target;

    public Transform director;

    private NavMeshAgent agent;
    private Animator anim;

    private bool dead;

    private float timeLeft = 0;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    void Update() {
      
        if(agent.isOnOffMeshLink)
        {
            Debug.Log("Off Mesh link!");
            agent.isStopped = true;
           
                Debug.Log("Reset Path");
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Debug.Log(data.endPos);
            //offMeshLink.costOverride = Mathf.Infinity;
                agent.SetDestination(target.position);
                agent.isStopped = false;
            
            
        }
        if (!dead)
        { 
            agent.SetDestination(target.position);
            transform.LookAt(agent.nextPosition);



            if (target.GetComponent<player>().isAttacking() && facingEachOther() && zombieIsCloseEnough())
            {
                anim.Play("back_fall");
                dead = true;
                director.GetComponent<director>().zombieDead();
                target.GetComponent<player>().increaseScore();
                Invoke("makeZombieDisappear", 1.2f);
            }
        }
        else
            agent.SetDestination(transform.position);
    }
    void makeZombieDisappear()
    {
        Destroy(transform.gameObject);


    }

    bool zombieIsCloseEnough()
    {
        float x = Mathf.Abs(transform.position.x - target.transform.position.x);
        float z= Mathf.Abs(transform.position.z - target.transform.position.z);
        if (x < 1 && z < 1)
            return true;
        return false;
    }

    bool facingEachOther()
    {
        if (Mathf.Abs(Vector3.Angle(transform.forward, target.transform.forward) - 180) < 40)
            return true;
        return false;

    }
    void OnCollisionEnter(Collision other)
    {
        if (!dead && other.collider.CompareTag("player"))
        {
            anim.Play("attack");
            makePlayerTakeDamage();
            timeLeft = time;
            // Debug.Log("colliding!!");
        }
    }
    
    void OnCollisionStay(Collision other)
    { 
        if (!dead && other.collider.CompareTag("player") )
        {
            if (timeLeft <= 0)
            { 
                anim.Play("attack");
                makePlayerTakeDamage();

                timeLeft = time;
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
            // Debug.Log("colliding!!");
        }
        
    }
    
    void makePlayerTakeDamage()
    {

        target.GetComponent<player>().makeSynchronized(transform);
    }

    public void takeDamageHelper()
    {
        target.GetComponent<player>().subtractFromHealthBarCurrentValue(damagePlayerTakes);

        UpdateHealthBar();
        if (target.GetComponent<player>().getHealthBarCurrentValue() <= 0)
        {
            target.GetComponent<player>().playDeadAnimation();
            Invoke("GameOverStuff", 1.2f);
        }
    }

    void GameOverStuff()
    {
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(true);
        message.color = Color.red;
        message.text = "Game Over";
    }
    void UpdateHealthBar()
    {
        float ratio = target.GetComponent<player>().getHealthBarCurrentValue() / target.GetComponent<player>().getHealthBarMaxValue();

        heathBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratioText.text = (ratio * 100).ToString("0") + "%";

        
    }

}
