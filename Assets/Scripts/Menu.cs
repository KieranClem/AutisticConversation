using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject PauseUI;
    bool GamePaused = false;
    public Text objectiveTextBox;
    private string CurrentObjectiveText;

    private void Awake()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (!GamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        PauseUI.SetActive(true);
        DisplayCurrentObjective();
        GamePaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        PauseUI.SetActive(false);
        GamePaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void StoreCurrentObjective(string objective)
    {
        CurrentObjectiveText = objective;
    }

    void DisplayCurrentObjective()
    {
        objectiveTextBox.text = "Last instruction message recived: " +  CurrentObjectiveText;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Hi, we be closing the game if this was an actual build");
    }
}
