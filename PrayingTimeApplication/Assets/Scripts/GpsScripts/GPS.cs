using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GPS : MonoBehaviour {

    public static float Longitude;
    public static float Latitude;


    public float r;
    public float x, y, z;

    private static GPS _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.location.isEnabledByUser)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            ConvertCoordinatesToXYZ();
        }
    }

    private IEnumerator Start()
    {

        Longitude = 0;
        Latitude = 0;

        r = 6371;
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(5f, 5f);

        // Wait until service initializes
        var maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        // Stop service if there is no need to query location updates continuously
        // Input.location.Stop();
    }
    private void ConvertCoordinatesToXYZ()
    {
        x = r * Mathf.Cos(Latitude) * Mathf.Cos(Longitude);
        y = r * Mathf.Cos(Latitude) * Mathf.Sin(Longitude);
        z = r * Mathf.Sin(Latitude);
    }
}
