using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenePreviewLoader : MonoBehaviour
{
    public GameObject loadingUI;
    public Transform loadingProgbar;
    public Text loadingText;
    private readonly float LOAD_READY_PERCENTAGE = 0.9f;
    AsyncOperation sceneAO;

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
        string desiredSceneName = descriptionLabel.text.ToLower();

        Debug.Log("SceneManager.sceneCount is " + SceneManager.sceneCountInBuildSettings);
        for (int a = 0; a < SceneManager.sceneCountInBuildSettings; a++)
        {
            //Scene scene = SceneManager.GetSceneByBuildIndex(a);
            string path = SceneUtility.GetScenePathByBuildIndex(a);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

            string lowerCaseSceneName = sceneName.ToLower();
            Debug.Log("lowerCaseSceneName is  " + lowerCaseSceneName);
            if (desiredSceneName.Equals(lowerCaseSceneName))
            {
                // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
                ChangeScene(a);
                Debug.Log("starting " + sceneName);

            }
        }
       
    }

    private void ChangeScene(int sceneIndex)
    {
        loadingUI.SetActive(true);
        
        StartCoroutine(LoadingSceneRealProgress(sceneIndex));
    }

    IEnumerator LoadingSceneRealProgress(int sceneIndex)
    {
        yield return new WaitForSeconds(1);

        sceneAO = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        // disable scene activation while loading to prevent auto load
        sceneAO.allowSceneActivation = false;

        while (!sceneAO.isDone)
        {
            loadingProgbar.localScale = new Vector3(sceneAO.progress, 1, 1) ;
            loadingText.text = sceneAO.progress.ToString("0") + "%";

            if (sceneAO.progress >= LOAD_READY_PERCENTAGE)
            {
                loadingProgbar.localScale = Vector3.one;
                loadingText.text = "100%";
               
                 sceneAO.allowSceneActivation = true;
                
            }
            Debug.Log(sceneAO.progress);
            yield return null;
        }
    }
}

