using System;
using UnityEngine;
using Unity.Notifications.Android;

public class Notify : MonoBehaviour
{
    
    private string[] arrayOfTime; 
    private int hours, min;
    private void Update()
    {
        arrayOfTime = MainCode._medicalInformation[0, 1].Split(':');
        hours = int.Parse(arrayOfTime[0]);
        min = int.Parse(arrayOfTime[1]);

        var totalMin = hours * 60 + min; 
        // remove notifications that that have been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        // creat the android notifications channel to send the massages through
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Reminder notifications",
            EnableVibration = true,
            EnableLights = true,
            
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //creat a notifications that is going to be sent!
        var notification = new AndroidNotification
        {
            Title = "medication reminder", 
            Text = "Don't forget to take your medicine",
            FireTime = DateTime.Now.AddMinutes(totalMin),
        };
        
        // send the notifications
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        // if the script is run and the massage is already scheduled, cancel it and re-schedule it. 
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) != NotificationStatus.Scheduled) return;
        AndroidNotificationCenter.CancelAllNotifications();
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
