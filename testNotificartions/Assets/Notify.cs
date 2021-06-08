using UnityEngine;
using Unity.Notifications.Android;

public class Notify : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
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
            FireTime = System.DateTime.Now.AddSeconds(30),
        };
        
        // send the notifications
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        // if the script is run and the massage is already scheduled, cancel it and re-schedule it. 
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) != NotificationStatus.Scheduled) return;
        AndroidNotificationCenter.CancelAllNotifications();
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
