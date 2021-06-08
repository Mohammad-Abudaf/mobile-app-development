using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI; 
using PrayingTime;
using UnityEngine.Serialization;
public class MainCode : MonoBehaviour
{
    [CanBeNull] public Text minHourClock;
    [CanBeNull] public Text secondsClock;
    [CanBeNull] public Text fajerTimeOutput;
    [CanBeNull] public Text dohaTimeOutput;
    [CanBeNull] public Text dohorTimeOutput; 
    [CanBeNull] public Text aserTimeOutput; 
    [CanBeNull] public Text magrebTimeOutput; 
    [CanBeNull] public Text ishaTimeOutput;
    [CanBeNull] public Text currentDay;
    
    [FormerlySerializedAs("CurrentDate")] public Text currentDate; 
    private int _currentMin, _currentHour, _currentSec;
    private DateTime _date = DateTime.Now;
    private string _day; 
    private void Update()
    {
        _currentMin = DateTime.Now.Minute;
        _currentHour = DateTime.Now.Hour;
        _currentSec = DateTime.Now.Second;

        if (secondsClock is { }) secondsClock.text = $"{_currentSec:00}";
        if (minHourClock is { }) minHourClock.text = $"{_currentHour:00}:{_currentMin:00}:";

        currentDate.text = $"{_date.Date}";
        _day = _date.DayOfWeek.ToString();
        if (currentDay is { }) currentDay.text = _day;

        if (fajerTimeOutput is { })
            fajerTimeOutput.text = $"{PrayingTable.fajetTime[0]:00}:{PrayingTable.fajetTime[1]:00}";
        if (dohaTimeOutput is { }) 
            dohaTimeOutput.text = $"{PrayingTable.dohaTime[0]:00}:{PrayingTable.dohaTime[1]:00}";
        if (dohorTimeOutput is { })
            dohorTimeOutput.text = $"{PrayingTable.dohorTime[0]:00}:{PrayingTable.dohorTime[1]:00}";
        if (aserTimeOutput is { })
            aserTimeOutput.text = $"{PrayingTable.aserTime[0]:00}:{PrayingTable.aserTime[1] - 8:00}";
        if (magrebTimeOutput is { })
            magrebTimeOutput.text = $"{PrayingTable.magrebTime[0]:00}:{PrayingTable.magrebTime[1]:00}";
        if (ishaTimeOutput is { }) 
            ishaTimeOutput.text = $"{PrayingTable.ishaTime[0]:00}:{PrayingTable.ishaTime[1]:00}";
    }
}