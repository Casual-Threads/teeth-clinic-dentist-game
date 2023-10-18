using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DragCGS : MonoBehaviour
{
    public bool isCanvasObject;
    public AudioSource MouseDownSFX;
    public GameObject MouseDownIndicator, MouseUpIndicator;
    public ParticleSystem triggerParticles;
    public int downSortingOrder, upSortinggOrder;
    public int totalTriggers;
    private Vector2 InitialPosition;
    private Vector2 MousePosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float deltaX, deltaY;
    private ScalePingPong pingPong;
    private bool isPosAssigned, restPos;
    private BoxCollider2D boxCollider;
    private List<BoxCollider2D> boxColliders = new List<BoxCollider2D>();
    private Animator m_Animator;
    private int didTrigger;
    private bool inTrigger = false;
    private bool isSingleTeethOff, isYellowTeethOff, isReparedTeethOn , isbrown, isblack, isyellow;
 
    public Transform  downParent;
    TeethCleaning TeethCleaningController;
    TeethReparing TeethReparingController;
    public UnityEvent MouseDown;
    public UnityEvent MouseUp;
    public UnityEvent SoundPlaySFX;
    public UnityEvent SoundStopSFX;
    private int index = 0;
    private int blackIndex = 0;
    private int emptyIndex = 0;
    private int reparedIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        TeethCleaningController = FindObjectOfType<TeethCleaning>();
        TeethReparingController = FindObjectOfType<TeethReparing>();
        restPos = true;
        boxCollider = GetComponent<BoxCollider2D>();
        pingPong = GetComponent<ScalePingPong>();
        m_Animator = GetComponentInChildren<Animator>();
        if (m_Animator)
            m_Animator.enabled = false;
        var _BoxColliders2D = FindObjectsOfType<BoxCollider2D>();
        for(int i = 0; i < _BoxColliders2D.Length; i++)
        {
            boxColliders.Add(_BoxColliders2D[i]);
        }
        if (MouseUpIndicator)
        {
            MouseUpIndicator.SetActive(true);
        }
    }
    private void ColliderManager(bool isTrue)
    {
        for(int i = 0; i < boxColliders.Count; i++)
        {
            if(boxColliders[i] != boxCollider)
            {
                boxColliders[i].enabled = isTrue;
            }
        }
    }
    void OnMouseDown()
    {
        MouseDown.Invoke();
        var _Renderers = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < _Renderers.Length; i++)
        {
            _Renderers[i].sortingOrder = downSortingOrder;
        }
        if (pingPong) pingPong.enabled = false;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(true);
        if (MouseUpIndicator) MouseUpIndicator.SetActive(false);
        //if (m_Animator) m_Animator.enabled = true;
        if (triggerParticles) triggerParticles.Play();
        if (MouseDownSFX) MouseDownSFX.Play();
        if (!isPosAssigned)
        {
            isPosAssigned = true;
            InitialPosition = transform.localPosition;
        }
        if (isCanvasObject)
        {
            screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition); // I removed this line to prevent centring 
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        else
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.localPosition.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.localPosition.y;
        }
    }
    void OnMouseDrag()
    {
        if (isCanvasObject)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector2(MousePosition.x - deltaX, MousePosition.y - deltaY);
        } 
    }
    void OnMouseUp()
    {
        MouseUp.Invoke();
        SoundStopSFX.Invoke();
        var _Renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _Renderers.Length; i++)
        {
            _Renderers[i].sortingOrder = upSortinggOrder;
        }
        if (pingPong) pingPong.enabled = true;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(false);
        if (m_Animator) m_Animator.enabled = false;
        if (triggerParticles) triggerParticles.Stop();
        if (restPos)
        {
            transform.localPosition = InitialPosition;
            if (MouseUpIndicator)
            {
                MouseUpIndicator.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        #region Teeth Cleaning
        if (TeethCleaningController)
        {
            if (col.gameObject.name == "EmptyTray" && gameObject.name == "ToffeeAnim")
            {
                gameObject.SetActive(false);
                TeethCleaningController.ObjectEnable(0);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "LemonAnim")
            {
                gameObject.SetActive(false);
                TeethCleaningController.ObjectEnable(1);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "FishAnim")
            {
                gameObject.SetActive(false);
                TeethCleaningController.ObjectEnable(2);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "StrawberryAnim")
            {
                gameObject.SetActive(false);
                TeethCleaningController.ObjectEnable(3);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "BubbleAnim")
            {
                gameObject.SetActive(false);
                TeethCleaningController.ObjectEnable(4);
            }
            else if (col.gameObject.name == "Toffee" && gameObject.name == "Clipper")
            {
                TeethCleaningController.tray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethCleaningController.TaskDone();
            }
            else if (col.gameObject.name == "Lemon" && gameObject.name == "Clipper")
            {
                TeethCleaningController.tray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethCleaningController.TaskDone();
            }
            else if (col.gameObject.name == "FishBone" && gameObject.name == "Clipper")
            {
                TeethCleaningController.tray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethCleaningController.TaskDone();
            }
            else if (col.gameObject.name == "Strawberry" && gameObject.name == "Clipper")
            {
                TeethCleaningController.tray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethCleaningController.TaskDone();
            }
            else if (col.gameObject.name == "BubbleGum" && gameObject.name == "Clipper")
            {
                TeethCleaningController.tray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethCleaningController.TaskDone();
            }

            else if (col.gameObject.tag == "YellowTeethTag" && gameObject.name == "Brush")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethCleaningController.burshSFX.Play();
                if (col.gameObject.tag == "YellowTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);

                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethCleaningController.taskFillbar.fillAmount += 0.2f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 5)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }
            }
            else if (col.gameObject.tag == "YellowGreenDurtTag" && gameObject.name == "Excavator")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethCleaningController.excavatorSFX.Play();
                if (col.gameObject.tag == "YellowGreenDurtTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethCleaningController.taskFillbar.fillAmount += 0.1667f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 6)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }
            }
            else if (col.gameObject.tag == "BlackTeethTag" && gameObject.name == "Grinder")
            {
                gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).GetComponent<DOTweenAnimation>().DOPlay();
                TeethCleaningController.grinderSFX.Play();
                if (col.gameObject.tag == "BlackTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.15f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethCleaningController.taskFillbar.fillAmount += 0.25f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 4)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }
            }
            else if (col.gameObject.tag == "CrackTag" && gameObject.name == "Drill")
            {
                gameObject.transform.GetComponent<Animator>().enabled = true;
                TeethCleaningController.drillSFX.Play();
                if (col.gameObject.tag == "CrackTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false; TeethCleaningController.taskFillbar.fillAmount += 0.25f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 4)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }

            }

            else if (col.gameObject.tag == "ChewTag" && gameObject.name == "ChewPuller")
            {
                col.transform.GetChild(0).gameObject.SetActive(false);
                TeethCleaningController.chewPullerSFX.Play();
                if (col.gameObject.tag == "ChewTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 2f);
                    {
                        col.enabled = false;
                        TeethCleaningController.taskFillbar.fillAmount += 0.25f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 4)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }
            }

            else if (col.gameObject.tag == "GreenDotTag" && gameObject.name == "TeethLaser")
            {
                TeethCleaningController.teethLaserSFX.Play();
                if (col.gameObject.tag == "GreenDotTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                    {
                        col.enabled = false;
                        TeethCleaningController.taskFillbar.fillAmount += 0.0834f;
                        TeethCleaningController.TaskFillBar();
                        index++;
                        if (index == 12)
                        {
                            index = 0;
                            TeethCleaningController.TaskDone();
                        }

                    }
                }
            }
        }
        #endregion

        #region Teeth Reparing
        if (TeethReparingController)
        {
            if (col.gameObject.name == "EmptyTray" && gameObject.name == "ToffeeAnim")
            {
                gameObject.SetActive(false);
                TeethReparingController.ObjectEnable(0);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "LemonAnim")
            {
                gameObject.SetActive(false);
                TeethReparingController.ObjectEnable(1);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "FishAnim")
            {
                gameObject.SetActive(false);
                TeethReparingController.ObjectEnable(2);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "StrawberryAnim")
            {
                gameObject.SetActive(false);
                TeethReparingController.ObjectEnable(3);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "BubbleAnim")
            {
                gameObject.SetActive(false);
                TeethReparingController.ObjectEnable(4);
            }
            else if (col.gameObject.name == "Toffee" && gameObject.name == "Clipper")
            {
                TeethReparingController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethReparingController.TaskDone();
            }
            else if (col.gameObject.name == "Lemon" && gameObject.name == "Clipper")
            {
                TeethReparingController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethReparingController.TaskDone();
            }
            else if (col.gameObject.name == "FishBone" && gameObject.name == "Clipper")
            {
                TeethReparingController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethReparingController.TaskDone();
            }
            else if (col.gameObject.name == "Strawberry" && gameObject.name == "Clipper")
            {
                TeethReparingController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethReparingController.TaskDone();
            }
            else if (col.gameObject.name == "BubbleGum" && gameObject.name == "Clipper")
            {
                TeethReparingController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethReparingController.TaskDone();
            }
            else if ((col.gameObject.tag == "SingleTeethTag" || col.gameObject.tag == "EmptyTag" || col.gameObject.tag == "ReparedTeethTag") && gameObject.name == "Brush")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                //TeethReparingController.burshSFX.Play();
                SoundPlaySFX.Invoke();
                if (col.gameObject.tag == "SingleTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);

                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.125f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 8)
                        {
                            //isSingleTeethOff = true;
                            index = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "EmptyTag")
                {
                    col.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetChild(0).GetComponent<Image>().color.a - 0.2f);
                    if (col.transform.GetChild(0).GetComponent<Image>().color.a <= 0)
                    {
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        col.enabled = false;
                        emptyIndex++;
                        if (emptyIndex == 2)
                        {
                            isYellowTeethOff = true;
                            emptyIndex = 0;
                        }
                    }

                }
                if (col.gameObject.tag == "ReparedTeethTag")
                {
                    col.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetChild(0).GetComponent<Image>().color.a + 0.2f);
                    if (col.transform.GetChild(0).GetComponent<Image>().color.a >= 1)
                    {
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        col.enabled = false;
                        reparedIndex++;
                        if (reparedIndex == 2)
                        {
                            isReparedTeethOn = true;
                            reparedIndex = 0;
                        }
                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && isReparedTeethOn == true) && isYellowTeethOff == true)
                {
                    SoundStopSFX.Invoke();
                    TeethReparingController.TaskDone();
                }
            }
            else if (col.gameObject.tag == "DurtTag" && gameObject.name == "Excavator")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethReparingController.excavatorSFX.Play();
                if (col.gameObject.tag == "DurtTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.25f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 4)
                        {
                            index = 0;
                            TeethReparingController.TaskDone();
                            SoundStopSFX.Invoke();
                        }

                    }
                }
            }
            else if ((col.gameObject.tag == "DamagedTeethTag" || col.gameObject.tag == "BlackDamagedTeethTag") && gameObject.name == "FirstDrill")
            {
                gameObject.transform.GetComponent<Animator>().enabled = true;
                TeethReparingController.drillSFX.Play();

                if (col.gameObject.tag == "DamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.1667f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 6)
                        {
                            isbrown = true;
                            index = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "BlackDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.2f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        blackIndex++;
                        if (blackIndex == 6)
                        {
                            isblack = true;
                            blackIndex = 0;
                        }

                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && isbrown == true) && isblack == true)
                {
                    isblack = false;
                    TeethReparingController.TaskDone();
                    SoundStopSFX.Invoke();
                }
            }

            else if ((col.gameObject.tag == "BlackDamagedTeethTag" || col.gameObject.tag == "YellowDamagedTeethTag") && gameObject.name == "SecondDrill")
            {
                gameObject.transform.GetComponent<Animator>().enabled = true;
                gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                //TeethReparingController.drillSFX.Play();

                if (col.gameObject.tag == "BlackDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        blackIndex++;
                        if (blackIndex == 6)
                        {
                            isblack = true;
                            blackIndex = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "YellowDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.2f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.1667f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 6)
                        {
                            isyellow = true;
                            index = 0;
                        }

                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && isyellow == true) && isblack == true)
                {
                    TeethReparingController.drillSFX.Stop();
                    isyellow = false;
                    isblack = false;
                    TeethReparingController.TaskDone();
                    //SoundStopSFX.Invoke();
                }
            }

            else if(col.gameObject.name == "Pot" && gameObject.name == "Spoon")
            {
                col.gameObject.transform.GetComponent<Animator>().enabled = true;
                col.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.SetActive(false);

            }
            else if((col.gameObject.tag == "YellowDamagedTeethTag" || col.gameObject.tag == "WhiteDamagedTeethTag") && gameObject.name == "SpoonOne")
            {
                if (col.gameObject.tag == "YellowDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.enabled = false;
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        blackIndex++;
                        if (blackIndex == 6)
                        {
                            isblack = true;
                            blackIndex = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "WhiteDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.2f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.167f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 6)
                        {
                            isyellow = true;
                            index = 0;
                        }

                    }
                }
                if (isyellow == true && isblack == true)
                {
                    TeethReparingController.TaskDone();
                }
            }

            else if (col.gameObject.tag == "GreenDotTag" && gameObject.name == "TeethLaser")
            {
                TeethReparingController.teethLaserSFX.Play();
                //col.transform.GetChild(0).gameObject.SetActive(false);
                if (col.gameObject.tag == "GreenDotTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 3f);
                    {
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 0.25f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 4)
                        {
                            index = 0;
                            TeethReparingController.TaskDone();
                        }

                    }
                }
            }
            else if (col.gameObject.name == "WaterLayer" && gameObject.name == "WaterPipe")
            {
                TeethReparingController.teethLaserSFX.Play();
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                if (col.gameObject.name == "WaterLayer")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.2f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        TeethReparingController.taskFillbar.fillAmount += 1f;
                        TeethReparingController.TaskFillBar();
                        index++;
                        if (index == 1)
                        {
                            index = 0;
                            TeethReparingController.TaskDone();
                        }

                    }
                }
            }
            else if (col.gameObject.name == "WaterForPump" && gameObject.name == "WateringOutPump")
            {
                print("yes");
                TeethReparingController.teethLaserSFX.Play();
                gameObject.transform.gameObject.SetActive(false);
                TeethReparingController.secondWaterOutPump.SetActive(true);
                if (col.gameObject.name == "WaterForPump")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 5f);
                    col.enabled = false;
                    Invoke("LastTask", 5f);
                }
            }
        }
        #endregion
    }
    public void LastTask()
    {
        TeethReparingController.taskFillbar.fillAmount += 1f;
        TeethReparingController.TaskFillBar();
        index++;
        if (index == 1)
        {
            index = 0;
            TeethReparingController.TaskDone();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "BlackDamagedTeethTag" || collision.gameObject.tag == "YellowDamagedTeethTag") && gameObject.name == "SecondDrill")
        {
            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else if (collision.gameObject.name == "WaterLayer" && gameObject.name == "WaterPipe")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        inTrigger = false;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        inTrigger = true;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(false);
    }
    private void OnEnable()
    {
        didTrigger = 0;
        if (boxCollider) boxCollider.enabled = true;
        if (m_Animator) m_Animator.enabled = false;
        if (pingPong) pingPong.enabled = true;
        if (MouseDownIndicator)
        {
            if (MouseDownIndicator.GetComponentInChildren<Image>())
            {
                MouseDownIndicator.GetComponentInChildren<Image>().enabled = true;
            }
        }
        if (MouseUpIndicator)
        {
            if (MouseUpIndicator.GetComponentInChildren<Image>())
            {
                MouseUpIndicator.GetComponentInChildren<Image>().enabled = true;
            }
        }
    }
    private void OnDisable()
    {
        DisableObjects();
        if (MouseDownIndicator)
        {
            if (MouseDownIndicator.GetComponentInChildren<Image>())
            {
                MouseDownIndicator.GetComponentInChildren<Image>().enabled = false;
            }
        }
        if (MouseUpIndicator)
        {
            if (MouseUpIndicator.GetComponentInChildren<Image>())
            {
                MouseUpIndicator.GetComponentInChildren<Image>().enabled = false;
            }
        }
        if (isPosAssigned) transform.localPosition = InitialPosition;
    }
    private void DisableObjects()
    {
        if (boxCollider) boxCollider.enabled = false;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(false);
        if (MouseUpIndicator) MouseUpIndicator.SetActive(false);
    }

    IEnumerator EnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.SetActive(isTrue);
    }

}
