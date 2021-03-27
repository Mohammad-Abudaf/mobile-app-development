using System;
using UnityEngine;
using UnityEngine.UI;

public class MainCode : MonoBehaviour{
    public Image SecondsHand;
    public Image MinHand;
    public Image HoursHand;

    public Text outputTimeAsDigital;

    private int CurrentSeconds;
    private int CurrentMin;
    private int CurrentHour;

    private void Update(){
        CurrentSeconds = DateTime.Now.Second;
        CurrentMin = DateTime.Now.Minute;
        CurrentHour = DateTime.Now.Hour;

        SecondsHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -6 * CurrentSeconds));
        MinHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -6 * CurrentMin));
        HoursHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -30 * CurrentHour));

        outputTimeAsDigital.text = $"{CurrentHour:00}:{CurrentMin:00}:{CurrentSeconds:00}";
    }

    public void ExitBtn(){
        print("the application will Quit");
        Application.Quit();
    }
}

