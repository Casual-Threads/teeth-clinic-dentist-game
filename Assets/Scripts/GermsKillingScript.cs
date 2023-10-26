using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermsKillingScript : MonoBehaviour
{
    [Header("Image Arrays")]
    public Image[] starImages;
    [Header("Sprites")]
    public Sprite goldStarSprite;
    public Sprite grayStarSprite;
    public Image taskFillbar;
    public GameObject firstPinkGerm;
    public GameObject secondPinkGerm, firstBlueGerm, secondBlueGerm, firstGreenGerm, secondGreenGerm, firstOrangeGerm, secondOrangeGerm;
    public AudioSource teethLaserSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskDone(int index)
    {
        if(index == 0)
        {
            firstPinkGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 1)
        {
            secondPinkGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 2)
        {
            firstBlueGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 3)
        {
            secondBlueGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 4)
        {
            firstGreenGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 5)
        {
            secondGreenGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 6)
        {
            firstOrangeGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(index == 7)
        {
            secondOrangeGerm.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
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
}
