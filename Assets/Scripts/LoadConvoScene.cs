using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConvoScene : MonoBehaviour
{

    private FadeController fadeController;
    private Animator animator;

    private void Awake()
    {
        fadeController = this.GetComponent<FadeController>();
        animator = fadeController.animator;
        
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
        fadeController.FadeOut();

        CharacterName += "Scene";


        StartCoroutine(LoadScene(CharacterName));
        //SceneManager.LoadScene(CharacterName);
    }

    public void LoadEndOfDemoScene()
    {
        fadeController.FadeOut();
        Destroy(GameObject.FindGameObjectWithTag("StressManager").gameObject);
        StartCoroutine(LoadScene("EndOfDemoScene"));
        //SceneManager.LoadScene("EndOfDemoScene");
    }

    public void LoadStartOfDemoScene()
    {
        fadeController.FadeOut();

        StartCoroutine(LoadScene("DemoScene1"));
        //SceneManager.LoadScene("DemoScene1");
    }

    public void LoadScenario2()
    {
        fadeController.FadeOut();

        StartCoroutine(LoadScene("JosephScene"));
        //SceneManager.LoadScene("JosephScene");
    }

    private IEnumerator LoadScene(string SceneName)
    {
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
        SceneManager.LoadScene(SceneName);

    }
}
