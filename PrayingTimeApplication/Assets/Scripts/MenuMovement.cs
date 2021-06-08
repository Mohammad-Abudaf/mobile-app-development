using UnityEngine;
using System;
public class MenuMovement : MonoBehaviour{
    public GameObject menuOriginPosition;
    public GameObject menuActivePosition;
    public GameObject menuPanel;
    public GameObject hiddenBackBtn; 

    private bool moveMenuPanel;
    private bool moveMenuPanelBack;

    public float moveSpeed;

    private float tempMenuPosition;
    // Start is called before the first frame update
    private void Start(){
        menuPanel.transform.position = menuOriginPosition.transform.position;
        hiddenBackBtn.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (moveMenuPanel) {
            menuPanel.transform.position =
                Vector3.Lerp(menuPanel.transform.position, menuActivePosition.transform.position, moveSpeed * Time.deltaTime);

            if (menuPanel.transform.localPosition.x == tempMenuPosition) {
                moveMenuPanel = false;
                menuPanel.transform.position = menuActivePosition.transform.position;
                tempMenuPosition = -999999999999.99f;
            }

            if (moveMenuPanel) {
                tempMenuPosition = menuPanel.transform.position.x;
            }
        }
        if (moveMenuPanelBack) {
            menuPanel.transform.position =
                Vector3.Lerp(menuPanel.transform.position, menuOriginPosition.transform.position, moveSpeed * Time.deltaTime);

            if (menuPanel.transform.localPosition.x == tempMenuPosition) {
                moveMenuPanelBack = false;
                menuPanel.transform.position = menuOriginPosition.transform.position;
                tempMenuPosition = -999999999999.99f;
            }

            if (moveMenuPanelBack) {
                tempMenuPosition = menuPanel.transform.position.x;
            }
        }
    }
    public void MovePanel(){
        moveMenuPanelBack = false;
        moveMenuPanel = true;
        hiddenBackBtn.SetActive(true);
        Debug.Log("opening the side menu");
    }
    public void MovePanelBack(){
        moveMenuPanel = false;
        moveMenuPanelBack = true;
        hiddenBackBtn.SetActive(false);
        Debug.Log("closing the side menu");
    }
}
