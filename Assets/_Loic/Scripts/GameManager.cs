using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region INSPECTOR FIELDS

        [SerializeField] private bool hasRun = true;
        [SerializeField] private bool updateUI = true;
        [SerializeField] private bool doUpdateUI = true;
        [SerializeField] private bool showPrinter = false;
        public bool headSetSnapped = false;
        public bool QRCodeHited = false;
        public bool dataTransmitted = false;
        public bool showHelicopter = false;

        [Header("DEBUG")]
        [SerializeField] public Pole pole;
        [SerializeField] public Arm arm;
        [SerializeField] private List<Platform> platformList = new List<Platform>();
        [SerializeField] private List<Platform> platformPrinter = new List<Platform>();
        [SerializeField] private List<GameObject> printerList = new List<GameObject>(); 
        [SerializeField] private Platform platformPole;
        [SerializeField] private Platform platformArm;
        [SerializeField] public GameObject headSetPrefab;
        [SerializeField] public GameObject headSetInstance;
        [SerializeField] public GameObject scanEffectPrefab;
        [SerializeField] public GameObject scanEffetInstance;
        [SerializeField] public GameObject printerPrefab;
        [SerializeField] public GameObject player;
        [SerializeField] public GameObject helicopterPrefab;
        [SerializeField] public GameObject boxPrefab;
        [SerializeField] public CameraShake cameraShake;

        [SerializeField] public GameObject particle;

        public event EventHandler UpdateUIEvent;

        [Header("Coroutine")]
        [Tooltip("Contient ma coroutine")]
        [SerializeField] private Coroutine delay;

	#endregion


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);   

        foreach(Platform p in platformList)
        {
            if(p.tag == "PlatformArm")
                platformArm = p;

            if(p.tag == "PlatformPole")
            {
                platformPole = p;
                delay = StartCoroutine(platformPole.OpenPlatformDelay("PlatformPole", 10.0f));  
            }  

            if(p.tag == "PlatformPrinter")
            {
                platformPrinter.Add(p);

                //printerList.Add(Instantiate(printerPrefab, p.transform.position + new Vector3(0, -0.082f, 0), Quaternion.Euler(180, 0, 0), p.transform));
                printerList.Add(Instantiate(printerPrefab, p.transform.position + new Vector3(0, -0.077f, 0), Quaternion.Euler(90, 0, 0), p.transform));
            }
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if(platformPole.stateOpen == true && pole.animationEnded == false && pole.stateOpen == false)
        {
            if(hasRun)
                headSetInstance = Instantiate(headSetPrefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
                
                
            hasRun = false;
            pole.OpenPole();


            StartCoroutine(LookAtRetardArm());
        }
            
        if(headSetSnapped)
        {
            if(!headSetInstance.GetComponent<HeadSet>().UI.activeInHierarchy)
                headSetInstance.GetComponent<HeadSet>().UI.SetActive(true);

            if(updateUI)
                StartCoroutine(UpdateUI());

            if(pole.stateOpen)
                pole.ClosePole();

            if(pole.animationEnded && !pole.stateOpen && platformPole.stateOpen)
                platformPole.ClosePlatform("PlatformPole");

            if(!platformPole.stateOpen && !platformArm.stateOpen && !platformArm.animationEnded)
            {
                platformArm.OpenPlatform("PlatformArm");
                delay = StartCoroutine(arm.OpenScanDelay(5.0f));
                
                hasRun = true;
            }

            if(hasRun && platformArm.stateOpen && arm.animationEnded && arm.stateOpen && QRCodeHited)
            {
                updateUI = true;
                hasRun = false;
                arm.LaunchScan();
                
                scanEffetInstance = Instantiate(scanEffectPrefab, new Vector3(player.transform.position.x, 1, player.transform.position.z), Quaternion.identity);
                delay = StartCoroutine(arm.CloseScanDelay(10.0f));



                printerList[0].transform.GetChild(0).gameObject.GetComponent<VolumicVR.PrinterStand>().PrintEndedEvent += OnPrintingEnded;
                if(doUpdateUI)
                {
                    doUpdateUI = false;
                    updateUI = true;
                }
                    

            }

            if(!hasRun && QRCodeHited && platformArm.stateOpen && arm.animationEnded && !arm.stateOpen)
            {
                particle.SetActive(true);

               

                platformArm.ClosePlatform("PlatformArm");

                //Hide Prefab to save ressources
                arm.gameObject.SetActive(false);
                pole.gameObject.SetActive(false);


                /* 
                    *****************************************
                    *****************************************
                        Here Launch Visual Data Transmission
                    *****************************************
                    *****************************************
                */

                dataTransmitted = true;
                showPrinter = true;
            }

        }

        if(showPrinter && QRCodeHited && platformArm.animationEnded && !platformArm.stateOpen && arm.animationEnded && !arm.stateOpen)
        {
            
            foreach(Platform p in platformPrinter)
            {
                if(!p.stateOpen && !p.animationEnded)
                    p.OpenPlatform("PlatformPrinter");
            }
                

            delay = StartCoroutine(LookAtRetard());

            if(dataTransmitted)        
            {
                dataTransmitted = false;
                updateUI = true;
                delay = StartCoroutine(LaunchPrint());
            }   
        }


        if(showHelicopter)
        {
            showPrinter = false;
            if(!helicopterPrefab.activeInHierarchy)
                helicopterPrefab.SetActive(showHelicopter);         
            showHelicopter = false;

            updateUI = true;
            
            helicopterPrefab.GetComponent<Helicopter>().HelicopterEndedEvent += LaunchPaint3D;   
            helicopterPrefab.GetComponent<Helicopter>().PaintEndedEvent += LaunchMoonTag;        
        }

    }

    IEnumerator LookAtRetard()
    {
        yield return new WaitForSeconds(3.0f);

        printerList.ForEach((t) => {
            var relativePos = player.transform.position - t.transform.GetChild(0).gameObject.transform.position;
            // Only rotation on y axis
            relativePos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
           
            t.transform.GetChild(0).gameObject.transform.rotation = Quaternion.RotateTowards(t.transform.GetChild(0).gameObject.transform.rotation, rotation, 500.0f * Time.deltaTime);
        });
        Debug.Log("I am doing this fucking rotation !!!!!!!!!!!!!");
    }

    IEnumerator LaunchPrint()
    {
        yield return new WaitForSeconds(4.0f);
        foreach(var t in printerList)
            t.transform.GetChild(0).gameObject.GetComponent<VolumicVR.PrinterStand>().StartPrinting();
    }

    void InitialState()
    {
        
    }

    public void OnPrintingEnded(object o, EventArgs e)
    {
        particle.SetActive(false);
        /*printerList[0].GetComponentInChildren<Highlighter>().enabled = true;
        printerList[0].GetComponentInChildren<Highlighter>().HighLighterEndedEvent += OnHighLighterEnded;*/

        foreach(GameObject go in printerList)
        {
            go.GetComponentInChildren<Highlighter>().enabled = true;
            go.GetComponentInChildren<Highlighter>().HighLighterEndedEvent += OnHighLighterEnded;
        }
    }

    public void LaunchMoonTag(object o,EventArgs e)
    {
        helicopterPrefab.GetComponent<Helicopter>().PaintEndedEvent -= LaunchMoonTag; 
        boxPrefab.transform.SetParent(null);
        boxPrefab.AddComponent<Rigidbody>();

        Debug.Log("Launch MOOOOOON TAGGGGGGGGGGGGGGGGGG");

    }

    public void LaunchPaint3D(object o,EventArgs e)
    {
        helicopterPrefab.GetComponent<Helicopter>().HelicopterEndedEvent -= LaunchPaint3D;
        helicopterPrefab.GetComponent<Helicopter>().LaunchTailPaint("Fade");
        helicopterPrefab.GetComponent<Helicopter>().LaunchTailPaint("Opaque");
    }

    public void OnHighLighterEnded(object o, EventArgs e)
    {
        cameraShake.enabled = true;
        foreach(Platform p in platformPrinter)
            p.ClosePlatform("PlatformPrinter");

        printerList[0].transform.GetChild(0).gameObject.GetComponentInChildren<Highlighter>().HighLighterEndedEvent -= OnHighLighterEnded;

        foreach(GameObject p in printerList)
            p.SetActive(false);

        showHelicopter = true;
    }

    /*public void UpdateUI()
    {
        updateUI = false;
        UpdateUIEvent?.Invoke ( this, EventArgs.Empty );  
    }*/

    IEnumerator UpdateUI()
    {
        updateUI = false;
        yield return new WaitForSeconds(1.5f);
        UpdateUIEvent?.Invoke ( this, EventArgs.Empty );  
    }





    IEnumerator LookAtRetardArm()
    {
        yield return new WaitForSeconds(10.0f);

        var relativePos = arm.transform.position - player.transform.position;
        // Only rotation on y axis
        relativePos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        
        arm.transform.rotation = Quaternion.RotateTowards(arm.transform.rotation, rotation, 500.0f * Time.deltaTime);
    }
  
}
