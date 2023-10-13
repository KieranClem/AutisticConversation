using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressMeter : MonoBehaviour
{
    StressTracker stressTracker;
    int currentStress;

    public Text stressMeterGauge;
    private bool CanSeeStressGauge = false;

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
    void Awake()
    {
        //Checks if a stress meters is already in scene and if so, deletes itself. This is for making testing easier, as I can keep stress meters in conversation scenes for testing without needing to delete them after
        GameObject [] meters = GameObject.FindGameObjectsWithTag("StressManager");
        if (meters.Length > 1)
        {
            Debug.Log("Extra stress meter deleted");
            Destroy(this.gameObject);
        }
        
        //Make sure this script is only on a empty gameobject, is here just to track the stress meter and activate the appropriate methods
        DontDestroyOnLoad(this.gameObject);

        stressTracker = Resources.Load("TrackStress") as StressTracker;

        stressTracker.Stress = 0;

        currentStress = 0;

        stressMeterGauge.text = currentStress.ToString() + "/" + stressTracker.MaxStress.ToString();

        stressMeterGauge.transform.parent.gameObject.SetActive(CanSeeStressGauge);

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

        if(Input.GetKeyDown(KeyCode.H))
        {
            CanSeeStressGauge = !CanSeeStressGauge;
            stressMeterGauge.transform.parent.gameObject.SetActive(CanSeeStressGauge);
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
        else if(currentStress >= 20)
        {
            StartCoroutine(SpawnTextBoxes());
        }

        stressMeterGauge.text = currentStress.ToString() + "/" + stressTracker.MaxStress.ToString();
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

            float xPos = Random.Range(0, Screen.width);
            float yPos = Random.Range(0, Screen.height);

            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);

            GameObject spawnObj = Instantiate(textBoxToSpawn, spawnPosition, Quaternion.identity, GameCanvas);

            yield return new WaitForSeconds(waitTimeBetweenSpawningTextBoxes);
        }
    }

    public void UpdateVariables()
    {
        if(GameCanvas == null)
        {
            GameCanvas = GameObject.FindGameObjectWithTag("CanvasForTextBoxes").GetComponent<RectTransform>();
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamMovement>();
            stressMeterGauge = GameObject.FindGameObjectWithTag("StressGauge").GetComponent<Text>();
            stressMeterGauge.text = currentStress.ToString() + "/" + stressTracker.MaxStress.ToString();
            stressMeterGauge.transform.parent.gameObject.SetActive(CanSeeStressGauge);
        }
    }
}
