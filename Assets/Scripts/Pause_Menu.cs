using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Pause_Menu : MonoBehaviour
{
    public bool onPauseMenu;
    public Button exitButton;
    //public GameObject PlayerUI;
    public GameObject PauseUI;
    //public GameObject RestartUI;
    

    // Start is called before the first frame update
    void Start()
    {
        Button ext = exitButton.GetComponent<Button>();
        onPauseMenu = false;

        ext.onClick.AddListener(ExitGame);
    }
    private void Update()
    {
        
        if (onPauseMenu==true)
        {
            //PlayerUI.SetActive(false);
            PauseUI.SetActive(true);
        }
        else
        {
            //PlayerUI.SetActive(true);
            PauseUI.SetActive(false);
        }
    }
    void ExitGame()
    {
        Debug.Log("You have exited the game!");
        Application.Quit();
    }
}
