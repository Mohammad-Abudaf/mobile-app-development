using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesMovementAndWebLinks : MonoBehaviour
{
    public void ONClickConnectUsBtn()
    {
        Application.OpenURL("https://www.facebook.com/mohammed.abudaf");
        print("opened URL");
    }
    public void ONClickAboutUsBtn()
    {
        Application.OpenURL("https://github.com/Mohammad-Abudaf");
        print("opened URL");

    }
    public void ONClickDonateUsBtn()
    {
        Application.OpenURL("https://www.patreon.com/");
        print("opened URL");

    }
    public void MoveToMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
        print("opened MainScreen");

    }
    public void MoveToAlarmScreen()
    {
        SceneManager.LoadScene("AlarmScreen");
        print("opened AlarmScreen"); 
    }
    public void MoveToCompassScreen()
    {
        SceneManager.LoadScene("CompassScreen");
        print("opened Compass Screen");
    }
    public void MoveToSettingScreen()
    {
        SceneManager.LoadScene("SettingsScreen");
        print("Opened SettingsScreen"); 
    }
}