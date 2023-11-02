using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    //public static ModeSelection instance;
    //public static ModeSelection Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new ModeSelection();
    //        }
    //        return instance;
    //    }
    //}
    //public static ModeSelection Instance;
    //private void Awake()
    //{
    //    if (Instance)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    //private static ModeSelection instance = null;
    //public static ModeSelection Instance
    //{
    //    get { return instance; }
    //}
    //void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(gameObject);
    //}
    public enum LoadLevel
    {
        TeethBraces, TeethGums, TeethReparing, TeethCleaning
    }
    private LoadLevel loadLevel;

    public GameObject[] chairOneCharacter;
    public GameObject[] chairTwoCharacter;
    public GameObject[] chairThreeCharacter;
    public GameObject[] characterAnim;
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
    public Sprite btnOnSprite;
    public Sprite btnOffSprite;
    private bool isVibration;
    public AudioSource charClickSFX;
    public GameObject SoundsObject;

    // Start is called before the first frame update
    void Start()
    {
        Usman_SaveLoad.LoadProgress();
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

        if (SaveData.Instance.isMusic == true)
        {
            musicOnOffBtn.sprite = btnOnSprite;
            SoundsObject.SetActive(true);
            BgMusic.Instance.SoundsObject.SetActive(true);
        }
        else if (SaveData.Instance.isMusic == false)
        {
            musicOnOffBtn.sprite = btnOffSprite;
            SoundsObject.SetActive(false);
            BgMusic.Instance.SoundsObject.SetActive(false);
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
        if (SaveData.Instance.isMusic == true)
        {
            musicOnOffBtn.sprite = btnOffSprite;
            SoundsObject.SetActive(false);
            BgMusic.Instance.SoundsObject.SetActive(false);
            SaveData.Instance.isMusic = false;
        }
        else if (SaveData.Instance.isMusic == false)
        {
            musicOnOffBtn.sprite = btnOnSprite;
            SoundsObject.SetActive(true);
            BgMusic.Instance.SoundsObject.SetActive(true);
            SaveData.Instance.isMusic = true;
        }
        Usman_SaveLoad.SaveProgress();
    }
    public void VibrationOnOff()
    {
        if (isVibration == false)
        {
            isVibration = true;
            vibrationOnOffBtn.sprite = btnOffSprite;

        }
        else if (isVibration == true)
        {
            isVibration = false;
            vibrationOnOffBtn.sprite = btnOnSprite;

#if !UNITY_EDITOR
            MoreMountains.NiceVibrations.NiceVibrationsDemoManager.Instance.TriggerHeavyImpact();
#endif

        }
    }
}
