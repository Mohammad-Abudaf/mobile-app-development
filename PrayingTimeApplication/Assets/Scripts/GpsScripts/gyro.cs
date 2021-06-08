using UnityEngine;

public class gyro : MonoBehaviour {
    private bool _gyroEnabled;
    private Gyroscope _gyr;
    public static Quaternion Attitude;

    private void Start () {
        _gyroEnabled = EnableGyro();
	}
    private void Update () {
        Attitude = _gyr.attitude;
	}
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _gyr = Input.gyro;
            _gyr.enabled = true;
            return true;
        }
        return false;
    }
}
