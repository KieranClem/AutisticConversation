using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float xSensitivity;
    public float ySensitivity;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private bool canMove = true;

    private bool RotateToSpeaker = false;
    private Vector3 SpeakerLocation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        canMove = true;
        RotateToSpeaker = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {


            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        if(RotateToSpeaker)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, SpeakerLocation, Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(SpeakerLocation), Time.deltaTime);
        }
    }

    public void LookToSpeaker(GameObject Speaker)
    {
        SpeakerLocation = Speaker.transform.position;
        Debug.Log(Speaker.transform.position);
        RotateToSpeaker = true;
        canMove = false;
    }

    public void StopLookingAtSpeaker()
    {
        RotateToSpeaker = false;
        canMove = true;
    }
}
