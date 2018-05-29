using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float runSpeed = 3;
    public float walkSpeed = 5;

    private float currentSpeed;

    public int scoreIncrease;

    public Text levelText;

    public Text scoreText;

    private int currentScore = 0;
    private float textTime = 1f;
    private int currentLevel = 1;
    private float healthBarCurrentValue = 92;

    private float healthBarMaxValue = 100;

    private Animator anim;

    private Rigidbody rb;
    private bool run;

    private bool currentlyTakingDamage;

    private Vector3 forward;

    private readonly object syncLock = new object();

    public float getHealthBarCurrentValue()
    {
        return healthBarCurrentValue;
    }
    public float getHealthBarMaxValue()
    {
        return healthBarMaxValue;
    }
    public void makeSynchronized(Transform zombie)
    {
        lock (syncLock)
        {
            currentlyTakingDamage = true;
            zombie.GetComponent<zombie>().takeDamageHelper();
            currentlyTakingDamage = false;
        }
    }
    public void subtractFromHealthBarCurrentValue(int damagePlayerTakes)
    {
        healthBarCurrentValue -= damagePlayerTakes;
        if (healthBarCurrentValue < 0)
            healthBarCurrentValue = 0;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentlyTakingDamage = false;
        forward = transform.forward;
        FadeAnimation();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            anim.SetBool("hp", true);
        else
            anim.SetBool("hp", false);
        if (Input.GetMouseButtonDown(1))
            anim.SetBool("hk", true);
        else
            anim.SetBool("hk", false);
        if (isAttacking())
            anim.speed = 0.7f;
        else
            anim.speed = 1f;


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

        if (run)
        {
            moveX *= 1.5f;
            currentSpeed = runSpeed;
        }
        else
            currentSpeed = walkSpeed;
        //rotates the player
        if (direction != 0)
        {
            transform.Rotate(0, moveX, 0, Space.World);
            forward = transform.forward;
        }
        /*
        Vector3 speedInAllDirections = transform.InverseTransformDirection(Vector3.forward);
        speedInAllDirections.x *= -1;
        //speedInAllDirections *= transform.forward;

        rb.velocity = Vector3.zero;
        rb.MovePosition(rb.position + speedInAllDirections * moveZ * Time.deltaTime);
        */
        if (speed > 0)
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public bool isAttacking()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("hk") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("hp"))
        {
            return true;
        }
        return false;
    }

   

    void FadeAnimation()
    {
        if (levelText != null)
        { 
        Invoke("helperFull", 0f);
        Invoke("helperZero", 4f);
    }
    }
    void helperFull()
    {

        StartCoroutine(FadeTextToFullAlpha());
    }
    void helperZero()
    {
        StartCoroutine(FadeTextToZeroAlpha());
    }

    IEnumerator FadeTextToFullAlpha()
    {
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 0);
        while (levelText.color.a < 1.0f)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, levelText.color.a + (Time.deltaTime / textTime));
            yield return null;
        }
    }

    IEnumerator FadeTextToZeroAlpha()
    {
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 1);
        while (levelText.color.a > 0.0f)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, levelText.color.a - (Time.deltaTime / textTime));
            yield return null;
        }
    }

    public void playDeadAnimation()
    {
        anim.Play("dead");
    }
    public void increaseScore()
    {
        currentScore += scoreIncrease;

        scoreText.text = "Score: " + currentScore;
    }

    public void SetForward(Vector3 newForward)
    {
        forward = newForward;
    }

    public void nextLevelAnimation()
    {
        currentLevel++;
        setLevelText();
        FadeAnimation();
    }

    void setLevelText()
    {
        levelText.text = "Level " + currentLevel;
    }

    public bool isTakingDamage()
    {
        return currentlyTakingDamage;
    }

}
