using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressMeter : MonoBehaviour
{
    StressTracker stressTracker;
    int currentStress;

    [Header("Information about adding stress")]

    public int TimeBeforeAddingToStress;
    public int StressToAddEachLoop;
    public bool StressfulLocation;

    private CamMovement MainCamera;

    [Header("Information needed to randomly spawn textboxes")]

    //Information surrounding spawning random UI objects
    public float waitTimeBetweenSpawningTextBoxes;
    public float numberOfTextBoxesThatCanSpawn;
    public List<GameObject> imageList;
    public RectTransform GameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //Make sure this script is only on a empty gameobject, is here just to track the stress meter and activate the appropriate methods
        DontDestroyOnLoad(this.gameObject);

        stressTracker = Resources.Load("TrackStress") as StressTracker;

        stressTracker.Stress = 0;

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamMovement>();


        StartCoroutine(StressIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            RemoveStress(10);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            AddToStress(10);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(SpawnTextBoxes());
        }
    }

    public void AddToStress(int StressToAdd)
    {
        currentStress = stressTracker.AddToStress(StressToAdd);

        //from here add a checker to see how stressed the character is and add the triggers that will call the effects

        if(currentStress >= 10 && currentStress < 20)
        {
            //starts first effect and checks if it has already been activates, if it has nothing happens
            if (!MainCamera.CharacterStressed)
            {
                MainCamera.CharacterStressed = true;
                StartCoroutine(MainCamera.StartStressMovement());
            }
            
        }
    }

    public void RemoveStress(int StressToRemove)
    {

        currentStress = stressTracker.DecreaseStress(StressToRemove);

        if (currentStress < 10)
        {
            MainCamera.CharacterStressed = false;
            StopCoroutine(MainCamera.StartStressMovement());
        }
    }

    IEnumerator StressIncrease()
    {
        while(StressfulLocation)
        {
            yield return new WaitForSeconds(TimeBeforeAddingToStress);
            AddToStress(StressToAddEachLoop);
        }
    }

    IEnumerator SpawnTextBoxes()
    {
        for(int i = 0; i < numberOfTextBoxesThatCanSpawn; i++)
        {
            GameObject textBoxToSpawn = imageList[Random.Range(0, imageList.Count)];

            Vector3 spawnPosition = GetBottomLeftCornerOfCanvas(GameCanvas) - new Vector3(Random.Range(0, GameCanvas.rect.x), Random.Range(0, GameCanvas.rect.y), 0);

            GameObject spawnObj = Instantiate(textBoxToSpawn, spawnPosition, Quaternion.identity, GameCanvas);

            yield return new WaitForSeconds(waitTimeBetweenSpawningTextBoxes);
        }
    }

    Vector3 GetBottomLeftCornerOfCanvas(RectTransform rectTransform)
    {
        Vector3[] v = new Vector3[4];
        rectTransform.GetWorldCorners(v);
        return v[0];
    }

    /*
    void MakeCameraMove()
    {
        
        float xPos = MainCamera.transform.rotation.x;
        float yPos = MainCamera.transform.rotation.y;

        xPos += Random.Range(-10f, 10f) * Time.deltaTime;
        yPos += Random.Range(-10f, 10f) * Time.deltaTime;

        xPos = Mathf.Clamp(xPos, -20f, 20f);
        yPos = Mathf.Clamp(yPos, -20f, 20f);

        MainCamera.transform.rotation = Quaternion.Euler(xPos, yPos, 0);
        CameraOrientation.rotation = Quaternion.Euler(0, yPos, 0);

        Debug.Log(MainCamera.transform.rotation);

        //Debug.Log("Hi");
    }*/
}
