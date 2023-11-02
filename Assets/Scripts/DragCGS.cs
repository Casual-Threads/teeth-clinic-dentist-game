using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DragCGS : MonoBehaviour
{
    public bool isCanvasObject;

    private Vector2 InitialPosition;
    private Vector2 MousePosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float deltaX, deltaY;
    private bool isPosAssigned, restPos;

    private int didTrigger;
    private bool inTrigger = false;

    //Gulfam Poration
    public UnityEvent MouseDown;
    public UnityEvent MouseUp;
    //public UnityEvent SoundPlaySFX;
    //public UnityEvent SoundStopSFX;
    TeethCleaning TeethCleaningController;
    TeethReparing TeethReparingController;
    TeethBraces TeethBracesController;
    TeethGums TeethGumsController;
    GermsKillingScript GermKillingController;
    public Transform  downParent;
    private int IndexOne = 0;
    private int IndexTwo = 0;
    private int IndexThree = 0;
    private bool IsAlphaReduced, isAlphaIncreased, IsSecondObjectAlphaReduced, isSecondObjectAlphaIncreased;
    //private bool isPinkGermOff, isBlueGermOff, isGreenGermOff, isOrangeGermOff;

    #region Start Region
    // Start is called before the first frame update
    void Start()
    {
        TeethCleaningController = FindObjectOfType<TeethCleaning>();
        TeethReparingController = FindObjectOfType<TeethReparing>();
        TeethBracesController = FindObjectOfType<TeethBraces>();
        TeethGumsController = FindObjectOfType<TeethGums>();
        GermKillingController = FindObjectOfType<GermsKillingScript>();
        restPos = true;
    }
    #endregion

    #region OnMouseDown
    void OnMouseDown()
    {
        MouseDown.Invoke();
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
        if (restPos)
        {
            transform.localPosition = InitialPosition;
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                            TeethCleaningController.burshSFX.Stop();
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
                            TeethCleaningController.excavatorSFX.Stop();
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
                            TeethCleaningController.grinderSFX.Stop();
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
                            TeethCleaningController.drillSFX.Stop();
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
                            TeethCleaningController.chewPullerSFX.Stop();
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
                            TeethCleaningController.teethLaserSFX.Stop();
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                TeethReparingController.burshSFX.Play();
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
                    TeethReparingController.burshSFX.Stop();
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
                            TeethReparingController.excavatorSFX.Stop();
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
                    TeethReparingController.TaskDone();
                    TeethReparingController.drillSFX.Stop();
                }
            }

            else if ((col.gameObject.tag == "BlackDamagedTeethTag" || col.gameObject.tag == "YellowDamagedTeethTag") && gameObject.name == "SecondDrill")
            {
                gameObject.transform.GetComponent<Animator>().enabled = true;
                gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethReparingController.drillSFX.Play();

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
                    TeethReparingController.TaskDone();
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
                            TeethReparingController.teethLaserSFX.Stop();
                            TeethReparingController.TaskDone();
                        }

                    }
                }
            }

            else if (col.gameObject.name == "WaterLayer" && gameObject.name == "WaterPipe")
            {
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
                TeethBracesController.burshSFX.Play();
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
                            TeethBracesController.burshSFX.Stop();
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
                    TeethBracesController.burshSFX.Stop();
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
                            TeethBracesController.excavatorSFX.Stop();
                        }

                    }
                }
            }

            else if ((col.gameObject.tag == "DamagedTeethTag" || col.gameObject.tag == "BlackDamagedTeethTag" || col.gameObject.tag == "SingleTeethTag") && gameObject.name == "MiniMicro")
            {

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
                }
            }

            else if (col.gameObject.tag == "DamagedTeethTag" && gameObject.name == "TeethCutter")
            {
                gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                TeethBracesController.teethTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (col.gameObject.tag == "DamagedTeethTag")
                {
                    if (downParent)
                    {
                        col.transform.GetChild(0).GetChild(0).parent = downParent;
                    }

                    StartCoroutine(ImageEnableOrDisable(1.5f, col.gameObject, false));
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
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "DownMid" && gameObject.name == "DownMidGameObject")
            {
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "RightSecondLast" && gameObject.name == "RightLastSecondGameObject")
            {
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperRight" && gameObject.name == "UpperRightGameObject")
            {
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperMid" && gameObject.name == "UpperMidGameObject")
            {
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.name == "UpperLeft" && gameObject.name == "UpperLeftGameObject")
            {
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.NewTeetInsert();
            }
            else if (col.gameObject.tag == "BraceSTag" && gameObject.name == "Brace")
            {

                gameObject.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                col.enabled = false;
                col.gameObject.GetComponent<Image>().enabled = true;
                TeethBracesController.BraceTaskDone();
            }
            else if (col.gameObject.tag == "GreenDotTag" && gameObject.name == "TeethLaser")
            {
                TeethBracesController.teethLaserSFX.Play();
                if (col.gameObject.tag == "GreenDotTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 4f);
                    {
                        col.enabled = false;
                        TeethBracesController.taskFillbar.fillAmount += 0.25f;
                        TeethBracesController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
                            TeethBracesController.TaskDone();
                            TeethBracesController.teethLaserSFX.Stop();
                        }

                    }
                }
            }


        }
        #endregion

        #region Teeth Gums

        if (TeethGumsController)
        {
            if (col.gameObject.name == "EmptyTray" && gameObject.name == "ToffeeAnim")
            {
                gameObject.SetActive(false);
                TeethGumsController.ObjectEnableInTray(0);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "LemonAnim")
            {
                gameObject.SetActive(false);
                TeethGumsController.ObjectEnableInTray(1);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "FishAnim")
            {
                gameObject.SetActive(false);
                TeethGumsController.ObjectEnableInTray(2);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "StrawberryAnim")
            {
                gameObject.SetActive(false);
                TeethGumsController.ObjectEnableInTray(3);
            }
            else if (col.gameObject.name == "EmptyTray" && gameObject.name == "BubbleAnim")
            {
                gameObject.SetActive(false);
                TeethGumsController.ObjectEnableInTray(4);
            }
            else if (col.gameObject.name == "Toffee" && gameObject.name == "Clipper")
            {
                gameObject.SetActive(false);
                TeethGumsController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethGumsController.TaskDone();
            }
            else if (col.gameObject.name == "Lemon" && gameObject.name == "Clipper")
            {
                gameObject.SetActive(false);
                TeethGumsController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethGumsController.TaskDone();
            }
            else if (col.gameObject.name == "FishBone" && gameObject.name == "Clipper")
            {
                gameObject.SetActive(false);
                TeethGumsController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.gameObject.transform.GetComponent<PolygonCollider2D>().enabled = false;
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethGumsController.TaskDone();
            }
            else if (col.gameObject.name == "Strawberry" && gameObject.name == "Clipper")
            {
                gameObject.SetActive(false);
                TeethGumsController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.enabled = false;
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                TeethGumsController.TaskDone();
            }
            else if (col.gameObject.name == "BubbleGum" && gameObject.name == "Clipper")
            {
                gameObject.SetActive(false);
                TeethGumsController.emptyTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                col.transform.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 1f);
                col.transform.GetChild(0).gameObject.SetActive(true);
                if (downParent)
                {
                    col.transform.GetChild(0).parent = downParent;
                }
                col.enabled = false;
                TeethGumsController.TaskDone();
            }

            else if ((col.gameObject.tag == "SingleTeethTag" || col.gameObject.tag == "EmptyTag") && gameObject.name == "Brush")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethGumsController.burshSFX.Play();
                if (col.gameObject.tag == "SingleTeethTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    if (col.GetComponent<Image>().color == new Color(1, 1, 1, 1))
                    {
                        col.enabled = false;
                        col.GetComponent<PolygonCollider2D>().enabled = false;
                        TeethGumsController.taskFillbar.fillAmount += 0.0833f;
                        TeethGumsController.TaskFillBar();
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
                if (isAlphaIncreased == true && IsAlphaReduced == true)
                {
                    TeethGumsController.burshSFX.Stop();
                    TeethGumsController.TaskDone();
                }
            }

            else if (col.gameObject.tag == "DurtTag" && gameObject.name == "Excavator")
            {
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                TeethGumsController.excavatorSFX.Play();
                if (col.gameObject.tag == "DurtTag")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        col.transform.GetChild(0).gameObject.SetActive(false);
                        col.enabled = false;
                        TeethGumsController.taskFillbar.fillAmount += 0.25f;
                        TeethGumsController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
                            TeethGumsController.TaskDone();
                            TeethGumsController.excavatorSFX.Stop();
                        }

                    }
                }
            }
            else if (col.gameObject.name == "Gum" && gameObject.name == "GumsScope")
            {
                col.transform.GetComponent<Image>().enabled = true;
                col.transform.GetChild(0).gameObject.SetActive(true);
                TeethGumsController.TaskDone();
            }
            else if((col.gameObject.name == "GumVisible" || col.gameObject.name == "InjuredPart")&& gameObject.name == "TeethLaser")
            {
                if (col.gameObject.name == "GumVisible")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        
                        IsAlphaReduced = true;
                    }
                }
                if (col.gameObject.name == "InjuredPart")
                {
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.3f);
                    col.transform.GetChild(0).localScale += new Vector3(0.17f , 0.17f, 0.17f);
                    if ((col.GetComponent<Image>().color.a >= 1) && (col.transform.GetChild(0).localScale.x >= 0.8 && col.transform.GetChild(0).localScale.y >= 0.8 && col.transform.GetChild(0).localScale.z >= 0.8))
                    {
                        isAlphaIncreased = true;
                        col.enabled = false;
                    }
                }
                if(IsAlphaReduced == true && isAlphaIncreased == true)
                {
                    TeethGumsController.TaskDone();
                }

            }
            else if((col.gameObject.name == "DirtyDrop" || col.gameObject.name == "CottonTray") && gameObject.name == "Cotton")
            {
                TeethGumsController.cottonTray.transform.DOLocalMove(new Vector3(-540f, -344f, 0), 1f);
                if (col.gameObject.name == "DirtyDrop")
                {
                    col.enabled = false;
                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.25f);
                    if(col.GetComponent<Image>().color.a <= 0)
                    {
                        IsAlphaReduced = true;
                    }
                }
                if (gameObject.name == "Cotton")
                {
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1, gameObject.GetComponent<Image>().color.a - 1f);
                    gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, gameObject.transform.GetChild(0).GetComponent<Image>().color.a + 1f);
                }
                if (col.gameObject.name == "CottonTray")
                {
                    gameObject.SetActive(false);
                    TeethGumsController.CottonImageOnTray();
                }
            }
            else if (col.gameObject.name == "InjuredPart" && gameObject.name == "MouthSpray")
            {

                TeethGumsController.mouthSprayAnim.SetActive(true);
                gameObject.SetActive(false);

            }
            else if (col.gameObject.name == "InjuredPart" && gameObject.name == "MouthSprayAnim")
            {
                if (col.gameObject.name == "InjuredPart")
                {

                    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.2f);
                    TeethGumsController.taskFillbar.fillAmount += 0.2f;
                    TeethGumsController.TaskFillBar();
                    if (col.GetComponent<Image>().color.a <= 0)
                    {
                        IsAlphaReduced = true;
                        col.enabled = false;

                    }
                }
                if (IsAlphaReduced == true)
                {
                    TeethGumsController.TaskDone();
                    gameObject.SetActive(false);
                }

            }
            else if (col.gameObject.tag == "GreenDotTag" && gameObject.name == "LaserLight")
            {
                TeethGumsController.teethLaserSFX.Play();
                if (col.gameObject.tag == "GreenDotTag")
                {
                    col.transform.DOScale(new Vector3(0f, 0f, 0f), 3f);
                    {
                        col.enabled = false;
                        TeethGumsController.taskFillbar.fillAmount += 0.25f;
                        TeethGumsController.TaskFillBar();
                        IndexOne++;
                        if (IndexOne == 4)
                        {
                            IndexOne = 0;
                            TeethGumsController.TaskDone();
                            TeethGumsController.teethLaserSFX.Stop();
                        }

                    }
                }
            }
        }
        #endregion

        #region Germs Killing

        if (col.gameObject.name == "PinkGerm" && gameObject.name == "PinkGermsKiller")
        {
            if (col.gameObject.name == "PinkGerm")
            {
                col.enabled = false;
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    GermKillingController.taskFillbar.fillAmount += 0.125f;
                    GermKillingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        GermKillingController.isPinkGermOff = true;
                        IndexOne = 0;
                        GermsTaskDone();
                    }

                }
            }
        }
        else if (col.gameObject.name == "BlueGerm" && gameObject.name == "BlueGermsKiller")
        {
            if (col.gameObject.name == "BlueGerm")
            {
                col.enabled = false;
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    GermKillingController.taskFillbar.fillAmount += 0.125f;
                    GermKillingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        GermKillingController.isBlueGermOff = true;
                        IndexOne = 0;
                        GermsTaskDone();
                    }

                }
            }
        }
        else if (col.gameObject.name == "GreenGerm" && gameObject.name == "GreenGermsKiller")
        {
            if (col.gameObject.name == "GreenGerm")
            {
                col.enabled = false;
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    GermKillingController.taskFillbar.fillAmount += 0.125f;
                    GermKillingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        GermKillingController.isGreenGermOff = true;
                        IndexOne = 0;
                        GermsTaskDone();
                    }

                }
            }
        }
        else if (col.gameObject.name == "OrangeGerm" && gameObject.name == "OrangeGermsKiller")
        {
            if (col.gameObject.name == "OrangeGerm")
            {
                col.enabled = false;
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    
                    GermKillingController.taskFillbar.fillAmount += 0.125f;
                    GermKillingController.TaskFillBar();
                    IndexOne++;
                    if (IndexOne == 2)
                    {
                        IndexOne = 0;
                        GermKillingController.isOrangeGermOff = true;
                        GermsTaskDone();
                    }

                }
            }
        }

        #endregion
    }

    public void GermsTaskDone()
    {

        if (TeethCleaningController)
        {
            if (GermKillingController.isPinkGermOff == true && GermKillingController.isBlueGermOff == true && GermKillingController.isGreenGermOff == true && GermKillingController.isOrangeGermOff == true)
            {
                TeethCleaningController.TaskDone();
            }
        }        
        else if (TeethReparingController)
        {
            if (GermKillingController.isPinkGermOff == true && GermKillingController.isBlueGermOff == true && GermKillingController.isGreenGermOff == true && GermKillingController.isOrangeGermOff == true)
            {  
                TeethReparingController.TaskDone();
            }
        }       
        else if (TeethBracesController)
        {
            if (GermKillingController.isPinkGermOff == true && GermKillingController.isBlueGermOff == true && GermKillingController.isGreenGermOff == true && GermKillingController.isOrangeGermOff == true)
            {
                TeethBracesController.TaskDone();
            }
        }       
        else if (TeethGumsController)
        {
            if (GermKillingController.isPinkGermOff == true && GermKillingController.isBlueGermOff == true && GermKillingController.isGreenGermOff == true && GermKillingController.isOrangeGermOff == true)
            {
                TeethGumsController.TaskDone();
            }
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
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (TeethReparingController)
        {
            if ((col.gameObject.tag == "BlackDamagedTeethTag" || col.gameObject.tag == "YellowDamagedTeethTag") && gameObject.name == "SecondDrill")
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else if (col.gameObject.name == "WaterLayer" && gameObject.name == "WaterPipe")
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        inTrigger = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        inTrigger = true;
    }
    private void OnEnable()
    {
        didTrigger = 0;
    }
    private void OnDisable()
    {
        if (isPosAssigned) transform.localPosition = InitialPosition;
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
    }

}
