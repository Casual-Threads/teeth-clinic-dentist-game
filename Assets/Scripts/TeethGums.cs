using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum TeethGumsActionPerform
{
    none, Clipper, Brush, Excavator, GumsScope, TeethLaser, GumCleaning, Spray, LaserLight, Germs
}

public class TeethGums : MonoBehaviour
{
    public static TeethGums Instance;
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
    public TeethGumsActionPerform action;
    [Header("Dragable Items")]
    public GameObject clipper;
    public GameObject brush, excavator, gumsScope, teethLaser, cottonPot, sprayBottel, mouthSprayAnim, laserLight;
    [Header("Items Layers")]
    public GameObject tray;
    public GameObject emptyTray, cottonTray, dirtyTeethLayer, gumsEffectedTeeth, teethShine, dirtyDrop, injuredPart, gum;
    [Header("Panels")]
    public GameObject teethGumsPanel;
    public GameObject levelCompletePanel, settingPanel, RateUsPanel, loadingPanel, germsPanel, darkPanel;
    [Header("Images")]
    public Image openMouth;
    public Image taskFillbar, loadingFillbar, lightHolder, lightWhiteLayer, musicOnOffBtn, vibrationOnOffBtn;
    [Header("Image Arrays")]
    public Image[] trayImages;
    public Image[] cottonTrayImages;
    public Image[] starImages;
    [Header("Arrays for Indications")]
    public Image[] durtLayer;
    [Header("Sprites")]
    public Sprite goldStarSprite;
    public Sprite grayStarSprite, lightOnSprite, lightOffSprite, cleanTeethLayer, btnOnSprite, btnOffSprite;
    [Header("Particle System")]
    public ParticleSystem taskDoneParticle;
    public GameObject finalParticle;
    [Header("Sounds")]
    public AudioSource itemPickSFX;
    public AudioSource itemDropSFX, burshSFX, excavatorSFX, teethLaserSFX;
    private bool isLight, isRateus, isMusic, isVibration;
    private bool isToffee, isLemon, isFishBone, isStrawberry, isBubble;
    AsyncOperation asyncLoad;
    public Transform downParent;
    void Start()
    {
        action = TeethGumsActionPerform.Clipper;
    }

    #region LightOnOFF
    public void LightOnOff()
    {
        if(isLight == false)
        {
            isLight = true;
            lightHolder.sprite = lightOnSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f);
        }
        else if(isLight == true)
        {
            isLight = false;
            lightHolder.sprite = lightOffSprite;
            lightWhiteLayer.transform.DOScale(new Vector3(0f, 0f, 0f), 0.15f);
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
        if(isMusic == false)
        {
            isMusic = true;
            musicOnOffBtn.sprite = btnOffSprite;
        }
        else if(isMusic == true)
        {
            isMusic = false;
            musicOnOffBtn.sprite = btnOnSprite;
        }
        settingPanel.SetActive(true);
    }
    public void VibrationOnOff()
    {
        if(isVibration == false)
        {
            isVibration = true;
            vibrationOnOffBtn.sprite = btnOffSprite;
        }
        else if(isVibration == true)
        {
            isVibration = false;
            vibrationOnOffBtn.sprite = btnOnSprite;
        }
        settingPanel.SetActive(true);
    }
    #endregion

    #region Next Scene Load
    public void LoadNextScene(string str)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    #endregion

    #region TaskDone
    public void TaskDone()
    {
        if (action == TeethGumsActionPerform.Clipper)
        {
            itemPickSFX.Play();
            clipper.SetActive(false);
        }
        else if (action == TeethGumsActionPerform.Brush)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.Excavator;
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
        else if (action == TeethGumsActionPerform.Excavator)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.GumsScope;
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, gumsScope, true));
            AfterTaskDonePerform();
        }
        else if (action == TeethGumsActionPerform.GumsScope)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.TeethLaser;
            StartCoroutine(ObjectEnableOrDisable(1.5f, teethLaser, true));
            StartCoroutine(ObjectEnableOrDisable(1.5f, gumsEffectedTeeth, true));
            StartCoroutine(ObjectEnableOrDisable(1.5f, dirtyTeethLayer, false));
            StartCoroutine(ObjectEnableOrDisable(1.5f, gum, false));
            gumsScope.SetActive(false);
            AfterTaskDonePerform();
        }

        else if (action == TeethGumsActionPerform.TeethLaser)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.GumCleaning;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, cottonPot, true));
            dirtyTeethLayer.SetActive(false);
            AfterTaskDonePerform();
        }
        else if (action == TeethGumsActionPerform.Spray)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.LaserLight;
            StartCoroutine(ObjectEnableOrDisable(0.5f, sprayBottel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, gumsEffectedTeeth, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, dirtyTeethLayer, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, laserLight, true));
            AfterTaskDonePerform();
        }
        else if (action == TeethGumsActionPerform.LaserLight)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.Germs;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethGumsPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, laserLight, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, true));
            AfterTaskDonePerform();


        }
        else if (action == TeethGumsActionPerform.Germs)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethShine, true));
            //dirtyTeethLayer.SetActive(false);
            //openMouth.sprite = cleanTeethLayer;
            print("All Task Done");
            StartCoroutine(LevelComplete());
        }
    }
    #endregion

    #region Funcations

    #region Gums Cleaning
    // First Task Tray Image On 1 by 1

    private int index =0;
    public void CottonImageOnTray()
    {
        //print(index);
        if (index == 0)
        {
            cottonTrayImages[0].gameObject.SetActive(true);
            index++;
        }
        else if (index == 1)
        {
            cottonTrayImages[1].gameObject.SetActive(true);
            index++;
        }
        else if (index == 2)
        {
            cottonTrayImages[2].gameObject.SetActive(true);
            index++;
        }
        else if (index == 3)
        {
            cottonTrayImages[3].gameObject.SetActive(true);
            index++;
        }
        dirtyDrop.GetComponent<CircleCollider2D>().enabled = true;
        taskFillbar.fillAmount += 0.25f;
        TaskFillBar();
        itemDropSFX.Play();
        if (cottonTrayImages[0].gameObject.activeSelf && cottonTrayImages[1].gameObject.activeSelf && cottonTrayImages[2].gameObject.activeSelf && cottonTrayImages[3].gameObject.activeSelf )
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.Spray;
            StartCoroutine(ObjectEnableOrDisable(0.5f, sprayBottel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, cottonPot, false));
            AfterTaskDonePerform();
        }
        StartCoroutine(TrayIEnumerator());
    }
    #endregion

    #region First Task
    // First Task Tray Image On 1 by 1
    public void ObjectEnableInTray(int index)
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
        if (trayImages[0].gameObject.activeSelf && trayImages[1].gameObject.activeSelf && trayImages[2].gameObject.activeSelf && trayImages[3].gameObject.activeSelf
            && trayImages[4].gameObject.activeSelf && trayImages[5].gameObject.activeSelf && trayImages[6].gameObject.activeSelf && trayImages[7].gameObject.activeSelf
            && trayImages[8].gameObject.activeSelf && trayImages[9].gameObject.activeSelf)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethGumsActionPerform.Brush;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, clipper, false));
            AfterTaskDonePerform();
        }
        StartCoroutine(TrayIEnumerator());
    }
    #endregion

    //private void ScopeTask()
    //{
    //    action = TeethGumsActionPerform.TeethLaser;
    //    StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, true));
    //    AfterTaskDonePerform();
    //}

    private void AfterTaskDonePerform()
    {
        injuredPart.transform.GetComponent<PolygonCollider2D>().enabled = true;
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
        cottonTray.transform.DOLocalMove(new Vector3(-991f, -344f, 0), 1f);
    }
    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(ObjectEnableOrDisable(0.5f, levelCompletePanel, true));
        StartCoroutine(ObjectEnableOrDisable(0.7f, finalParticle, true));
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

    IEnumerator ImageEnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSeconds(_Delay);
        activateObject.transform.GetComponent<Image>().enabled = isTrue;
    }
    IEnumerator EnableAnim(float _Delay, Animator activateObject)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.enabled = true;
    }
    #endregion
}
