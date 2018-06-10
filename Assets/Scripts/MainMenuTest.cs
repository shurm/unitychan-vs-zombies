using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTest : MonoBehaviour {

    public Texture[] imageArray;
    public int currentImage=0;
    Rect imageRect;
    Rect buttonRect ;
 
    void Start()
    {
        imageRect = new Rect(0, 0, Screen.width, Screen.height);
        buttonRect = new Rect(0, Screen.height - Screen.height / 10, Screen.width, Screen.height / 10);
    }

    void OnGUI()
    {
        GUI.Label(imageRect, imageArray[currentImage]);
        if (GUI.Button(buttonRect, "Next"))
        {
            currentImage = (currentImage + 1) % imageArray.Length;
        }
    }

}
