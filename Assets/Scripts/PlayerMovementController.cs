using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float runSpeed = 3;
    public float walkSpeed = 2;

    public float delayForFightingAnimations = 0.2f;

    private float currentSpeed;
    private Animator anim;
    private bool run;

    private float delayForFightingAnimationsCopy;

    public float currentDirectionInput = 0;
    public float currentDirection = 0;
    public float timeDelta = 0;

    public float timeDeltaDirectionScale = 0.075f;
    private Vector2 t1 = Vector2.zero, t2 = Vector2.zero, l = Vector2.zero;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        delayForFightingAnimationsCopy = delayForFightingAnimations;

    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(delayForFightingAnimations);
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Punch", true);
            delayForFightingAnimations = delayForFightingAnimationsCopy;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Kick", true);
            delayForFightingAnimations = delayForFightingAnimationsCopy;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
                run = true;
            else
                run = false;

            currentDirectionInput = Input.GetAxis("Horizontal");

            float speed = Input.GetAxis("Vertical");
            if (speed < 0)
                speed = 0;

            anim.SetFloat("Speed", speed);

            if (currentDirectionInput != currentDirection)
            {
                timeDelta += (Time.deltaTime * timeDeltaDirectionScale);
                t1.y = currentDirection;
                t2.y = currentDirectionInput;
                l = Vector2.Lerp(t1, t2, timeDelta);
                currentDirection = l.y;

            }
            else
            {
                timeDelta = 0;
            }
            anim.SetFloat("Direction", currentDirection);
            anim.SetBool("run", run);

            float moveX = currentDirection * 100f * Time.deltaTime;

            //if player is running, player travels more distance (aka travels at higher velocity)
            if (run)
            {
                moveX *= 1.5f;
                currentSpeed = runSpeed;
            }
            else
                currentSpeed = walkSpeed;

            //rotates the player
            if (currentDirection != 0)
                transform.Rotate(0, moveX, 0, Space.World);

            if (speed > 0)
            {
                transform.position += transform.forward * currentSpeed * Time.deltaTime;
                StopAttack();
            }
            else
            {
                if (anim.GetBool("Punch") || anim.GetBool("Kick"))
                {
                    if (delayForFightingAnimations <= 0)
                    {
                        StopAttack();
                    }
                    else
                        delayForFightingAnimations -= Time.deltaTime;
                }
            }
        }
    }

    private void StopAttack()
    {
        anim.SetBool("Punch", false);
        anim.SetBool("Kick", false);
    }


    public void PlayDeadAnimation()
    {
        anim.Play("dead");
    }
    

   
}
