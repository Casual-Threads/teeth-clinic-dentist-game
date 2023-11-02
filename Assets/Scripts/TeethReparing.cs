using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum TeethReparingActionPerform
{
    none, Clipper, Brush, Excavator, FirstDrill, SecondDrill, FillSpoon, TeethLaser, PourWater, WateringOut, Germs
}

public class TeethReparing : MonoBehaviour
{
    public static TeethReparing Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    [Header("Action Types")]
    public TeethReparingActionPerform action;
    [Header("Dragable Items")]
    public GameObject clipper;
    public GameObject brush, excavator, firstDrill, secondDrill, spoonWithPot, teethLaser, waterPipe, waterOutPump;
    [Header("Items Layers")]
    public GameObject tray;
    public GameObject emptyTray, dirtyTeethLayer, whiteTeethLayer, teethShine, waterLayer, pumpWaterLayer, secondWaterOutPump, lightIndication, trayHandIndication;
    [Header("Panels")]
    public GameObject teethReparingPanel;
    public GameObject levelCompletePanel, settingPanel, rateUsPanel, loadingPanel, germsPanel, darkPanel, adPanel;
    [Header("Images")]
    public Image openMouth;
    public Image taskFillbar, loadingFillbar, lightHolder, lightWhiteLayer, musicOnOffBtn, vibrationOnOffBtn;
    [Header("Image Arrays")]
    public Image[] trayImages;
    public Image[] starImages;
    [Header("Arrays for Indications")]
    public Image[] durtLayer;
    public Image[] brownDamagedTeethLayer, blackDamagedTeethLayer, yellowDamagedTeethLayer;
    [Header("Sprites")]
    public Sprite goldStarSprite;
    public Sprite grayStarSprite, lightOnSprite, lightOffSprite, cleanTeethLayer, btnOnSprite, btnOffSprite;
    [Header("Particle System")]
    public ParticleSystem taskDoneParticle;
    public ParticleSystem balloonParticle;
    public GameObject finalParticle;
    [Header("Sounds")]
    public AudioSource itemPickSFX;
    public AudioSource itemDropSFX, burshSFX, excavatorSFX, drillSFX, teethLaserSFX, AudienceCheerSFX;
    public GameObject SoundsObject;
    private bool isLight, isRateus, isMusic, isVibration;
    AsyncOperation asyncLoad;
    private bool isCheck;
    public Transform downParent;
    private bool canShowInterstitial;
    private IEnumerator adDelayCouroutine;
    public Text waitAdLoadTime;
    void Start()
    {
        adDelayCouroutine = adDelay(30);
        StartCoroutine(adDelayCouroutine);
        Usman_SaveLoad.LoadProgress();
        action = TeethReparingActionPerform.Clipper;
        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            PlayerPrefs.SetInt("IsFirstTime", 0);
        }
        if (PlayerPrefs.GetInt("IsFirstTime") == 0)
        {
            StartCoroutine(ObjectEnableOrDisable(2.5f, lightIndication, true));
        }
        else
        {
            StartCoroutine(ObjectEnableOrDisable(1f, clipper, true));
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

    #region ShowInterstitialAD
    public void ShowInterstitial()
    {
        if (MyAdsManager.instance)
        {
            MyAdsManager.instance.ShowInterstitialAds();
        }
    }
    private void CheckInterstitialAD()
    {
        if (MyAdsManager.Instance != null)
        {

            if (MyAdsManager.Instance.IsInterstitialAvailable() && canShowInterstitial)
            {
                canShowInterstitial = !canShowInterstitial;
                adDelayCouroutine = adDelay(30);
                StartCoroutine(adDelayCouroutine);
                StartCoroutine(showInterstitialAD());
            }
        }
    }

    IEnumerator showInterstitialAD()
    {
        if (adPanel)
        {
            adPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            waitAdLoadTime.text = "..2";
            yield return new WaitForSeconds(1f);
            waitAdLoadTime.text = ".1";
            yield return new WaitForSeconds(1f);
            waitAdLoadTime.text = "0";
            yield return new WaitForSeconds(0.5f);
            adPanel.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            waitAdLoadTime.text = "...3";
        }
        ShowInterstitial();
    }
    IEnumerator adDelay(float _Delay)
    {
        yield return new WaitForSeconds(_Delay);
        canShowInterstitial = !canShowInterstitial;
    }
    #endregion

    #region LightOnOFF
    public void LightOnOff()
    {
        lightIndication.SetActive(false);
        if (isLight == false)
        {
            isLight = true;
            lightHolder.sprite = lightOnSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f);
        }
        else if (isLight == true)
        {
            isLight = false;
            lightHolder.sprite = lightOffSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(0f, 0f, 0f), 0.15f);
        }
        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            PlayerPrefs.SetInt("IsFirstTime", 0);
        }
        if (PlayerPrefs.GetInt("IsFirstTime") == 0)
        {
            StartCoroutine(ObjectEnableOrDisable(1f, trayHandIndication, true));
            StartCoroutine(ObjectEnableOrDisable(1f, clipper, true));
        }
    }
    #endregion

    #region SettingPanel
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

    public void RateUsPanelOnOff()
    {
        if (SaveData.Instance.isRateUs == true)
        {
            SaveData.Instance.isRateUs = false;
            rateUsPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(LevelComplete());
        }
        Usman_SaveLoad.SaveProgress();
    }
    #endregion

    #region Next Scene Load
    public void LoadNextScene(string str)
    {
        StartCoroutine(ObjectEnableOrDisable(0.1f, finalParticle, false));
        StartCoroutine(ObjectEnableOrDisable(0.1f, balloonParticle.gameObject, false));
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    #endregion

    #region TaskDone
    public void TaskDone()
    {   
        if (action == TeethReparingActionPerform.Clipper)
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
            PlayerPrefs.Save();
            itemPickSFX.Play();
            //clipper.SetActive(false);
        }
        else if(action == TeethReparingActionPerform.Brush)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.Excavator;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, true));
            AfterTaskDonePerform();
            for (int i = 0; i < durtLayer.Length; i++)
            {
                durtLayer[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    durtLayer[i].transform.parent = downParent;
                }
            }
        }
        else if(action == TeethReparingActionPerform.Excavator)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, firstDrill, true));
            AfterTaskDonePerform();
            for (int i = 0; i < brownDamagedTeethLayer.Length; i++)
            {
                brownDamagedTeethLayer[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    brownDamagedTeethLayer[i].transform.parent = downParent;
                }
            }
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethReparingActionPerform.FirstDrill)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, firstDrill, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, secondDrill, true));
            AfterTaskDonePerform();
            for (int i = 0; i < blackDamagedTeethLayer.Length; i++)
            {
                blackDamagedTeethLayer[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    blackDamagedTeethLayer[i].transform.parent = downParent;
                }
            }
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethReparingActionPerform.SecondDrill)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, secondDrill, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, spoonWithPot, true));
            AfterTaskDonePerform();
            for (int i = 0; i < yellowDamagedTeethLayer.Length; i++)
            {
                yellowDamagedTeethLayer[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    yellowDamagedTeethLayer[i].transform.parent = downParent;
                }
            }
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethReparingActionPerform.FillSpoon)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.TeethLaser;
            StartCoroutine(ObjectEnableOrDisable(0.5f, spoonWithPot, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, true));
            AfterTaskDonePerform();
        }
        else if(action == TeethReparingActionPerform.TeethLaser)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.PourWater;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, waterPipe, true));
            tray.SetActive(false);
            dirtyTeethLayer.SetActive(false);
            openMouth.sprite = cleanTeethLayer;
            AfterTaskDonePerform();

        }
        else if (action == TeethReparingActionPerform.PourWater)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.WateringOut;
            waterLayer.SetActive(false);
            pumpWaterLayer.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, waterPipe, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, waterOutPump, true));
            AfterTaskDonePerform();
        }
        else if (action == TeethReparingActionPerform.WateringOut)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.Germs;
            StartCoroutine(ObjectEnableOrDisable(0.3f, waterOutPump, false));
            StartCoroutine(ObjectEnableOrDisable(0.3f, secondWaterOutPump, false));
            StartCoroutine(ObjectEnableOrDisable(0.3f, teethReparingPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, true));

        }
        else if (action == TeethReparingActionPerform.Germs)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethReparingPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethShine, true));
            dirtyTeethLayer.SetActive(false);
            openMouth.sprite = cleanTeethLayer;
            print("All Task Done");
            Invoke("RateUsPanelOnOff", 2f);
        }
        CheckInterstitialAD();
    }

    IEnumerator AfterTaskColliderAndIndicationsOn()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 6; i++)
        {
            if (action == TeethReparingActionPerform.Excavator)
            {
                brownDamagedTeethLayer[i].transform.GetComponent<PolygonCollider2D>().enabled = true;
            }
            else if (action == TeethReparingActionPerform.FirstDrill)
            {
                blackDamagedTeethLayer[i].transform.GetComponent<PolygonCollider2D>().enabled = true;
            }
            else if (action == TeethReparingActionPerform.SecondDrill)
            {
                yellowDamagedTeethLayer[i].transform.GetComponent<PolygonCollider2D>().enabled = true;
            }
                
        }
        StartCoroutine(ChangeActionType());
    }
    IEnumerator ChangeActionType()
    {
        yield return new WaitForSeconds(1f);

        if (action == TeethReparingActionPerform.Excavator)
        {
            action = TeethReparingActionPerform.FirstDrill;
        }
        else if (action == TeethReparingActionPerform.FirstDrill)
        {
            action = TeethReparingActionPerform.SecondDrill;
        }
        else if (action == TeethReparingActionPerform.SecondDrill)
        {
            action = TeethReparingActionPerform.FillSpoon;
        }

    }
    #endregion

    #region Funcations

    #region First Task
    // First Task Tray Image On 1 by 1
    public void ObjectEnable(int index)
    {
        if (index == 0)
        {
            if (trayImages[0].gameObject.activeSelf)
            {
                trayImages[1].gameObject.SetActive(true);
            }
            trayImages[0].gameObject.SetActive(true);
                    
        }
        else if (index == 1)
        {
            if (trayImages[2].gameObject.activeSelf)
            {
                trayImages[3].gameObject.SetActive(true);
            }
            trayImages[2].gameObject.SetActive(true);
            
        }
        else if (index == 2)
        {
            if (trayImages[4].gameObject.activeSelf)
            {
                trayImages[5].gameObject.SetActive(true);
            }
            trayImages[4].gameObject.SetActive(true);
            
        }
        else if (index == 3)
        {
            if (trayImages[6].gameObject.activeSelf)
            {
                trayImages[7].gameObject.SetActive(true);
            }
            trayImages[6].gameObject.SetActive(true);

        }
        else if (index == 4)
        {
            if (trayImages[8].gameObject.activeSelf)
            {
                trayImages[9].gameObject.SetActive(true);
            }
            trayImages[8].gameObject.SetActive(true);
        }
        taskFillbar.fillAmount += 0.1f;
        TaskFillBar();
        itemDropSFX.Play();
        StartCoroutine(ObjectEnableOrDisable(0.5f, clipper, true));
        clipper.GetComponent<ScalePingPong>().enabled = true;
        if (trayImages[0].gameObject.activeSelf && trayImages[1].gameObject.activeSelf && trayImages[2].gameObject.activeSelf && trayImages[3].gameObject.activeSelf
            && trayImages[4].gameObject.activeSelf && trayImages[5].gameObject.activeSelf && trayImages[6].gameObject.activeSelf && trayImages[7].gameObject.activeSelf
            && trayImages[8].gameObject.activeSelf && trayImages[9].gameObject.activeSelf)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethReparingActionPerform.Brush;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, clipper, false));
            AfterTaskDonePerform();
        }
        StartCoroutine(TrayIEnumerator());
    }
    #endregion
    private void AfterTaskDonePerform()
    {
        StartCoroutine(ObjectEnableOrDisable(2f, taskDoneParticle.gameObject, false));
        taskFillbar.fillAmount = 0f;
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = grayStarSprite;
        }
    }
    public void TaskFillBar()
    {
        if (taskFillbar.fillAmount >= 0.2f)
        {
            starImages[0].sprite = goldStarSprite;
            if (taskFillbar.fillAmount >= 0.5f)
            {
                starImages[1].sprite = goldStarSprite;
                if (taskFillbar.fillAmount >= 0.83f)
                {
                    starImages[2].sprite = goldStarSprite;
                }
            }
        }
    }
    #endregion

    #region IEnumerators
    IEnumerator TrayIEnumerator()
    {
        yield return new WaitForSeconds(0.5f);
        emptyTray.transform.DOLocalMove(new Vector3(-991f, -344f, 0), 1f);
    }
    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(ObjectEnableOrDisable(0.5f, levelCompletePanel, true));
        AudienceCheerSFX.Play();
        StartCoroutine(ObjectEnableOrDisable(0.7f, finalParticle, true));
        StartCoroutine(ObjectEnableOrDisable(0.7f, balloonParticle.gameObject, true));
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
    #endregion

    #region EnableOrDisable
    IEnumerator ObjectEnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.SetActive(isTrue);
    }
    IEnumerator EnableAnim(float _Delay, Animator activateObject)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.enabled = true;
    }
    #endregion
}
