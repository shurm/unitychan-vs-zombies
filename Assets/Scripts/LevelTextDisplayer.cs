using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextDisplayer : MonoBehaviour
{
    public Text levelText;
    public int currentLevel = 1;

    public float textTime = 1f;

    // Use this for initialization
    void Start () {
        FadeAnimation();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
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
}
