using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MainMenuElements
{
    [Header("GameObject")]
    public GameObject LoadingPanel;
    public Image fillbar;
}

public class MainMenu : MonoBehaviour
{
    public MainMenuElements uIElements;
    AsyncOperation asyncLoad;

    public void Play(string str)
    {
        //if (GAManager.Instance != null)
        //{
        //    GAManager.Instance.LogDesignEvent("MainMenu:PlayClick");
        //}
        uIElements.LoadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }

    IEnumerator LoadingScene(string str)
    {
        asyncLoad = SceneManager.LoadSceneAsync(str);
        asyncLoad.allowSceneActivation = false;
        while (uIElements.fillbar.fillAmount < 1)
        {
            uIElements.fillbar.fillAmount += Time.deltaTime / 3;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        asyncLoad.allowSceneActivation = true;
    }


}