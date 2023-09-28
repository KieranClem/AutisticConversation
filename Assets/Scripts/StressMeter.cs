using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressMeter : MonoBehaviour
{
    StressTracker stressTracker;
    int currentStress;

    public int TimeBeforeAddingToStress;
    public int StressToAddEachLoop;
    public bool StressfulLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        //Make sure this script is only on a empty gameobject, is here just to track the stress meter and activate the appropriate methods
        DontDestroyOnLoad(this.gameObject);

        stressTracker = Resources.Load("TrackStress") as StressTracker;

        stressTracker.Stress = 0;

        StartCoroutine(StressIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            StressfulLocation = false;
        }
    }

    public void AddToStress(int StressToAdd)
    {
        currentStress = stressTracker.AddToStress(StressToAdd);

        //from here add a checker to see how stressed the character is and add the triggers that will call the effects

        if(currentStress >= 10 && currentStress < 20)
        {
            //Insert function here
        }
    }

    public void RemoveStress(int StressToRemove)
    {

        currentStress = stressTracker.DecreaseStress(StressToRemove);
    }

    IEnumerator StressIncrease()
    {
        while(StressfulLocation)
        {
            yield return new WaitForSeconds(TimeBeforeAddingToStress);
            AddToStress(StressToAddEachLoop);
        }
    }
}
