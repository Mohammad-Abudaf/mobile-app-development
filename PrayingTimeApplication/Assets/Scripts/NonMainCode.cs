using UnityEngine;
using UnityEngine.UI; 
using System;
public class NonMainCode : MonoBehaviour {
    public Text minHourClock;
    public Text secondsClock;
    public Text currentDay;
    public Text currentDate;
    private int _currentMin, _currentHour, _currentSec;
    
    private DateTime date = DateTime.Now;
    private string day; 
    
    // Update is called once per frame
    private void Update() {
        _currentMin = DateTime.Now.Minute;
        _currentHour = DateTime.Now.Hour;
        _currentSec = DateTime.Now.Second; 
        secondsClock.text = $"{_currentSec:00}";
        minHourClock.text = $"{_currentHour:00}:{_currentMin:00}:";
        currentDate.text = $"{date.Date}";
        // ReSharper disable once HeapView.BoxingAllocation
        day = date.DayOfWeek.ToString();
        currentDay.text = day;
    }
}