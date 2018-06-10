using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextDisplayer : MonoBehaviour
{
    public Radar radar;
    public Text levelText;
    public int currentLevel = 1;

    public float textTime = 1f;

   
    // Use this for initialization
    void Start ()
    {    
        FadeAnimation();
    }
	
    public void StartNextLevelAnimation()
    {
        currentLevel++;
        setLevelText();
        FadeAnimation();
    }

    private void setLevelText()
    {
        levelText.text = "Level " + currentLevel;
    }
   
    private void FadeAnimation()
    {
        if (levelText != null)
        {
            Invoke("helperFull", 0f);
            Invoke("helperZero", 4f);
        }
    }
    private void helperFull()
    {

        StartCoroutine(FadeTextToFullAlpha());
    }
    private void helperZero()
    {
        StartCoroutine(FadeTextToZeroAlpha());
        
    }

    private IEnumerator FadeTextToFullAlpha()
    {
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 0);
        while (levelText.color.a < 1.0f)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, levelText.color.a + (Time.deltaTime / textTime));
            yield return null;
        }
    }

    private IEnumerator FadeTextToZeroAlpha()
    {
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 1);
        while (levelText.color.a > 0.0f)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, levelText.color.a - (Time.deltaTime / textTime));
            yield return null;
        }
    }
}
