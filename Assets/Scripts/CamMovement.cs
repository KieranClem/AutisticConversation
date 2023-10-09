using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    //General movement information
    public float xSensitivity;
    public float ySensitivity;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public bool canMove = true;

    //Information for rotating to the speaker
    private bool RotateToSpeaker = false;
    private Vector3 SpeakerLocation;
    private PlayerMovement player;

    //Information releated to tracking and altering player due to stress
    private StressMeter tracking;
    [HideInInspector] public bool CharacterStressed;

    // Start is called before the first frame update
    void Start()
    {
        if (canMove)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            player = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<PlayerMovement>();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        canMove = true;
        RotateToSpeaker = false;
        CharacterStressed = false;

        CheckCurrentStress();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Tracks the player mouse movements and changes the camera rotation to match
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

            yRotation += mouseX;

            xRotation -= mouseY;
            //locks the camera to stop is from rotating more then 90 degrees up or down
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        if(RotateToSpeaker)
        {
            //look towards the speaker
            var rotation = Quaternion.LookRotation(SpeakerLocation - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50 * Time.deltaTime);
        }
    }

    public void LookToSpeaker(GameObject Speaker)
    {
        SpeakerLocation = Speaker.transform.position;
        RotateToSpeaker = true;
        canMove = false;
        player.CanMove = false;
    }

    public void StopLookingAtSpeaker()
    {
        RotateToSpeaker = false;
        canMove = true;
        player.CanMove = true;
    }

    void CheckCurrentStress()
    {
        tracking = GameObject.FindGameObjectWithTag("StressManager").GetComponent<StressMeter>();
        tracking.AddToStress(0);
    }

    public IEnumerator StartStressMovement()
    {
        while (CharacterStressed)
        {

            xRotation += Random.Range(-500f, 500f) * Time.deltaTime;
            yRotation += Random.Range(-500f, 500f) * Time.deltaTime;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            yield return new WaitForSeconds(1f);

        }
    }
}
