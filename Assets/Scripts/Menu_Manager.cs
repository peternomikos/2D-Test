using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        Button btn = playButton.GetComponent<Button>();
        Button ext = exitButton.GetComponent<Button>();

        ext.onClick.AddListener(ExitGame);
        btn.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        Debug.Log("You have load the scene!");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    void ExitGame()
    {
        Debug.Log("You have exited the game!");
        Application.Quit();
    }
}
