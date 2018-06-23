using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenePreviewLoader : MonoBehaviour
{

    [System.Serializable]
    public class ImageDescriptionPair
    {
        public string description;
        public Sprite previewImage;
    }


    public ImageDescriptionPair[] sceneAndDescriptions;
    public Image previewSign;

    public Text descriptionLabel;

    private int index = 0;

    // Use this for initialization
    void Start ()
    {
        SetPreviewAndDescription();
    }

    private void SetPreviewAndDescription()
    {
        descriptionLabel.text = sceneAndDescriptions[index].description;
        previewSign.sprite = sceneAndDescriptions[index].previewImage;
    }
	
    public void LeftArrowClick()
    {
        index = (index - 1) % sceneAndDescriptions.Length;
        SetPreviewAndDescription();
    }

    public void RightArrowClick()
    {
        index = (index + 1) % sceneAndDescriptions.Length;
        SetPreviewAndDescription();
    }

    public void LoadCurrentScene()
    {
        string sceneName = descriptionLabel.text.ToLower();

        for (int a = 0; a < SceneManager.sceneCount; a++)
        {
            Scene scene = SceneManager.GetSceneAt(a);
            string lowerCaseSceneName = scene.name.ToLower();
            if (scene.name.Equals(lowerCaseSceneName))
            {
                // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
                SceneManager.LoadScene(a, LoadSceneMode.Single);
                Debug.Log("loading scene");
               
            }
           
        }
        
        
    }
}
