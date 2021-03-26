using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour{
    public new Text name;
    public Text valueAsText;
    private int inComingValue;
    private void Start()
    {
        name.gameObject.transform.Rotate(0, 0,0);
        print("the game has started! ");
    }
    private void Update(){
        // ReSharper disable once HeapView.BoxingAllocation
        valueAsText.text = $"%{(float)Mathf.Round(inComingValue)}";
    }

    public void rotationControl(float value){
        name.gameObject.transform.Rotate(0, 0, value);
        print("rotating the object!");
        inComingValue = (int)value;
    }
}
