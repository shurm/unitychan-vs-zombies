using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public GameObject player;
    [Range(0f,1f)]
    public float healthRatio = 1f;

   
	// Use this for initialization
	void Start ()
    {
      
    }
	
	// Update is called once per frame
	void Update () {
        UpdateHealthBar();

    }

    
    void UpdateHealthBar()
    {

        //float ratio = target.GetComponent<player>().getHealthBarCurrentValue() / target.GetComponent<player>().getHealthBarMaxValue();
        if(healthRatio>0.5f)
            healthBar.color = Color.Lerp(Color.yellow, Color.green, healthRatio-0.5f);
        else
            healthBar.color = Color.Lerp(Color.red, Color.yellow, healthRatio*2);
        healthBar.rectTransform.localScale = new Vector3(healthRatio, 1, 1);

        //ratioText.text = (ratio * 100).ToString("0") + "%";

    }
}
