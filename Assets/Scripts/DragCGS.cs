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

    //Gulfam Poration
    public UnityEvent MouseDown;
    public UnityEvent MouseUp;
    public UnityEvent SoundPlaySFX;
    public UnityEvent SoundStopSFX;
    TeethCleaning TeethCleaningController;
    TeethReparing TeethReparingController;
    TeethBraces TeethBracesController;
    ObjectOff ObjectOffController;
    public Transform  downParent;
    private int IndexOne = 0;
    private int IndexTwo = 0;
    private int IndexThree = 0;
    private bool IsAlphaReduced, isAlphaIncreased, IsSecondObjectAlphaReduced, isSecondObjectAlphaIncreased;
    private bool isPinkGermOff, isBlueGermOff, isGreenGermOff, isOrangeGermOff;

    #region Start Region
    // Start is called before the first frame update
    void Start()
    {
        TeethCleaningController = FindObjectOfType<TeethCleaning>();
        TeethReparingController = FindObjectOfType<TeethReparing>();
        TeethBracesController = FindObjectOfType<TeethBraces>();
        ObjectOffController = FindObjectOfType<ObjectOff>();
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
    #endregion

    #region OnMouseDown
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
    #endregion

    #region OnMouseDrag
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
    #endregion

    #region OnMouseUp
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

    #endregion
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
                        IndexOne++;
                        if (IndexOne == 5)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 6)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 12)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 8)
                        {
                            IsAlphaReduced = true;
                            IndexOne = 0;
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
                        IndexTwo++;
                        if (IndexTwo == 2)
                        {
                            IsSecondObjectAlphaReduced = true;
                            IndexTwo = 0;
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
                        IndexThree++;
                        if (IndexThree == 2)
                        {
                            isAlphaIncreased = true;
                            IndexThree = 0;
                        }
                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && isAlphaIncreased == true) && IsAlphaReduced == true && IsSecondObjectAlphaReduced == true)
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
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 6)
                        {
                            IsAlphaReduced = true;
                            IndexOne = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "BlackDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.2f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        IndexTwo++;
                        if (IndexTwo == 6)
                        {
                            isAlphaIncreased = true;
                            IndexTwo = 0;
                        }

                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && IsAlphaReduced == true) && isAlphaIncreased == true)
                {
                    isSecondObjectAlphaIncreased = false;
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
                        IndexOne++;
                        if (IndexOne == 6)
                        {
                            IsAlphaReduced = true;
                            IndexOne = 0;
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
                        IndexTwo++;
                        if (IndexTwo == 6)
                        {
                            isAlphaIncreased = true;
                            IndexTwo = 0;
                        }

                    }
                }
                if ((TeethReparingController.taskFillbar.fillAmount == 1 && isAlphaIncreased == true) && IsAlphaReduced == true)
                {
                    TeethReparingController.drillSFX.Stop();
                    isAlphaIncreased = false;
                    isSecondObjectAlphaIncreased = false;
                    TeethReparingController.TaskDone();
                    //SoundStopSFX.Invoke();
                }
            }

            else if (col.gameObject.name == "Pot" && gameObject.name == "Spoon")
            {
                col.gameObject.transform.GetComponent<Animator>().enabled = true;
                col.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.SetActive(false);

            }

            else if ((col.gameObject.tag == "YellowDamagedTeethTag" || col.gameObject.tag == "WhiteDamagedTeethTag") && gameObject.name == "SpoonOne")
            {
                if (col.gameObject.tag == "YellowDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.enabled = false;
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        IndexOne++;
                        if (IndexOne == 6)
                        {
                            IsAlphaReduced = true;
                            IndexOne = 0;
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
                        IndexTwo++;
                        if (IndexTwo == 6)
                        {
                            isAlphaIncreased = true;
                            IndexTwo = 0;
                        }

                    }
                }
                if (IsAlphaReduced == true && isAlphaIncreased == true)
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
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
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
                        IndexOne++;
                        if (IndexOne == 1)
                        {
                            IndexOne = 0;
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

        #region Teeth Braces

        if (TeethBracesController)
        {
            if (col.gameObject.name == "EmptyTray" && gameObject.name == "ToffeeAnim")
            {
                gameObject.SetActive(false);
                TeethBracesController.ObjectEnableInTray(0);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "LemonAnim")
            {
                gameObject.SetActive(false);
                TeethBracesController.ObjectEnableInTray(1);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "FishAnim")
            {
                gameObject.SetActive(false);
                TeethBracesController.ObjectEnableInTray(2);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "StrawberryAnim")
            {
                gameObject.SetActive(false);
                TeethBracesController.ObjectEnableInTray(3);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "BubbleAnim")
            {
                gameObject.SetActive(false);
                TeethBracesController.ObjectEnableInTray(4);
            }
            else if (col.gameObject.name == "Toffee" && gameObject.name == "Clipper")
            {
                TeethBracesController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethBracesController.TaskDone();
            }
            else if (col.gameObject.name == "Lemon" && gameObject.name == "Clipper")
            {
                TeethBracesController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethBracesController.TaskDone();
            }
            else if (col.gameObject.name == "FishBone" && gameObject.name == "Clipper")
            {
                TeethBracesController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethBracesController.TaskDone();
            }
            else if (col.gameObject.name == "Strawberry" && gameObject.name == "Clipper")
            {
                TeethBracesController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethBracesController.TaskDone();
            }
            else if (col.gameObject.name == "BubbleGum" && gameObject.name == "Clipper")
            {
                TeethBracesController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                TeethBracesController.TaskDone();
            }

            else if ((col.gameObject.tag == "SingleTeethTag" || col.gameObject.tag == "EmptyTag") && gameObject.name == "Brush")
            {          
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                //TeethReparingController.burshSFX.Play();
                SoundPlaySFX.Invoke();
                if (col.gameObject.tag == "SingleTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    if (col.GetComponent<Image>().color == new Color(1, 1, 1, 1))
                    {
                        col.enabled = false;
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        TeethBracesController.taskFillbar.fillAmount += 0.0833f;
                        TeethBracesController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 12)
                        {
                            isAlphaIncreased = true;
                            IndexOne = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "EmptyTag")
                {
                    col.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    if (col.transform.GetChild(0).GetComponent<Image>().color == new Color(1, 1, 1, 1))
                    {
                        col.enabled = false;
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        IndexTwo++;
                        if (IndexTwo == 2)
                        {
                            IsAlphaReduced = true;
                            IndexTwo = 0;
                        }
                    }

                }
                if (isAlphaIncreased == true &&  IsAlphaReduced == true)
                {
                    SoundStopSFX.Invoke();
                    TeethBracesController.TaskDone();
                }
            }

            else if (col.gameObject.tag == "DurtTag" && gameObject.name == "Excavator")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethBracesController.excavatorSFX.Play();
                if (col.gameObject.tag == "DurtTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethBracesController.taskFillbar.fillAmount += 0.25f;
                        TeethBracesController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
                            TeethBracesController.TaskDone();
                            SoundStopSFX.Invoke();
                        }

                    }
                }
            }

            else if ((col.gameObject.tag == "DamagedTeethTag" || col.gameObject.tag == "BlackDamagedTeethTag" || col.gameObject.tag == "SingleTeethTag") && gameObject.name == "MiniMicro")
            {
                //gameObject.transform.GetComponent<Animator>().enabled = true;
                //TeethBracesController.drillSFX.Play();

                if (col.gameObject.tag == "DamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.3f);
                    if (col.GetComponent<Image>().color.a >= 1)
                    {
                        col.enabled = false;
                        TeethBracesController.taskFillbar.fillAmount += 0.1667f;
                        TeethBracesController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 6)
                        {
                            isAlphaIncreased = true;
                            IndexOne = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "BlackDamagedTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.3f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.enabled = false;
                        IndexTwo++;
                        if (IndexTwo == 6)
                        {
                            IsAlphaReduced = true;
                            IndexTwo = 0;
                        }

                    }
                }
                if (col.gameObject.tag == "SingleTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);

                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.enabled = false;
                        IndexThree++;
                        if (IndexThree == 12)
                        {
                            IsSecondObjectAlphaReduced = true;
                            IndexThree = 0;
                        }

                    }
                }
                if (isAlphaIncreased == true && IsAlphaReduced == true && IsSecondObjectAlphaReduced == true)
                {
                    TeethBracesController.TaskDone();
                    SoundStopSFX.Invoke();
                }
            }

            else if (col.gameObject.tag == "DamagedTeethTag" && gameObject.name == "TeethCutter")
            {
                TeethBracesController.teethTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                //TeethReparingController.drillSFX.Play();
                col.transform.GetChild(0).gameObject.SetActive(true);

                col.transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
                if (col.gameObject.tag == "DamagedTeethTag")
                {
                    if (downParent)
                    {
                        col.transform.GetChild(0).GetChild(0).parent = downParent;
                    }

                    StartCoroutine(ImageEnableOrDisable(1.8f, col.gameObject, false));
                    TeethBracesController.TaskDone();
                }

            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethOne")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(0);
            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethTwo")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(1);
            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethThree")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(2);
            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethFour")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(3);
            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethFive")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(4);
            }
            else if (col.gameObject.name == "TeethTray" && gameObject.name == "CrackTeethSix")
            {
                gameObject.SetActive(false);
                TeethBracesController.TeethTrayInOut(5);
            }
            else if (col.gameObject.name == "LeftDown" && gameObject.name == "LeftDownGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "DownMid" && gameObject.name == "DownMidGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "RightSecondLast" && gameObject.name == "RightLastSecondGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperRight" && gameObject.name == "UpperRightGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperMid" && gameObject.name == "UpperMidGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperLeft" && gameObject.name == "UpperLeftGameObject")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.tag == "BraceSTag" && gameObject.name == "Brace")
            {
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.BraceTaskDone();
            }
            else if (col.gameObject.tag == "GreenDotTag" && gameObject.name == "TeethLaser")
            {
                TeethBracesController.teethLaserSFX.Play();
                //col.transform.GetChild(0).gameObject.SetActive(false);
                if (col.gameObject.tag == "GreenDotTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 3f);
                    {
                        col.enabled = false;
                        TeethBracesController.taskFillbar.fillAmount += 0.25f;
                        TeethBracesController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
                            TeethBracesController.TaskDone();
                        }

                    }
                }
            }


        }
        #endregion

        #region Germs Killing
        if (col.gameObject.name == "MagnifyingGlass" && gameObject.name == "PinkGermParent")
        {
            gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
            gameObject.transform.GetComponent<PolygonCollider2D>().isTrigger = false;
            //print("yes");
        }
        else if (col.gameObject.name == "PinkGerm" && gameObject.name == "PinkGermsKiller")
        {
            if (col.gameObject.name == "PinkGerm")
            {
                col.enabled = false;
                TeethReparingController.teethLaserSFX.Play();
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    TeethReparingController.taskFillbar.fillAmount += 0.125f;
                    TeethReparingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        isPinkGermOff = true;
                        IndexOne = 0;
                    }

                }
            }
        }
        else if (col.gameObject.name == "BlueGerm" && gameObject.name == "BlueGermsKiller")
        {
            //col.transform.GetChild(0).gameObject.SetActive(false);
            if (col.gameObject.name == "BlueGerm")
            {
                TeethReparingController.teethLaserSFX.Play();
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    col.enabled = false;
                    TeethReparingController.taskFillbar.fillAmount += 0.125f;
                    TeethReparingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        IndexOne = 0;
                        isBlueGermOff = true;
                    }

                }
            }
        }
        else if (col.gameObject.name == "GreenGerm" && gameObject.name == "GreenGermsKiller")
        {
            TeethReparingController.teethLaserSFX.Play();
            //col.transform.GetChild(0).gameObject.SetActive(false);
            if (col.gameObject.name == "GreenGerm")
            {
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    col.enabled = false;
                    TeethReparingController.taskFillbar.fillAmount += 0.125f;
                    TeethReparingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        IndexOne = 0;
                        isGreenGermOff = true;
                    }

                }
            }
        }
        else if (col.gameObject.name == "OrangeGerm" && gameObject.name == "OrangeGermsKiller")
        {
            TeethReparingController.teethLaserSFX.Play();
            //col.transform.GetChild(0).gameObject.SetActive(false);
            if (col.gameObject.name == "OrangeGerm")
            {
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    col.enabled = false;
                    TeethReparingController.taskFillbar.fillAmount += 0.125f;
                    TeethReparingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        IndexOne = 0;
                        isOrangeGermOff = true;
                        //TeethReparingController.TaskDone();
                    }

                }
            }
        }
        #endregion
    }

    public void GermsTaskDone()
    {
        if(isPinkGermOff == true && isBlueGermOff == true && isGreenGermOff == true && isOrangeGermOff == true)
        {
            TeethReparingController.TaskDone();
        }
    } 
    public void LastTask()
    {
        TeethReparingController.taskFillbar.fillAmount += 1f;
        TeethReparingController.TaskFillBar();
        IndexOne++;
        if (IndexOne == 1)
        {
            IndexOne = 0;
            TeethReparingController.TaskDone();
        }
    }

    public void OffObject(GameObject activateObject)
    {
        activateObject.transform.GetComponent<Image>().enabled = false;
        //activateObject.SetActive(isTrue);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TeethReparingController)
        {
            if ((collision.gameObject.tag == "BlackDamagedTeethTag" || collision.gameObject.tag == "YellowDamagedTeethTag") && gameObject.name == "SecondDrill")
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else if (collision.gameObject.name == "WaterLayer" && gameObject.name == "WaterPipe")
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.name == "MagnifyingGlass" && gameObject.name == "PinkGermParent")
        {
            gameObject.transform.GetComponent<PolygonCollider2D>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }

        inTrigger = false;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        inTrigger = true;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(false);

        //if (collision.gameObject.name == "PinkGerm" && gameObject.name == "PinkGermsKiller")
        //{

        //}
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

    IEnumerator ObjectEnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSeconds(_Delay);
        activateObject.SetActive(isTrue);
    }
    IEnumerator ImageEnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSeconds(_Delay);
        activateObject.transform.GetComponent<Image>().enabled = isTrue;
        //StartCoroutine(ObjectEnableOrDisable(1f, activateObject, false));
    }

}
