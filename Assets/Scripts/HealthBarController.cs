using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public Text healthBarText;
    public Health playersHealth;

	// Use this for initialization
	void Start ()
    {
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateHealthBar();
    }

    
    void UpdateHealthBar()
    {
        float healthRatio = playersHealth.currentValue / 100f;
        if(healthRatio>0.5f)
            healthBar.color = Color.Lerp(Color.yellow, Color.green, healthRatio-0.5f);
        else
            healthBar.color = Color.Lerp(Color.red, Color.yellow, healthRatio*2);
        healthBar.rectTransform.localScale = new Vector3(healthRatio, 1, 1);

        healthBarText.text = playersHealth.currentValue.ToString("0") + "%";

    }
}
