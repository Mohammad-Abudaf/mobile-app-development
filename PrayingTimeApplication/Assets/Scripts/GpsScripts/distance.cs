using UnityEngine;

public class distance : MonoBehaviour {

    private float _cLat, _cLong, _tLat, _tLong;
    public static float Distance;

	// Use this for initialization
    private void Start () {
        _cLat = GPS.Latitude;
        _cLong = GPS.Longitude;
        _tLat = generateTargetLocation.newLat;
        _tLong = generateTargetLocation.newLong;
	}
	// Update is called once per frame
    private void Update () {

        _cLat = GPS.Latitude;
        _cLong = GPS.Longitude;
        _tLat = generateTargetLocation.newLat;
        _tLong = generateTargetLocation.newLong;
        Distance = DistanceInMetersBetweenEarthCoordinates(_cLat,_cLong,_tLat,_tLong);
    }

    private static float DegreesToRadians(float degrees)
    {
        return degrees * Mathf.PI / 180;
    }

    private static float DistanceInMetersBetweenEarthCoordinates(float cLat, float cLon, float tLat, float tLon)
    {
        const int earthRadiusKm = 6371;

        var dLat = DegreesToRadians(tLat - cLat);
        var dLon = DegreesToRadians(tLon - cLon);

        cLat = DegreesToRadians(cLat);
        tLat = DegreesToRadians(tLat);

        var a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2) * Mathf.Cos(cLat) * Mathf.Cos(tLat);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return earthRadiusKm * c / 1000;
    }
}

