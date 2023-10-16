using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum TeethCleaningActionPerform
{
    none, Clipper, Brush, Excavator, Grinder, Drill, ChewPuller, TeethLaser
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
    public GameObject levelCompletePanel, settingPanel, RateUsPanel, loadingPanel;
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
    public Sprite grayStarSprite, onLightSprite, offLightSprite, cleanTeethLayer, onSprite, OffSprite;
    [Header("Particle System")]
    public ParticleSystem taskDoneParticle;
    public GameObject finalParticle;
    [Header("Sounds")]
    public AudioSource itemPickSFX;
    public AudioSource itemDropSFX, burshSFX, excavatorSFX, grinderSFX, drillSFX, chewPullerSFX, teethLaserSFX;
    private bool isLight, isRateus, isMusic, isVibration;
    AsyncOperation asyncLoad;
    public Transform downParent;
    void Start()
    {
        action = TeethCleaningActionPerform.Clipper;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
            musicOnOffBtn.sprite = OffSprite;
        }
        else if(isMusic == true)
        {
            isMusic = false;
            musicOnOffBtn.sprite = onSprite;
        }
        settingPanel.SetActive(true);
    }
    public void VibrationOnOff()
    {
        if(isVibration == false)
        {
            isVibration = true;
            vibrationOnOffBtn.sprite = OffSprite;
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
            StartCoroutine(EnableOrDisable(0.5f, brush, false));
            StartCoroutine(EnableOrDisable(0.5f, excavator, true));
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
            StartCoroutine(EnableOrDisable(0.5f, excavator, false));
            StartCoroutine(EnableOrDisable(0.5f, grinder, true));
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
            StartCoroutine(EnableOrDisable(0.5f, grinder, false));
            StartCoroutine(EnableOrDisable(0.5f, drill, true));
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
            StartCoroutine(EnableOrDisable(0.5f, drill, false));
            StartCoroutine(EnableOrDisable(0.5f, chewPuller, true));
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
            StartCoroutine(EnableOrDisable(0.5f, chewPuller, false));
            StartCoroutine(EnableOrDisable(0.5f, teethLaser, true));
            AfterTaskDonePerform();

        }
        else if(action == TeethCleaningActionPerform.TeethLaser)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(EnableOrDisable(0.5f, teethLaser, false));
            dirtyTeethLayer.SetActive(false);
            openMouth.sprite = cleanTeethLayer;
            StartCoroutine(EnableOrDisable(0.5f, teethShine, true));
            print("All Task Done");
            StartCoroutine(LevelComplete());
        }

    }

    private void AfterTaskDonePerform()
    {
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
        StartCoroutine(EnableOrDisable(0.5f, clipper, true));
        if (trayImages[0].gameObject.activeSelf && trayImages[1].gameObject.activeSelf && trayImages[2].gameObject.activeSelf && trayImages[3].gameObject.activeSelf
            && trayImages[4].gameObject.activeSelf && trayImages[5].gameObject.activeSelf && trayImages[6].gameObject.activeSelf && trayImages[7].gameObject.activeSelf
            && trayImages[8].gameObject.activeSelf && trayImages[9].gameObject.activeSelf)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethCleaningActionPerform.Brush;
            StartCoroutine(EnableOrDisable(0.5f, brush, true));
            StartCoroutine(EnableOrDisable(0.5f, clipper, false));
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
    IEnumerator EnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
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
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnableOrDisable(0.5f, levelCompletePanel, true));
        StartCoroutine(EnableOrDisable(0.7f, finalParticle, true));
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
