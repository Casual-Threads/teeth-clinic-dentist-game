using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOff : MonoBehaviour
{
    public static ObjectOff Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public IEnumerator EnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        print("enter in Ien");
        yield return new WaitForSeconds(_Delay);
        print("After Delay");
        activateObject.SetActive(isTrue);
    }
}
