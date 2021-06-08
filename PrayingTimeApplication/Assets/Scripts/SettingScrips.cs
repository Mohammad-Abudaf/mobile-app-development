using UnityEngine;

public class SettingScrips : MonoBehaviour
{
    public void GOMyAccount()
    {
        Application.OpenURL("https://github.com/Mohammad-Abudaf");
    }

    public void SignUp()
    {
        Application.OpenURL("https://github.com/login");
    }

    public void SignOut()
    {
        //this website leads to ProjectWebSite. 
        Application.OpenURL("//");
    }

    public void MemberShip()
    {
        Application.OpenURL("https://www.patreon.com/");
    }

    public void GotoHelpWebSite()
    {
        Application.OpenURL("https://www.google.com/search?q=help&oq=help+&aqs=chrome..69i57j0i10j0l3j69i60j69i61l2.4191j0j7&sourceid=chrome&ie=UTF-8");
    }
}
