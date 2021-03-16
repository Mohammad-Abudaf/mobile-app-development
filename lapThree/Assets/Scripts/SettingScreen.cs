using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SettingScreen01{
    public class SettingScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        public Sprite[] allImgs;
        public Image myImg;

        private static int _index;

        private void Start(){
            _index = 1;
            myImg.sprite = allImgs[_index];
        }
        public void ChangeBackground(){
            if (_index < 4) {
                _index++;
                myImg.sprite = allImgs[_index];
            }
            else {
                _index = 0;
                _index++;
                myImg.sprite = allImgs[_index];

            }
        }
        public static int getIndex(){
            return _index;
        }
        public void BackToHomeScreen(){
            SceneManager.LoadScene("Home");
            print("moving to Home Screen");
        }
    }
}