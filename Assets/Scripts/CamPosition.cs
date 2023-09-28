using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    public Transform PlayerPosition;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerPosition.position;
    }
}
