using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum TeethCleaningActionPerform
{
    none, Clipper, Brush, Excavator, Grinder, Drill, ChewPuller, TeethLaser, Germs
}

public class TeethCleaning : MonoBehaviour
{
    public static TeethCleaning Instance;
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
    public TeethCleaningActionPerform action;
    [Header("Dragable Items")]
    public GameObject clipper;
    public GameObject brush, excavator, grinder, drill, chewPuller, teethLaser;
    [Header("Items Layers")]
    public GameObject tray;
    public GameObject dirtyTeethLayer, teethShine/*, indicationsObject*/;
    [Header("Panels")]
    public GameObject TeethCleaningPanel;
    public GameObject levelCompletePanel, settingPanel, rateUsPanel, loadingPanel, germsPanel, darkPanel, adPanel;
    [Header("Images")]
    public Image openMouth;
    public Image taskFillbar, loadingFillbar, lightImage, lightWhiteLayer, musicOnOffBtn, vibrationOnOffBtn;
    [Header("Image Arrays")]
    public Image[] trayImages;
    public Image[] starImages;
    [Header("Indications Arrays")]
    public Image[] yellowTeethIndications;
    public Image[] yellowGreenDurtIndications, blackTeethIndications, chewIndications, crackIndications;
    [Header("Sprites")]
    public Sprite goldStarSprite;
    public Sprite grayStarSprite, onLightSprite, offLightSprite, cleanTeethLayer, onSprite, offSprite;
    [Header("Particle System")]
    public ParticleSystem taskDoneParticle;
    public ParticleSystem balloonParticle;
    public GameObject finalParticle;
    [Header("Sounds")]
    public AudioSource itemPickSFX;
    public AudioSource itemDropSFX, burshSFX, excavatorSFX, grinderSFX, drillSFX, chewPullerSFX, teethLaserSFX, AudienceCheerSFX;
    private bool isLight, isRateus, isMusic, isVibration;
    AsyncOperation asyncLoad;
    public Transform downParent;
    private bool canShowInterstitial;
    private IEnumerator adDelayCouroutine;
    public Text waitAdLoadTime;
    void Start()
    {
        adDelayCouroutine = adDelay(30);
        StartCoroutine(adDelayCouroutine);
        Usman_SaveLoad.LoadProgress();
        action = TeethCleaningActionPerform.Clipper;
    }
    public void ShowInterstitial()
    {
        if (MyAdsManager.instance)
        {
            MyAdsManager.instance.ShowInterstitialAds();
        }
    }
    #region ShowInterstitialAD
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
    public void LightOnOff()
    {
        if(isLight == false)
        {
            isLight = true;
            lightImage.sprite = onLightSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f);
        }
        else if(isLight == true)
        {
            isLight = false;
            lightImage.sprite = offLightSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(0f, 0f, 0f), 0.15f);
        }
    }

    public void SettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void MusicOnOff()
    {
        if(isMusic == false)
        {
            isMusic = true;
            musicOnOffBtn.sprite = offSprite;
        }
        else if(isMusic == true)
        {
            isMusic = false;
            musicOnOffBtn.sprite = onSprite;
        }
        settingPanel.SetActive(true);
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
    public void VibrationOnOff()
    {
        if(isVibration == false)
        {
            isVibration = true;
            vibrationOnOffBtn.sprite = offSprite;
        }
        else if(isVibration == true)
        {
            isVibration = false;
            vibrationOnOffBtn.sprite = onSprite;
        }
        settingPanel.SetActive(true);
    }

    public void LoadNextScene(string str)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }


    public void TaskDone()
    {   
        if (action == TeethCleaningActionPerform.Clipper)
        {
            itemPickSFX.Play();
            clipper.SetActive(false);
        }
        else if(action == TeethCleaningActionPerform.Brush)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.Excavator;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, true));
            AfterTaskDonePerform();
            for (int i = 0; i < yellowGreenDurtIndications.Length; i++)
            {
                yellowGreenDurtIndications[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    yellowGreenDurtIndications[i].transform.parent = downParent;
                }
            }
        }
        else if(action == TeethCleaningActionPerform.Excavator)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.Grinder;
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, grinder, true));
            AfterTaskDonePerform();
            for (int i = 0; i < blackTeethIndications.Length; i++)
            {
                blackTeethIndications[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    blackTeethIndications[i].transform.parent = downParent;
                }
            }
        }
        else if(action == TeethCleaningActionPerform.Grinder)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.Drill;
            StartCoroutine(ObjectEnableOrDisable(0.5f, grinder, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, drill, true));
            AfterTaskDonePerform();
            for (int i = 0; i < crackIndications.Length; i++)
            {
                crackIndications[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    crackIndications[i].transform.parent = downParent;
                }
            }
        }
        else if(action == TeethCleaningActionPerform.Drill)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.ChewPuller;
            StartCoroutine(ObjectEnableOrDisable(0.5f, drill, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, chewPuller, true));
            AfterTaskDonePerform();

            for (int i = 0; i < chewIndications.Length; i++)
            {
                chewIndications[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    chewIndications[i].transform.parent = downParent;
                }
            }
        }
        else if(action == TeethCleaningActionPerform.ChewPuller)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.TeethLaser;
            StartCoroutine(ObjectEnableOrDisable(0.5f, chewPuller, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, true));
            AfterTaskDonePerform();

        }
        else if(action == TeethCleaningActionPerform.TeethLaser)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.Germs;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, TeethCleaningPanel, false));
            AfterTaskDonePerform();

        }
        else if (action == TeethCleaningActionPerform.Germs)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, TeethCleaningPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethShine, true));
            dirtyTeethLayer.SetActive(false);
            openMouth.sprite = cleanTeethLayer;
            print("All Task Done");
            Invoke("RateUsPanelOnOff", 2f);
        }

    }

    private void AfterTaskDonePerform()
    {
        StartCoroutine(ObjectEnableOrDisable(2f, taskDoneParticle.gameObject, false));
        taskFillbar.fillAmount = 0f;
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = grayStarSprite;
        }
    }

    public void ObjectEnable(int index)
    {

        if (index == 0)
        {
            if (trayImages[8].gameObject.activeSelf)
            {
                trayImages[9].gameObject.SetActive(true);
            }
            trayImages[8].gameObject.SetActive(true);
                    
        }
        else if (index == 1)
        {
            if (trayImages[2].gameObject.activeSelf)
            {
                trayImages[3].gameObject.SetActive(true);
            }
            else if (trayImages[1].gameObject.activeSelf)
            {
                trayImages[2].gameObject.SetActive(true);
            }
            else if (trayImages[0].gameObject.activeSelf)
            {
                trayImages[1].gameObject.SetActive(true);
            }
            trayImages[0].gameObject.SetActive(true);
            
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
            trayImages[6].gameObject.SetActive(true);
            
        }
        else if (index == 4)
        {
            trayImages[7].gameObject.SetActive(true);
            
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
            action = TeethCleaningActionPerform.Brush;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, clipper, false));
            AfterTaskDonePerform();
            for (int i = 0; i < yellowTeethIndications.Length; i++)
            {
                yellowTeethIndications[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    yellowTeethIndications[i].transform.parent = downParent;
                }
            }
        }
        StartCoroutine(TrayIEnumerator());
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
    IEnumerator TrayIEnumerator()
    {
        yield return new WaitForSeconds(0.5f);
        tray.transform.DOLocalMove(new Vector3(-991f, -344f, 0), 1f);
    }
    IEnumerator LevelComplete()
    {
        //if (SaveData.Instance.isRateUs == true)
        //{
        //    SaveData.Instance.isRateUs = false;
        //    rateUsPanel.SetActive(true);
        //}
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
}
