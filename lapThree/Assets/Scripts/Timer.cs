using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using SettingScreen01;

public class Timer : MonoBehaviour{
    private bool Enable;
    private float seconds;
    public Image SecondsHands;
    public Image myImg;
    public Sprite[] allImgs;
    private int _index;
    private void Start(){
        _index = SettingScreen.getIndex();
        myImg.sprite = allImgs[_index];
    }

    private void Update(){
        if (Enable) {
            seconds += Time.deltaTime;
            SecondsHands.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -6 * (float)Math.Truncate(seconds)));
        }

    }
    public void Go()
    {
        Enable = true;
        print("the Start btn was pressed!");
    }
    public void Stop()
    {
        Enable = false;
        print("the Stop btn was pressed!");
    }
    public void Reset()
    {
        SecondsHands.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        print("the reset btn was pressed!");
        seconds = 0;
    }
    public void BackBtn(){
        SceneManager.LoadScene("Home");
        print("back to home screen!");
    }
}
