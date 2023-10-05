using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxActions : MonoBehaviour
{
    //This note is more an idea for later: maybe make it so when stress gets worse the text boxes start to shake, making it harder to ignore?


    public float timeTextboxIsActive;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndExistence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndExistence()
    {
        yield return new WaitForSeconds(timeTextboxIsActive);
        Destroy(this.gameObject);
    }
}
