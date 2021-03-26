using System;
using UnityEngine;
using UnityEngine.UI;

/* to make a timer we need:
1. seconds hand.
2. using unity engine user interface
3. specify the seconds hand as img gameObjects
*/

public class HandCode : MonoBehaviour{
    private bool Enable;
    private float seconds;
    public Image SecondsHands;

    private void Start(){
        Enable = false;
    }

    private void Update()
    {
        if (Enable) {
            // ReSharper disable once HeapView.BoxingAllocation
            seconds += Time.deltaTime;
            print($"the seconds now is {seconds}");
            SecondsHands.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -6 * (float)Math.Truncate(seconds)));
        }
    }
    public void Go()
    {
        Enable = true;
        print("btn was pressed!");
    }
    public void Stop()
    {
        Enable = false;
        print("btn was pressed!");

    }
    public void Reset()
    {
        SecondsHands.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
<<<<<<< HEAD
        seconds = 0; 
=======
        seconds = 0;
>>>>>>> 7bbf5d7f31c21507471d8e48c54e1ec70c105d7c
        //SecondHand.gameObject.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        print("btn was pressed!");
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
