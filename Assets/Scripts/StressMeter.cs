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
    public bool InNoisyEnviroment;
    public float waitTimeBetweenSpawningTextBoxes;
    public float numberOfTextBoxesThatCanSpawn;
    public List<GameObject> imageList;
    public RectTransform GameCanvas;
    private bool SpawningTextBoxes = false;
    [HideInInspector] public List<GameObject> textBoxesThatHaveBeenSpawned;

    // Start is called before the first frame update
    void Awake()
    {
        //Checks if a stress meters is already in scene and if so, deletes itself. This is for making testing easier, as I can keep stress meters in conversation scenes for testing without needing to delete them after
        GameObject [] meters = GameObject.FindGameObjectsWithTag("StressManager");
        if (meters.Length > 1)
        {
            //Allows for variables to be updated to match the current scene eg if the enviroment is noisy the redundent stress meter will inform the already existing one of that before deleteing itself
            foreach(GameObject meter in meters)
            {
                if(meter != this.gameObject)
                {
                    meter.GetComponent<StressMeter>().InNoisyEnviroment = InNoisyEnviroment;
                    meter.GetComponent<StressMeter>().AddToStress(0);
                }
            }
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

        if (currentStress >= 10 && currentStress < 20)
        {
            //starts first effect and checks if it has already been activates, if it has nothing happens
            if (!MainCamera.CharacterStressed)
            {
                MainCamera.CharacterStressed = true;
                MainCamera.CancelRotationToSpeaker();
                StartCoroutine(MainCamera.LookBetweenTwoPoints());
            }
            
        }
        else if(currentStress >= 20 && InNoisyEnviroment)
        {
            SpawningTextBoxes = true;
            this.GetComponent<AudioSource>().Play();
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
            MainCamera.LookToSpeaker(GameObject.FindGameObjectWithTag("ConversationPartner").gameObject);
        }

        if (currentStress < 20)
        {
            StopCoroutine(SpawnTextBoxes());
            SpawningTextBoxes = false;
            foreach(GameObject box in textBoxesThatHaveBeenSpawned)
            {
                Destroy(box);
            }
            this.GetComponent<AudioSource>().Stop();
            textBoxesThatHaveBeenSpawned.Clear();
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
        while(SpawningTextBoxes == true)
        {
            GameObject textBoxToSpawn = imageList[Random.Range(0, imageList.Count)];

            float xPos = Random.Range(0, Screen.width);
            float yPos = Random.Range(0, Screen.height);

            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);

            GameObject spawnObj = Instantiate(textBoxToSpawn, spawnPosition, Quaternion.identity, GameCanvas);

            textBoxesThatHaveBeenSpawned.Add(spawnObj);

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
            StressfulLocation = false;
            stressMeterGauge.transform.parent.gameObject.SetActive(CanSeeStressGauge);
        }
    }
}
