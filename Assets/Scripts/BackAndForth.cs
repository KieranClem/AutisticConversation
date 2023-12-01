using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float Speed = 0.5f;
    public float DistanceToMove = 5f;
    Vector3 pointA;
    Vector3 pointB;
    
    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(this.transform.position.x, 0, 0);
        pointB = new Vector3(this.transform.position.x + DistanceToMove, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);

    }
}
