using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConvoScene : MonoBehaviour
{
    private void Awake()
    {
        GameObject stressMeter = GameObject.FindGameObjectWithTag("StressManager");
        if(stressMeter != null)
        {
            stressMeter.GetComponent<StressMeter>().UpdateVariables();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void LoadNeededScene(string CharacterName)
    {
        CharacterName += "Scene";

        SceneManager.LoadScene(CharacterName);
    }

    public void LoadEndOfDemoScene()
    {
        Destroy(GameObject.FindGameObjectWithTag("StressManager").gameObject);
        SceneManager.LoadScene("EndOfDemoScene");
    }

    public void LoadStartOfDemoScene()
    {
        SceneManager.LoadScene("DemoScene1");
    }
}
