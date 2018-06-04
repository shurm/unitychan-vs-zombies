using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float runSpeed = 3;
    public float walkSpeed = 2;

    private float currentSpeed;
    private Animator anim;
    private bool run;

  

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Punch", true);
        }

        else if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Kick", true);
        }
        else
        {
            anim.SetBool("Punch", false);
            anim.SetBool("Kick", false);

            if (Input.GetKey(KeyCode.LeftShift))
                run = true;
            else
                run = false;

            float direction = Input.GetAxis("Horizontal");

            float speed = Input.GetAxis("Vertical");
            if (speed < 0)
                speed = 0;

            anim.SetFloat("Speed", speed);
            anim.SetFloat("Direction", direction);
            anim.SetBool("run", run);

            float moveX = direction * 100f * Time.deltaTime;

            //if player is running, player travels more distance (aka travels at higher velocity)
            if (run)
            {
                moveX *= 1.5f;
                currentSpeed = runSpeed;
            }
            else
                currentSpeed = walkSpeed;

            //rotates the player
            if (direction != 0)
                transform.Rotate(0, moveX, 0, Space.World);

            if (speed > 0)
                transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
    }

    public bool IsAttacking()
    {
        AnimatorStateInfo animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName("hk") || animatorStateInfo.IsName("hp"))
            return true;
      
        return false;
    }


    public void PlayDeadAnimation()
    {
        anim.Play("dead");
    }
    

   
}
