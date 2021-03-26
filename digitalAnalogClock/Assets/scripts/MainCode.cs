using System;
using UnityEngine;
using UnityEngine.UI;
public class MainCode : MonoBehaviour{
    public Image secondsHand;
    public Image minHand;
    public Image hoursHand;

    public Text outputTimeAsDigital;

    private float _currentSec;
    private float _currentMin;
    private float _currentHour;
    private float _totalSecond;
    private void Update(){
        _totalSecond = Time.time;
        _currentSec = _totalSecond % 60;
        _currentMin = (_totalSecond / 60) % 60;
        _currentHour = (_totalSecond / 3600) % 24;

        secondsHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -6 * (float)Math.Truncate(_currentSec)));
        minHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -6 * _currentMin));
        hoursHand.gameObject.transform.rotation =
            Quaternion.Euler(new Vector3(0, 0, -6 * _currentHour));

        outputTimeAsDigital.text = $"{(int)_currentHour:00}:{(int)_currentMin:00}:{(int)_currentSec:00}";
    }

    public void ExitBtn(){
        print("the application will quit now");
        Application.Quit();
    }
}
