using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class DragMagnifyingGlass : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 dragOffset;
    GermsKillingScript GermKillingController;
    

    void Start()
    {
        GermKillingController = FindObjectOfType<GermsKillingScript>();

    }
    private void OnMouseDown()
    {
        dragOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition - dragOffset;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstPinkGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondPinkGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstBlueGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondBlueGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstGreenGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondGreenGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstOrangeGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondOrangeGerm")
        {
            col.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstPinkGerm")
        {
            GermKillingController.TaskDone(0);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondPinkGerm")
        {
            GermKillingController.TaskDone(1);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstBlueGerm")
        {
            GermKillingController.TaskDone(2);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondBlueGerm")
        {
            GermKillingController.TaskDone(3);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstGreenGerm")
        {
            GermKillingController.TaskDone(4);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondGreenGerm")
        {
            GermKillingController.TaskDone(5);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "FirstOrangeGerm")
        {
            GermKillingController.TaskDone(6);
        }
        else if (gameObject.name == "MagnifyingGlass" && col.gameObject.name == "SecondOrangeGerm")
        {
            GermKillingController.TaskDone(7);
        }
    }
}
