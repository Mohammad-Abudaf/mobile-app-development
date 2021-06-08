using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class generateTargetLocation : MonoBehaviour {

    // Use this for initialization

    private static float w;
    private static float t;
    private static float currentLat;
    private static float currentLong;
    private static float x;
    private static float y;
    private static float radius;
    private static float rand1;
    private static float rand2;

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

private void Start () {

        radius = 6371f / 111000f;
        

        newLat = 0;
        newLong = 0;
    }
	
    // Update is called once per frame
    private void Update () {

        //targetLocation();
        
    }

    public static void targetLocation()
    {
        currentLat = GPS.Latitude;
        currentLong = GPS.Longitude;

        //currentLat = 71.43153f;
        //currentLong = 33.97607f;

        rand1 = Random.Range(0.0f, 1.0f);
        rand2 = Random.Range(0.0f, 1.0f);

        w = radius * Mathf.Sqrt(rand1);
        t = 2 * Mathf.PI * rand2;
        x = w * Mathf.Cos(t);
        y = w * Mathf.Sin(t);

        x /= Mathf.Cos(currentLong);

        //newLat = x + currentLat;
        //newLong = y + currentLong;
        
        newLat = 21.25f;
        newLong = 39.49f;

    }
}