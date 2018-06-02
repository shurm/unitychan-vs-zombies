using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverChecker : MonoBehaviour
{
    public Camera mainCamera;
    public Camera gameOverCamera;
    public Text mainText;

    public GameObject player;

    private Health playersHealth;
    // Use this for initialization
    void Start ()
    {
        playersHealth = player.GetComponent<Health>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playersHealth.currentValue<=0)
        {
            //target.GetComponent<player>().playDeadAnimation();
            DoGameOver();
        }
    }
    
    void DoGameOver()
    {
        mainCamera.gameObject.SetActive(false);
        gameOverCamera.gameObject.SetActive(true);
        mainText.color = Color.red;
        mainText.text = "Game Over";
    }
}
