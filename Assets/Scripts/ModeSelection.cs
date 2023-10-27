using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    public GameObject[] chairOneCharacter;
    public GameObject[] chairTwoCharacter;
    public GameObject[] chairThreeCharacter;
    public GameObject[] characterAnim;
    public enum LoadLevel
    {
        TeethBraces, TeethGums, TeethReparing, TeethCleaning
    }
    private LoadLevel loadLevel;
    AsyncOperation asyncLoad;
    private int characterOneIndex;
    private int characterTwoIndex;
    private int characterThreeIndex;
    [Header("Panels")]
    public GameObject loadingPanel;
    public GameObject settingPanel;
    [Header("Images")]
    public Image loadingFillbar;
    public Image musicOnOffBtn, vibrationOnOffBtn;
    [Header("Sprites")]
    public Sprite onSprite;
    public Sprite offSprite;
    private bool isMusic, isVibration;
    public AudioSource charClickSFX;
    // Start is called before the first frame update
    void Start()
    {
        characterOneIndex = Random.Range(0, chairOneCharacter.Length);
        chairOneCharacter[characterOneIndex].SetActive(true);

        characterTwoIndex = Random.Range(0, chairTwoCharacter.Length);

        if (characterTwoIndex != characterOneIndex)
        {
            chairTwoCharacter[characterTwoIndex].SetActive(true);
        }
        


        characterThreeIndex = Random.Range(0, chairThreeCharacter.Length);
        if (characterOneIndex != characterThreeIndex)
        {
            if (characterTwoIndex != characterThreeIndex)
            {
                chairThreeCharacter[characterThreeIndex].SetActive(true);
            }
                
        }

    }
    public void Play(int index)
    {
        charClickSFX.Play();
        chairOneCharacter[index].SetActive(false);
        chairTwoCharacter[index].SetActive(false);
        chairThreeCharacter[index].SetActive(false);
        characterAnim[index].SetActive(true);
        StartCoroutine(LoadinPanelOn(index));
    }
    IEnumerator LoadinPanelOn(int index)
    {
        yield return new WaitForSeconds(3f);
        characterAnim[index].SetActive(false);
        loadingPanel.SetActive(true);
        loadLevel = (LoadLevel)index;
        StartCoroutine(LoadingScene(loadLevel.ToString()));
    }
    IEnumerator LoadingScene(string str)
    {
        asyncLoad = SceneManager.LoadSceneAsync(str);
        asyncLoad.allowSceneActivation = false;
        while (loadingFillbar.fillAmount < 1)
        {
            loadingFillbar.fillAmount += Time.deltaTime / 3;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        asyncLoad.allowSceneActivation = true;
    }

    public void GoToBack(string str)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    public void SettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void MusicOnOff()
    {
        if (isMusic == false)
        {
            isMusic = true;
            musicOnOffBtn.sprite = offSprite;
        }
        else if (isMusic == true)
        {
            isMusic = false;
            musicOnOffBtn.sprite = onSprite;
        }
        settingPanel.SetActive(true);
    }
    public void VibrationOnOff()
    {
        if (isVibration == false)
        {
            isVibration = true;
            vibrationOnOffBtn.sprite = offSprite;
        }
        else if (isVibration == true)
        {
            isVibration = false;
            vibrationOnOffBtn.sprite = onSprite;
        }
        settingPanel.SetActive(true);
    }
}
