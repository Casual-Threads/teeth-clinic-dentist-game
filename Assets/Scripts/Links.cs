using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Links : MonoBehaviour
{
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/search?q=pub%3A%20S%20%26%20U%20Games&c=apps&hl=en&gl=PK");
    }
    public void RateUs()
    {
        // Amazon Link
        //Application.OpenURL("http://www.amazon.com/gp/mas/dl/android?p=com.amazon.shotgun.sounds.gunsimulator");
        // Play Store Link
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.casualthreads.shotgun.sounds.gunsimulator");
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1FifyUdyGCnEXalDgDaGcYahm7rHYyQSZfq43Uw1XqVw/edit");
    }
}
