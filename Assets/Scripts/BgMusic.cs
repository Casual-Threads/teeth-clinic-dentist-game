using UnityEngine;
using System.Collections;

public class BgMusic : MonoBehaviour {

    public GameObject SoundsObject;
    private static BgMusic instance = null;
    public static BgMusic Instance
    {
        get { return instance; }
    }
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }		
}
