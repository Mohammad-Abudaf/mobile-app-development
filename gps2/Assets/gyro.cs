using UnityEngine;

public class gyro : MonoBehaviour {
    private bool gyroEnabled;
    private Gyroscope gyr;
    public static Quaternion attitude;
    // Use this for initialization
    void Start () {

        gyroEnabled = EnableGyro();
	}
	// Update is called once per frame
	void Update () {
        attitude = gyr.attitude;
	}
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyr = Input.gyro;
            gyr.enabled = true;
            return true;
        }
        return false;
    }
}
