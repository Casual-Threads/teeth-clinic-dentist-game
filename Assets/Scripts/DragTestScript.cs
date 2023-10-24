using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DragTestScript : MonoBehaviour
{
    public bool isCanvasObject;

    private Vector2 InitialPosition;
    //private Vector2 MousePosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    //private Vector3 curScreenPoint;
    //private Vector3 curPosition;
    //private float deltaX, deltaY;

    private bool isPosAssigned, restPos;



    #region Start Region
    // Start is called before the first frame update
    void Start()
    {

        restPos = true;

    }

    #endregion

    #region OnMouseDown
    void OnMouseDown()
    {
        //MouseDown.Invoke();
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
        //else
        //{
        //    deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.localPosition.x;
        //    deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.localPosition.y;
        //}
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
            print(transform.position);
        }
        //else
        //{
        //    MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    transform.localPosition = new Vector2(MousePosition.x - deltaX, MousePosition.y - deltaY);
        //}
    }
    #endregion

    #region OnMouseUp
    void OnMouseUp()
    {
        //MouseUp.Invoke();
        if (restPos)
        {
            transform.localPosition = InitialPosition;
        }
    }

    #endregion
}
