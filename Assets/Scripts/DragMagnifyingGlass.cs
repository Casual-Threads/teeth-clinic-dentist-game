using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMagnifyingGlass : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 dragOffset;

    private void OnMouseDown()
    {
        dragOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition - dragOffset;
    }
}
