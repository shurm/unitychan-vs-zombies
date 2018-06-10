using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{

    private int score;

    public Text scoreText;

    public void UpdateScore(int addition)
    {
        score += addition;
        scoreText.text = "Score: " + score;
    }
}
