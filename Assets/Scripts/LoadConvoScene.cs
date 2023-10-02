using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConvoScene : MonoBehaviour
{
    public void LoadNeededScene(string CharacterName)
    {
        CharacterName += "Scene";

        SceneManager.LoadScene(CharacterName);
    }
}
