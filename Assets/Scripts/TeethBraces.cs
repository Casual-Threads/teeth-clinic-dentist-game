using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum TeethBracesActionPerform
{
    none, Clipper, Brush, Excavator, MiniMicro, TeethCutter, NewTeethInsert, Brace, TeethLaser, Germs
}

public class TeethBraces : MonoBehaviour
{
    public static TeethBraces Instance;
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
    public TeethBracesActionPerform action;
    [Header("Dragable Items")]
    public GameObject clipper;
    public GameObject brush, excavator, miniMicro, teethCutter, braces, teethLaser;
    [Header("Items Layers")]
    public GameObject tray;
    public GameObject emptyTray, teethTray, newTeethTray, dirtyTeethLayer, whiteTeethLayer, whiteSingleTeethLayer, teethShine;
    [Header("Panels")]
    public GameObject TeethBracesPanel;
    public GameObject levelCompletePanel, settingPanel, RateUsPanel, loadingPanel, germsPanel, darkPanel;
    [Header("Images")]
    public Image openMouth;
    public Image taskFillbar, loadingFillbar, lightImage, lightWhiteLayer, musicOnOffBtn, vibrationOnOffBtn, bracedTeethLayer;
    [Header("Image Arrays")]
    public Image[] trayImages;
    public Image[] teethTrayImages;
    public Image[] starImages;
    [Header("Arrays for Indications")]
    public Image[] durtLayer;
    public Image[] crackTeethLayer, blackDamagedTeethLayer, yellowSingleTeethLayer;
    [Header("Sprites")]
    public Sprite goldStarSprite;
    public Sprite grayStarSprite, onLightSprite, offLightSprite, cleanTeethLayer, onSprite, OffSprite;
    [Header("Particle System")]
    public ParticleSystem taskDoneParticle;
    public GameObject finalParticle;
    [Header("Sounds")]
    public AudioSource itemPickSFX;
    public AudioSource itemDropSFX, burshSFX, excavatorSFX, drillSFX, teethLaserSFX;
    private bool isLight, isRateus, isMusic, isVibration;
    AsyncOperation asyncLoad;
    private bool isCheck;
    private int NewTeethInsertIndex = 0;
    private int bracesIndex = 0;
    public Transform downParent;
    void Start()
    {
        action = TeethBracesActionPerform.Clipper;
    }

    #region LightOnOFF
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
        if (action == TeethBracesActionPerform.Clipper)
        {
            itemPickSFX.Play();
            clipper.SetActive(false);
        }
        else if(action == TeethBracesActionPerform.Brush)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, true));
            whiteSingleTeethLayer.SetActive(true);
            AfterTaskDonePerform();
            for (int i = 0; i < durtLayer.Length; i++)
            {
                durtLayer[i].transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    durtLayer[i].transform.parent = downParent;
                }
            }
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethBracesActionPerform.Excavator)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, excavator, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, miniMicro, true));
            AfterTaskDonePerform();
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethBracesActionPerform.MiniMicro)
        {
            taskDoneParticle.gameObject.SetActive(true);
            StartCoroutine(ObjectEnableOrDisable(0.5f, miniMicro, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethCutter, true));
            AfterTaskDonePerform();
            StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if(action == TeethBracesActionPerform.TeethCutter)
        {
            itemPickSFX.Play();
            teethCutter.transform.GetComponent<Image>().enabled = false;
            //taskDoneParticle.gameObject.SetActive(true);
            //StartCoroutine(EnableOrDisable(0.5f, teethCutter, false));
            //StartCoroutine(EnableOrDisable(0.5f, spoonWithPot, true));
            //AfterTaskDonePerform();
            //StartCoroutine(AfterTaskColliderAndIndicationsOn());
        }
        else if (action == TeethBracesActionPerform.TeethLaser)
        {
            taskDoneParticle.gameObject.SetActive(true);
            //action = TeethReparingActionPerform.PourWater;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, false));
            StartCoroutine(ObjectEnableOrDisable(0.3f, TeethBracesPanel, false));
            StartCoroutine(ObjectEnableOrDisable(0.5f, germsPanel, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, darkPanel, true));
            tray.SetActive(false);
            dirtyTeethLayer.SetActive(false);
            bracedTeethLayer.gameObject.SetActive(true);
            bracedTeethLayer.sprite = cleanTeethLayer;
            AfterTaskDonePerform();

        }
        //else if (action == TeethBracesActionPerform.WateringOut)
        //{
        //    taskDoneParticle.gameObject.SetActive(true);
        //    action = TeethBracesActionPerform.Germs;
        //    StartCoroutine(EnableOrDisable(0.3f, waterOutPump, false));
        //    StartCoroutine(EnableOrDisable(0.3f, TeethBracesPanel, false));
        //    StartCoroutine(EnableOrDisable(0.5f, germsPanel, true));
        //    StartCoroutine(EnableOrDisable(0.5f, darkPanel, true));
        //    //dirtyTeethLayer.SetActive(false);
        //    //openMouth.sprite = cleanTeethLayer;
        //    //print("All Task Done");
        //    //StartCoroutine(LevelComplete());
        //}
        //else if (action == TeethBracesActionPerform.Germs)
        //{
        //    taskDoneParticle.gameObject.SetActive(true);
        //    action = TeethBracesActionPerform.Germs;
        //    StartCoroutine(EnableOrDisable(0.5f, germsPanel, false));
        //    StartCoroutine(EnableOrDisable(0.5f, darkPanel, false));
        //    StartCoroutine(EnableOrDisable(0.5f, teethShine, true));
        //    //dirtyTeethLayer.SetActive(false);
        //    //openMouth.sprite = cleanTeethLayer;
        //    print("All Task Done");
        //    StartCoroutine(LevelComplete());
        //}
    }

    IEnumerator AfterTaskColliderAndIndicationsOn()
    {
        yield return new WaitForSeconds(1f);

        if(action == TeethBracesActionPerform.Brush)
        {
            for (int i = 0; i < yellowSingleTeethLayer.Length; i++)
            {
                yellowSingleTeethLayer[i].transform.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
        else if (action == TeethBracesActionPerform.MiniMicro)
        {
            for (int i = 0; i < crackTeethLayer.Length; i++)
            {
                crackTeethLayer[i].transform.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }

        StartCoroutine(ChangeActionType());
    }
    IEnumerator ChangeActionType()
    {
        yield return new WaitForSeconds(1f);

        if (action == TeethBracesActionPerform.Brush)
        {
            action = TeethBracesActionPerform.Excavator;
        }
        else if (action == TeethBracesActionPerform.Excavator)
        {
            action = TeethBracesActionPerform.MiniMicro;
        }
        else if (action == TeethBracesActionPerform.MiniMicro)
        {
            action = TeethBracesActionPerform.TeethCutter;
        }
    }
    #endregion

    #region Funcations

    #region Tray Images ON Task
    // First Task Tray Image On 1 by 1
    public void TeethTrayInOut(int index)
    {
        if (index == 0)
        {
            teethTrayImages[0].gameObject.SetActive(true);
            crackTeethLayer[0].gameObject.SetActive(false);
        }
        else if (index == 1)
        {
            teethTrayImages[1].gameObject.SetActive(true);
            crackTeethLayer[1].gameObject.SetActive(false);
        }
        else if (index == 2)
        {
            teethTrayImages[2].gameObject.SetActive(true);
            crackTeethLayer[2].gameObject.SetActive(false);
        }
        else if (index == 3)
        {
            teethTrayImages[3].gameObject.SetActive(true);
            crackTeethLayer[3].gameObject.SetActive(false);
        }
        else if (index == 4)
        {
            teethTrayImages[4].gameObject.SetActive(true);
            crackTeethLayer[4].gameObject.SetActive(false);
        }
        else if (index == 5)
        {
            teethTrayImages[5].gameObject.SetActive(true);
            crackTeethLayer[5].gameObject.SetActive(false);
        }
        taskFillbar.fillAmount += 0.1667f;
        TaskFillBar();
        itemDropSFX.Play();
        StartCoroutine(ImageEnableOrDisable(0.5f, teethCutter, true));
        if (teethTrayImages[0].gameObject.activeSelf && teethTrayImages[1].gameObject.activeSelf && teethTrayImages[2].gameObject.activeSelf && teethTrayImages[3].gameObject.activeSelf
            && teethTrayImages[4].gameObject.activeSelf && teethTrayImages[5].gameObject.activeSelf)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethBracesActionPerform.NewTeethInsert;
            //StartCoroutine(EnableOrDisable(0.5f, brush, true));
            StartCoroutine(ImageEnableOrDisable(0.5f, teethCutter, false));
            newTeethTray.transform.DOLocalMove(new Vector3(-19f, -746f, 0), 1f);
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
            action = TeethBracesActionPerform.Brush;
            StartCoroutine(ObjectEnableOrDisable(0.5f, brush, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, clipper, false));
            AfterTaskDonePerform();
        }
        StartCoroutine(TrayIEnumerator());
    }
    #endregion

    #region Brace  Task Done
    // First Task Tray Image On 1 by 1
    public void NewTeetInsert()
    {
        NewTeethInsertIndex++;
        taskDoneParticle.gameObject.SetActive(true);
        taskFillbar.fillAmount += 0.167f;
        TaskFillBar();
        itemDropSFX.Play();
        StartCoroutine(ObjectEnableOrDisable(1.5f, taskDoneParticle.gameObject, false));
        if (NewTeethInsertIndex == 6)
        {
            action = TeethBracesActionPerform.Brace;
            StartCoroutine(ObjectEnableOrDisable(1f, braces, true));
            newTeethTray.transform.DOLocalMove(new Vector3(1155f, -746f, 0), 1f);
            AfterTaskDonePerform();
        }
        StartCoroutine(TrayIEnumerator());
    }
    #endregion

    #region Brace  Task Done
    // First Task Tray Image On 1 by 1
    public void BraceTaskDone()
    {
        bracesIndex++;
        taskFillbar.fillAmount += 0.04167f;
        TaskFillBar();
        itemDropSFX.Play();
        if (bracesIndex == 24)
        {
            taskDoneParticle.gameObject.SetActive(true);
            action = TeethBracesActionPerform.TeethLaser;
            StartCoroutine(ObjectEnableOrDisable(0.5f, teethLaser, true));
            StartCoroutine(ObjectEnableOrDisable(0.5f, braces, false));
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
        teethTray.transform.DOLocalMove(new Vector3(-991f, -344f, 0), 1f);
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
