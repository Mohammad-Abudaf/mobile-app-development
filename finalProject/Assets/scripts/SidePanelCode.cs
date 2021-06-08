using UnityEngine;

public class SidePanelCode : MonoBehaviour
{
    public void GOToLogInWebSite()
    {
        Application.OpenURL("LogInWebSite.com");
    }
    public void GOToSignInWebSite()
    {
        Application.OpenURL("SignInWeb.com");
    }
    public void GOToConnectUsWebSite()
    {
        Application.OpenURL("https://www.facebook.com/mhmd.mahmoud.1203");
    }

    public void AboutUsURL()
    {
        Application.OpenURL("https://github.com/Mohammad-Abudaf");
    }
    
}
