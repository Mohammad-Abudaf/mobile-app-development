using System;
using ServerPortSelect;
using UnityEngine;
using UnityEngine.UI;

public class MainCode : MonoBehaviour{
    public Image secondsHand;
    public Image minHand;
    public Image hoursHand;
    public InputField portInput;
    public Text outputTimeAsDigital;
    private int CurrentSeconds;
    private float CurrentMin;
    private float CurrentHour;

    private ModbusServer myServer = new ModbusServer();
    private void Update(){
        if (!myServer.Coil[1072]) {
            return;
        }
        CurrentSeconds = DateTime.Now.Second;
        CurrentMin = DateTime.Now.Minute + (float)CurrentSeconds/60 ;
        CurrentHour = DateTime.Now.Hour + CurrentMin/60;

            myServer.HoldingRegister[1313] = (ushort)CurrentSeconds;
            myServer.HoldingRegister[1314] = (ushort)CurrentMin;
            myServer.HoldingRegister[1315] = (ushort)CurrentHour;

            secondsHand.gameObject.transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, -6 * CurrentSeconds));
            minHand.gameObject.transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, -6 * CurrentMin));
            hoursHand.gameObject.transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, -30 * CurrentHour));

            outputTimeAsDigital.text = $"{(int)CurrentHour:00}:{(int)CurrentMin:00}:{CurrentSeconds:00}";
        }

    public void ONBtn(){
        myServer.Coil[1072] = true;
        myServer.port = int.Parse(portInput.text);
    }

    public void OffBtn(){
        myServer.Coil[1072] = false;
        myServer.port = 502; //default value
    }

    public void ExitBtn(){
        print("the application will Quit");
        Application.Quit();
    }
}
