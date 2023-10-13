using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public enum TeethReparingActionPerform
{
    none, Clipper, Brush, Excavator, Grinder, Drill, ChewPuller, TeethLaser
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
    public TeethReparingActionPerform action;
    public GameObject clipper;
    public GameObject brush, excavator, grinder, drill, chewPuller, teethLaser;
    public GameObject tray/*, cleanTeethLayer*/, dirtyTeethLayer;
    public Image openMouth;
    public Image taskFillbar, lightImage, lightWhiteLayer;
    public Image[] trayImages;
    public Image[] starImages;
    public Sprite goldStarSprite;
    public Sprite grayStarSprite, onLightSprite, offLightSprite, cleanTeethLayer;
    public ParticleSystem taskDoneParticle;
    private bool isLight;
    //public Transform startParent, downParent;
    void Start()
    {
        action = TeethReparingActionPerform.Clipper;
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

    public void TaskDone()
    {
        if (action == TeethReparingActionPerform.Clipper)
        {
            print("Clipper All Task Done");
        }
        else
        {
            taskDoneParticle.Play();
        }
        
        if (action == TeethReparingActionPerform.Clipper)
        {
            clipper.SetActive(false);
        }
        else if(action == TeethReparingActionPerform.Brush)
        {
            action = TeethReparingActionPerform.Excavator;
            StartCoroutine(EnableOrDisable(0.5f, brush, false));
            StartCoroutine(EnableOrDisable(0.5f, excavator, true));

        }
        else if(action == TeethReparingActionPerform.Excavator)
        {
            action = TeethReparingActionPerform.Grinder;
            StartCoroutine(EnableOrDisable(0.5f, excavator, false));
            StartCoroutine(EnableOrDisable(0.5f, grinder, true));
        }
        else if(action == TeethReparingActionPerform.Grinder)
        {
            action = TeethReparingActionPerform.Drill;
            StartCoroutine(EnableOrDisable(0.5f, grinder, false));
            StartCoroutine(EnableOrDisable(0.5f, drill, true));
        }
        else if(action == TeethReparingActionPerform.Drill)
        {
            action = TeethReparingActionPerform.ChewPuller;
            StartCoroutine(EnableOrDisable(0.5f, drill, false));
            StartCoroutine(EnableOrDisable(0.5f, chewPuller, true));
        }
        else if(action == TeethReparingActionPerform.ChewPuller)
        {
            action = TeethReparingActionPerform.TeethLaser;
            StartCoroutine(EnableOrDisable(0.5f, chewPuller, false));
            StartCoroutine(EnableOrDisable(0.5f, teethLaser, true));
        }
        else if(action == TeethReparingActionPerform.TeethLaser)
        {
            StartCoroutine(EnableOrDisable(0.5f, teethLaser, false));
            dirtyTeethLayer.SetActive(false);
            openMouth.sprite = cleanTeethLayer;
            print("All Task Done");
        }
        if(action == TeethReparingActionPerform.Clipper && action == TeethReparingActionPerform.TeethLaser)
        {
            print("Clipper All Task Done");
        }
        else
        {
            taskFillbar.fillAmount = 0f;
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].sprite = grayStarSprite;
            }
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
        if(taskFillbar.fillAmount >= 0.2f)
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
        StartCoroutine(EnableOrDisable(0.5f, clipper, true));
        if (trayImages[0].gameObject.activeSelf && trayImages[1].gameObject.activeSelf && trayImages[2].gameObject.activeSelf && trayImages[3].gameObject.activeSelf
            && trayImages[4].gameObject.activeSelf && trayImages[5].gameObject.activeSelf && trayImages[6].gameObject.activeSelf && trayImages[7].gameObject.activeSelf
            && trayImages[8].gameObject.activeSelf && trayImages[9].gameObject.activeSelf)
        {
            taskDoneParticle.Play();
            action = TeethReparingActionPerform.Brush;
            StartCoroutine(EnableOrDisable(0.5f, brush, true));
            StartCoroutine(EnableOrDisable(0.5f, clipper, false));
            taskFillbar.fillAmount = 0f;
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].sprite = grayStarSprite;
            }
        }
        StartCoroutine(TrayIEnumerator());
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
}
