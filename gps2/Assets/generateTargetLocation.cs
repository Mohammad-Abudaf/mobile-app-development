using UnityEngine;
using UnityEngine.UI;
public class generateTargetLocation : MonoBehaviour {
    private float w, t, currentLat, currentLong, x, y, radius, rand1, rand2;
    public Text lat, lon;
    public static float newLat, newLong;
    /*
    public static generateTargetLocation instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    */

    void Start () {
        radius = 6371f / 111000f;
        newLat = 0;
        newLong = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //targetLocation();
	}
    public void targetLocation()
    {
        currentLat = GPS.latitude;
        currentLong = GPS.longitude;
        //currentLat = 71.43153f;
        //currentLong = 33.97607f;

        rand1 = Random.Range(0.0f, 1.0f);
        rand2 = Random.Range(0.0f, 1.0f);

        w = radius * Mathf.Sqrt(rand1);
        t = 2 * Mathf.PI * rand2;
        x = w * Mathf.Cos(t);
        y = w * Mathf.Sin(t);

        x /= Mathf.Cos(currentLong);

        newLat = x + currentLat;
        newLong = y + currentLong;
    }
}
