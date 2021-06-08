using UnityEngine;
public class compassRotation : MonoBehaviour {
    private GameObject compass;
    private float bearing;
    Quaternion attitude;
    void Start () {
        compass = GameObject.Find("navArrow");
    }
	
	// Update is called once per frame
	void Update () {
        bearing = angleFromCoordinate(GPS.latitude, GPS.longitude,generateTargetLocation.newLat, generateTargetLocation.newLong);
        attitude = gyro.attitude;
        //attitude.Set(0, 0, 0, gyro.attitude[3]);//gyro.attitude[3]
        attitude[0] = 0;
        attitude[1] = 0;
        attitude[3] *= -1;
        compass.transform.rotation = attitude;
        compass.transform.rotation *= Quaternion.Slerp(compass.transform.rotation, Quaternion.Euler(0, 0, Input.compass.magneticHeading + bearing), 1f);
    }
    private float angleFromCoordinate(float lat1, float long1, float lat2, float long2)
    {
        lat1 *= Mathf.Deg2Rad;
        lat2 *= Mathf.Deg2Rad;
        long1 *= Mathf.Deg2Rad;
        long2 *= Mathf.Deg2Rad;

        float dLon = (long2 - long1);
        float y = Mathf.Sin(dLon) * Mathf.Cos(lat2);
        float x = (Mathf.Cos(lat1) * Mathf.Sin(lat2)) - (Mathf.Sin(lat1) * Mathf.Cos(lat2) * Mathf.Cos(dLon));
        float brng = Mathf.Atan2(y, x);
        brng = Mathf.Rad2Deg * brng;
        brng = (brng + 360) % 360;
        brng = 360 - brng;
        return brng;
    }
}
