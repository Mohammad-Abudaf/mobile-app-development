using UnityEngine;
using UnityEngine.UI;
public class textMang : MonoBehaviour {
    public Text lat, lon, tLat, tLon, attitude,dist;
    // Update is called once per frame
    private void Update()
    {
        lat.text = "LAT : " + GPS.latitude.ToString();
        lon.text = "LON : " + GPS.longitude.ToString();
        tLon.text = "TLON : " + generateTargetLocation.newLong.ToString();
        tLat.text = "TLAT : " + generateTargetLocation.newLat.ToString();
        attitude.text = "Attitude: " + gyro.attitude[2].ToString();
        dist.text = "Distance: " + distance.Distance.ToString();
    }
}
