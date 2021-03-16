using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SettingScreen01;
public class HomeScreen : MonoBehaviour{
    public Sprite[] allImgs;
    public Image myImg;
    private int _index;
    private void Start(){
        _index = SettingScreen.getIndex();
        myImg.sprite = allImgs[_index];
    }
    public void MoveToSettings(){
        SceneManager.LoadScene("Setting");
        print("moving to settings ");
    }
    public void MoveToTimer(){
        SceneManager.LoadScene("Timer");
        print("moving to timer");
    }
    public void QuitApp(){
        print("the application will quit");
        Application.Quit();
    }
}