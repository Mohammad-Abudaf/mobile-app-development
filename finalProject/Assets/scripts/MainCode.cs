using UnityEngine;
using UnityEngine.UI;


public class MainCode : MonoBehaviour
{
    public GameObject[] medicalContact = new GameObject[8];
    public Button floatingButton;
    public GameObject inputPanel;

    private bool _inputPanelActive;

    //first Diminution is for the main contact (game objects) in the scroll view
    // second Diminution is for the details inside the game objects 0 for name, 1 for time, 2  for description
    public static string[,] _medicalInformation = new string[8, 3];
    private int _countingOfMedicalObjectsIndexes;

    private void Start()
    {
        _inputPanelActive = false;
        inputPanel.SetActive(false);
        _countingOfMedicalObjectsIndexes = 0;

        foreach (var t in medicalContact)
        {
            t.SetActive(false);
        }
        _medicalInformation[0,1] = "00:00"; 

    }

    public void FloatingBtnBehaviour()
    {
        if (!_inputPanelActive)
        {
            inputPanel.SetActive(true);
            _inputPanelActive = true;
        }
        else
        {
            inputPanel.SetActive(false);
            _inputPanelActive = false;
        }
    }

    public InputField nameOfTheDrugInput;
    public InputField timeOfTheDrugInput;
    public InputField descriptionOfTheDrugInput;
    
    public void DoneBtn()
    {
        if (_countingOfMedicalObjectsIndexes > 8)
        {
            _countingOfMedicalObjectsIndexes = 0;
        }

        if (_countingOfMedicalObjectsIndexes < 8)
        {
            _countingOfMedicalObjectsIndexes++;
            _medicalInformation[_countingOfMedicalObjectsIndexes - 1, 0] = nameOfTheDrugInput.text;
            _medicalInformation[_countingOfMedicalObjectsIndexes - 1, 1] = timeOfTheDrugInput.text;
            _medicalInformation[_countingOfMedicalObjectsIndexes - 1, 2] = descriptionOfTheDrugInput.text;
            
            _inputPanelActive = false;
            inputPanel.SetActive(false);
            print("done button was pressed!");
        }
        else
        {
            _countingOfMedicalObjectsIndexes = 0; 
        }
    }
    
    public GameObject[] totalDeleteBtn = new GameObject[10];
    
    public void DeleteBtn(int index)
    {
        _countingOfMedicalObjectsIndexes--;
        medicalContact[index].SetActive(false);
        _medicalInformation[index, 0] = null; 
        _medicalInformation[index, 1] = null; 
        _medicalInformation[index, 2] = null;
        
        print(index);
    }

    private void Update()
    {
        if (_countingOfMedicalObjectsIndexes < 8)
        {
            for (var i = 0; i < _countingOfMedicalObjectsIndexes; i++)
            {
                medicalContact[i].SetActive(true);

                medicalContact[i].GetComponentsInChildren<Text>()[0].text =
                    _medicalInformation[i, 0];
            
                medicalContact[i].GetComponentsInChildren<Text>()[1].text =
                    _medicalInformation[i, 1];
            
                medicalContact[i].GetComponentsInChildren<Text>()[2].text =
                    _medicalInformation[i, 2];
            }
        }
        else
        {
            _countingOfMedicalObjectsIndexes = 0; 
        }
    }
}
