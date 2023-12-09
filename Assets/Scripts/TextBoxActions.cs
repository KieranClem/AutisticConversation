using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxActions : MonoBehaviour
{
    //This note is more an idea for later: maybe make it so when stress gets worse the text boxes start to shake, making it harder to ignore?

    private float timeOnScene;
    public float timeTextboxIsActive;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(EndExistence());
        timeOnScene = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeOnScene += Time.deltaTime;
        if(timeOnScene > timeTextboxIsActive)
        {
            Destroy(this.gameObject);
        }
    }

    /*IEnumerator EndExistence()
    {
        yield return new WaitForSeconds(timeTextboxIsActive);
        GameObject.FindGameObjectWithTag("StressManager").GetComponent<StressMeter>().textBoxesThatHaveBeenSpawned.Remove(this.gameObject);
        Destroy(this.gameObject);
    }*/
}
