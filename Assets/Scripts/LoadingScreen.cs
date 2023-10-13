using System.Collections;
using UnityEngine.SceneManagement;
//using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public enum Scenes
{
    MainMenu
}

[System.Serializable]
public class LoadingProperties
{
    public Image fillBar;
    public Scenes nextScene;
    [Range(3, 10)]
    public float waitTime;
}

public class LoadingScreen : MonoBehaviour
{
    public LoadingProperties loadingProps;
    void Start()
    {
        //if (GAManager.Instance != null)
        //{
        //    GAManager.Instance.LogDesignEvent("Splash:Start");
        //}
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1;
        AudioListener.pause = false;
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {

        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(loadingProps.nextScene.ToString());
        //if (GAManager.Instance != null)
        //{
        //    GAManager.Instance.LogDesignEvent("Splash:MainMenu");
        //}
        asyncLoad.allowSceneActivation = false;

        while (loadingProps.fillBar.fillAmount < 1)
        {
            loadingProps.fillBar.fillAmount += Time.deltaTime / loadingProps.waitTime;
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
