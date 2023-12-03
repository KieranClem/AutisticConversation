using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float Speed = 0.5f;
    public float DistanceToMove = 5f;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 rotationA;
    Vector3 rotationB;

    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        pointB = new Vector3(this.transform.position.x + DistanceToMove, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);



    }
}
