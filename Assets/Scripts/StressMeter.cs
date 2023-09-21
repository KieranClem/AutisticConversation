using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressMeter : MonoBehaviour
{
    StressTracker stressTracker;
    int currentStress;
    
    // Start is called before the first frame update
    void Start()
    {
        //Make sure this script is only on a empty gameobject, is here just to track the stress meter and activate the appropriate methods
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToStress(int StressToAdd)
    {
        Debug.Log("Hi " + StressToAdd);

        currentStress = stressTracker.AddToStress(StressToAdd);

        //from here add a checker to see how stressed the character is and add the triggers that will call the effects
    }

    public void RemoveStress(int StressToRemove)
    {

        currentStress = stressTracker.DecreaseStress(StressToRemove);
    }
}
