using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressRedirectory : MonoBehaviour
{
    private StressMeter mainStressMeter;
    
    // Start is called before the first frame update
    void Start()
    {
        mainStressMeter = GameObject.FindGameObjectWithTag("StressManager").GetComponent<StressMeter>();
    }

    public void RedirectStressAdition(int StressToAdd)
    {
        mainStressMeter.AddToStress(StressToAdd);
    }

    public void RedirectStressDecrease(int StressToTakeAway)
    {
        mainStressMeter.RemoveStress(StressToTakeAway);
    }
}
