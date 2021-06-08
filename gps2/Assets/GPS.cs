using System.Collections;
using UnityEngine;
public class GPS : MonoBehaviour {
    public static float longitude;
    public static float latitude;
    public float R;
    public float x, y, z;
    public static GPS instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.location.isEnabledByUser)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            convertCoordinatesToXYZ();
        }
    }

    IEnumerator Start()
    {
        longitude = 0;
        latitude = 0;
        R = 6371;
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;
        // Start service before querying location
        Input.location.Start(5f, 5f);

        // Wait until service initializes
        int maxWait = 20;
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
    void convertCoordinatesToXYZ()
    {
        x = R * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        y = R * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        z = R * Mathf.Sin(latitude);
    }

}
