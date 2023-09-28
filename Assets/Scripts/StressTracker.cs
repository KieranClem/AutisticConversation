using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "InformationTracking/StressTracker")]


public class StressTracker : ScriptableObject
{
    public int Stress;
    public int MaxStress;

    public int AddToStress(int stressToAdd)
    {
        Stress = Stress + stressToAdd;

        if (Stress > MaxStress)
            Stress = MaxStress;

        return Stress;
    }

    public int DecreaseStress(int stressToRemove)
    {
        Stress -= stressToRemove;

        if(Stress < 0)
        {
            Stress = 0;
        }

        return Stress;
    }
}
