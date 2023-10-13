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
    public Transform /*startParent,*/ downParent;
    TeethCleaning TeethCleaningController;
    public UnityEvent MouseDown;
    public UnityEvent MouseUp;
    private int index = 0;
    //public UnityEvent OnEnterSoundPlay;
    //public UnityEvent OnExitSoundStop;
    // Start is called before the first frame update
    void Start()
    {
        TeethCleaningController = FindObjectOfType<TeethCleaning>();
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
        //OnEnterSoundPlay.Invoke();
        #region Teeth Cleaning
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
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
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
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
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
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
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
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
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
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
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
            //col.transform.GetChild(0).gameObject.SetActive(false);
            if (col.gameObject.tag == "GreenDotTag")
            {
                col.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
                {
                    col.enabled = false;
                    TeethCleaningController.taskFillbar.fillAmount += 0.0834f;
                    if (TeethCleaningController.taskFillbar.fillAmount >= 0.2f)
                    {
                        TeethCleaningController.starImages[0].sprite = TeethCleaningController.goldStarSprite;
                        if (TeethCleaningController.taskFillbar.fillAmount >= 0.5f)
                        {
                            TeethCleaningController.starImages[1].sprite = TeethCleaningController.goldStarSprite;
                            if (TeethCleaningController.taskFillbar.fillAmount >= 0.83f)
                            {
                                TeethCleaningController.starImages[2].sprite = TeethCleaningController.goldStarSprite;
                            }
                        }
                    }
                    index++;
                    if (index == 12)
                    {
                        index = 0;
                        TeethCleaningController.TaskDone();
                    }

                }
            }
        }
        #endregion
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        inTrigger = true;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //OnExitSoundStop.Invoke();
        TeethCleaningController.burshSFX.Stop();
        TeethCleaningController.excavatorSFX.Stop();
        TeethCleaningController.grinderSFX.Stop();
        TeethCleaningController.drillSFX.Stop();
        TeethCleaningController.chewPullerSFX.Stop();
        TeethCleaningController.teethLaserSFX.Stop();
        inTrigger = false;
        if (MouseDownIndicator) MouseDownIndicator.SetActive(true);
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
    //void GreenMask()
    //{
    //    FacialScript.Instance.greenMask.gameObject.SetActive(true);
    //    FacialScript.Instance.blueMask.gameObject.SetActive(false);
    //}
}
