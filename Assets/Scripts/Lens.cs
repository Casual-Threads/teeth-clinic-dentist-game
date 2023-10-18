using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField]
    private Transform smallLayer, bigLayer;

    // Update is called once per frame
    void Update()
    {
        bigLayer.position = smallLayer.position * 2 - transform.position;
    }
}
