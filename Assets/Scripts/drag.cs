using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.Events;

public class drag : MonoBehaviour
{
    //public RuntimeAnimatorController foamAnimatorReverse;

    public bool tap = false;
    Vector3 initialPosition;
    public Vector3 CameraSet;
    private int didTrigger;
    public int totalTriggers;
    bool dustRemove = false;
    bool bubblesOff = false;
    bool mehndiRemove = false;
    bool dropOn = false;
    //bool allDone = true;
    public UnityEvent MouseDown;
    public UnityEvent MouseUp;
    //Spa SpaController;
    //Ubtan UbtanController;
    private int index = 0;
    
    #region Start and Update
    void Start()
    {
        initialPosition = gameObject.transform.localPosition;
        tap = false;
        //SpaController = FindObjectOfType<Spa>();
        //UbtanController = FindObjectOfType<Ubtan>();
    }

    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                Vector3 loc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                loc.z = 0f;
                if (gameObject.GetComponent<BoxCollider2D>().bounds.Contains(loc))
                {
                    CameraSet = gameObject.transform.position - loc;
                }
            }

            if (tap)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 loc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    loc.z = 0f;
                    tap = true;
                    gameObject.transform.position = loc + CameraSet;
                    
                }

                if (Input.GetMouseButtonUp(0))
                {

                    gameObject.transform.DOLocalMove(initialPosition, 1f).OnComplete(delegate
                    {
                        tap = false;
                    });
                }
            }
    }
    void OnMouseDown()
    {
        //gameObject.GetComponent<ScalePingPong>().enabled = false;
        MouseDown.Invoke();
        //if (gameObject.name == "SerumDropper")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f));
        //}
        //if (gameObject.name == "FacewashBottel")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f));
        //}
        //if (gameObject.name == "WaterJar")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 96f));
        //}
    }
    void OnMouseUp()
    {
        //gameObject.GetComponent<ScalePingPong>().enabled = true;
        MouseUp.Invoke();
        //if (SpaController)
        //{
        //    SpaController.eyesImage.sprite = SpaController.closeEyeSprites[5];
        //}
        //if (UbtanController)
        //{
        //    UbtanController.playerElements.eyesImage.sprite = UbtanController.closeEyeSprites[5];
        //}
        //if (gameObject.name == "SerumDropper")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 40f));
        //}
        //if (gameObject.name == "FacewashBottel")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -26f));
        //}
        //if (gameObject.name == "WaterJar")
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 51.5f));
        //}
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.tag == "DropTag" && gameObject.name == "WaterJar")
        //{
        //    col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.3f);
        //    if (col.GetComponent<Image>().color.a >= 1)
        //    {
        //        col.enabled = false;
        //        index++;
        //        if (index == 10)
        //        {
        //            print("123456789");
        //            dropOn = true;
        //        }

        //    }

        //    //if (dropOn == true)
        //    //{
        //    //    Invoke("ubtanTaskDone", 1f);
        //    //    UbtanController.cleaner.SetActive(false);
        //    //}
        //}

        //if (UbtanController)
        //{
        //    UbtanController.playerElements.eyesImage.sprite = UbtanController.closeEyeSprites[SaveData.Instance.selectedCharacter];
        //}
        if (col.gameObject.name == "Toffee" && gameObject.name == "Clipper")
        {
            print("yes");
            //SpaController.ohNoSFX.Play();
            //Invoke("ObjectOff", 0.1f);
            //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[SaveData.Instance.selectedCharacter];
            //SpaController.LipsImage.sprite = SpaController.lipsSprites[0];
            //if (col.gameObject.transform.childCount > 0)
            //col.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            //col.gameObject.transform.GetChild(1).GetComponent<Animator>().Play(0);
            //col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //didTrigger++;
            //col.enabled = false;
            //if(didTrigger <= 2)
            //{
            //    Invoke("ObjectActivation", 1f);
            //}
            //else if (didTrigger >= totalTriggers)
            //{
            //    Invoke("doneFunctionInvoke", 1.2f);
            //}
        }
        else if (col.gameObject.name == "Worm" && gameObject.name == "WormPoper")
        {
            //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[SaveData.Instance.selectedCharacter];
            //SpaController.ohNoSFX.Play();
            Invoke("ObjectOff", 0.1f);
            //SpaController.LipsImage.sprite = SpaController.lipsSprites[0];
            if (col.gameObject.transform.childCount > 0)
            col.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            col.gameObject.transform.GetChild(1).GetComponent<Animator>().Play(0);
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            didTrigger++;
            col.enabled = false;
            if (didTrigger <= 2)
            {
                Invoke("ObjectActivation", 1.5f);
            }
            else if (didTrigger >= totalTriggers)
            {
                Invoke("doneFunctionInvoke", 1.5f);
            }
        }
        else if (col.gameObject.name == "Serum" && gameObject.name == "SerumDropper")
        {
            //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[SaveData.Instance.selectedCharacter];
            Invoke("ObjectOff", 0.1f);
            if (col.gameObject.transform.childCount > 0)
            col.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            col.gameObject.transform.GetChild(1).GetComponent<Animator>().Play(0);
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            didTrigger++;
            col.enabled = false;
            if (didTrigger <= 2)
            {
                Invoke("ObjectActivation", 1f);
            }
            else if (didTrigger >= totalTriggers)
            {
                Invoke("doneFunctionInvoke", 1.5f);
            }
        }
        else if (col.gameObject.name == "Facewash" && gameObject.name == "FacewashBottel")
        {
            Invoke("ObjectOff", 0.1f);
            //SpaController.facewashSFX.Play();
            if (col.gameObject.transform.childCount > 0)
                col.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            col.gameObject.transform.GetChild(1).GetComponent<Animator>().Play(0);
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            didTrigger++;
            col.enabled = false;
            if (didTrigger <= 1)
            {
                Invoke("ObjectActivation", 2.4f);
            }
            else if (didTrigger >= totalTriggers)
            {
                Invoke("doneFunctionInvoke", 2.4f);
            }
        }
        else if ((col.gameObject.name == "Foam" || col.gameObject.name == "Dust") && gameObject.name == "Remover")
        {
            //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[SaveData.Instance.selectedCharacter];
            //SpaController.removerSFX.Play();
            if (col.gameObject.name == "Foam")
            {
                col.gameObject.transform.GetComponent<Animator>().enabled = true;
                //col.gameObject.transform.GetComponent<Animator>().runtimeAnimatorController = foamAnimatorReverse;
                col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                bubblesOff = true;
            }

            if (col.gameObject.name == "Dust")
            {
                col.transform.GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetComponent<Image>().color.a - 0.4f);
                if(col.transform.GetComponent<Image>().color.a <= 0)
                {
                    dustRemove = true;
                }
            }
            if(bubblesOff ==  true && dustRemove == true)
            {
                //SpaController.LipsImage.sprite = SpaController.lipsSprites[2];
                Invoke("doneFunctionInvoke", 1f);
            }

        }
        else if(col.gameObject.name == "YellowLayer" && gameObject.name == "Leaf")
        {
            //UbtanController.removerSFX.Play();
            //UbtanController.Indication.SetActive(false);
            col.transform.GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetComponent<Image>().color.a + 0.2f);
            //if(col.transform.GetComponent<Image>().color.a >= 1 && UbtanController.mehndiPot.gameObject.activeSelf)
            {
                //UbtanController.mehndiPot.gameObject.SetActive(false);
                Invoke("ubtanTaskDone", 1f);
            }
        }
        if ((col.gameObject.name == "YellowLayer" || col.gameObject.tag == "DropTag") && gameObject.name == "WaterJar")
        {
            //UbtanController.Indication.SetActive(false);
            if (col.gameObject.name == "YellowLayer")
            {
                col.transform.GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetComponent<Image>().color.a - 0.2f);
                if (col.transform.GetComponent<Image>().color.a <= 0.5)
                {
                    mehndiRemove = true;
                }
            }
            if (col.gameObject.tag == "DropTag")
            {
                col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a + 0.3f);
                if(col.GetComponent<Image>().color.a >= 1)
                {
                    col.enabled = false;
                    index++;
                    if(index == 10)
                    {
                        index = 0;
                        dropOn = true;
                    }
                    
                }
            }

            if (mehndiRemove == true && dropOn == true)
            {
                Invoke("ubtanTaskDone", 1f);
                //UbtanController.waterJar.gameObject.SetActive(false);
            }
        }
        else if ((col.gameObject.name == "YellowLayer" || col.gameObject.tag == "DropTag") && gameObject.name == "Cleaner")
        {
            //UbtanController.Indication.SetActive(false);
            //UbtanController.removerSFX.Play();
            if (col.gameObject.name == "YellowLayer")
            {
                col.transform.GetComponent<Image>().color = new Color(1, 1, 1, col.transform.GetComponent<Image>().color.a - 0.2f);
                if (col.transform.GetComponent<Image>().color.a <= 0)
                {
                    mehndiRemove = true;
                }
            }
            if (col.gameObject.tag == "DropTag")
            {
                col.GetComponent<Image>().color = new Color(1, 1, 1, col.GetComponent<Image>().color.a - 0.3f);
                if (col.GetComponent<Image>().color.a <= 0)
                {
                    col.enabled = false;
                    index++;
                    if (index == 10)
                    {
                        index = 0;
                        dropOn = true;

                    }

                }
            }

            if (dropOn == true)
            {
                Invoke("ubtanTaskDone", 1f);
                //UbtanController.cleaner.gameObject.SetActive(false);
            }
        }

    }
    public void ObjectActivation()
    {
        //if(SpaController.facewashSFX) SpaController.facewashSFX.Stop();
        //SpaController.LipsImage.sprite = SpaController.lipsSprites[1];
        //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[5];
        gameObject.SetActive(true);
    }
    public void ObjectOff()
    {
        gameObject.SetActive(false);
    }
    public void doneFunctionInvoke()
    {
        //SpaController.TaskDone();
        //SpaController.LipsImage.sprite = SpaController.lipsSprites[1];
        //if(gameObject.name == "Remover")
        //{
        //    SpaController.LipsImage.sprite = SpaController.lipsSprites[2];
        //}
        //if (SpaController.facewashSFX) SpaController.facewashSFX.Stop();
        //SpaController.eyesImage.sprite = SpaController.closeEyeSprites[5];
        //SpaController.taskParticle.Play();
        //if (SpaController.taskParticle) SpaController.taskParticle.transform.GetComponent<AudioSource>().Play();
        totalTriggers = 0;
    }
    public void ubtanTaskDone()
    {
        //UbtanController.TaskDone();
        //UbtanController.playerElements.eyesImage.sprite = UbtanController.closeEyeSprites[5];
        //UbtanController.taskParticle.Play();
        //if (UbtanController.taskParticle) UbtanController.taskParticle.transform.GetComponent<AudioSource>().Play();
    }

}
