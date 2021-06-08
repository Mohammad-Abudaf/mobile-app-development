using UnityEngine;
using UnityEngine.UI;

public class textMang : MonoBehaviour {

    public Text lat, lon, tLat, tLon, attitude,dist;
    
    // Update is called once per frame
    private void Update()
    {
        generateTargetLocation.targetLocation();
        lat.text = GPS.Latitude.ToString();
        lon.text = GPS.Longitude.ToString();
        tLon.text = generateTargetLocation.newLong.ToString();
        tLat.text = generateTargetLocation.newLat.ToString();
        attitude.text =  gyro.Attitude[2].ToString();
        dist.text =  distance.Distance.ToString();
    }
}
