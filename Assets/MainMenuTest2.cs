using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTest2 : MonoBehaviour {

    // Assign this with the prefab gameobject that has an Image component in the inspector.
    // It already stores the Image component instead of a GO.
    public Image imagePrefab;

    // Fill your list with image somewhere.
    List<Sprite> images = new List<Sprite>();

    // For testing try this
    // Remember to mark the image file as "Sprite UI" in the import settings in Unity
    public Sprite testSprite;

    void Start()
    {
        for (int a = 0; a < 3; a++)
        { 
            // Put test image in array or list
            images.Add(testSprite);

            Vector3 position = new Vector3(0, a * 50, 0);

            // Instantiate the imagePrefab
            Image imageInstance = GameObject.Instantiate(imagePrefab, position, Quaternion.identity, transform);

         

            // Assign the first image of the list or array or chose the one you need.
            //imageInstance.sprite = images[0];
        }
    }
}
