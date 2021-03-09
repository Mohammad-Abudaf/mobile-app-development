using UnityEngine;
using UnityEngine.UI;
public class Main: MonoBehaviour{

    public Image background;
    public Text outputSeconds;
    public InputField secondInput;

    private int seconds;
    private bool StartCounting;
    private bool isShown;

    private void Start(){
        StartCounting = false;
    }

    private void Update(){
        if (StartCounting){
            if (Time.frameCount % 60 == 0 && seconds > 0){
                seconds -= 1;
            }
            if (seconds == 0){
                if (isShown) {
                    background.gameObject.SetActive(true);
                    print("the image is shown!");
                }
                else {
                    background.gameObject.SetActive(false);
                    print("the image is hidden!");
                }
            }
        }
        outputSeconds.text = $"remaining {seconds} sec";
    }
    public void ShowBtn(){
        StartCounting = true;
        isShown = true;
        seconds = int.Parse(secondInput.text);
        print("the shown btn pressed!");
    }
    public void HideBtn(){
        StartCounting = true;
        isShown = false;
        seconds = int.Parse(secondInput.text);
        print("the hide button pressed");
    }
}