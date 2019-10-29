using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using System.Threading;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region INSPECTOR FIELDS

        [SerializeField] private bool hasRun = true;
        public bool headSetSnapped = false;
        public bool QRCodeHited = false;
        public bool dataTransmitted = false;

        [Header("DEBUG")]
        [SerializeField] public Pole pole;
        [SerializeField] public Arm arm;
        [SerializeField] private List<Platform> platformList = new List<Platform>();
        [SerializeField] private List<Platform> platformPrinter = new List<Platform>();
        [SerializeField] private List<GameObject> printer = new List<GameObject>(); 
        [SerializeField] private Platform platformPole;
        [SerializeField] private Platform platformArm;
        [SerializeField] public GameObject headSetPrefab;
        [SerializeField] public GameObject headSetInstance;
        [SerializeField] public GameObject scanEffectPrefab;
        [SerializeField] public GameObject scanEffetInstance;
        [SerializeField] public GameObject printerPrefab;
        [SerializeField] public GameObject player;
        


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
    }

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Platform p in platformList)
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

                printer.Add(Instantiate(printerPrefab, p.transform.position + new Vector3(0, -0.082f, 0), Quaternion.Euler(180, 0, 0), p.transform));
            }
        }*/


        Parallel.ForEach(platformList, p => {
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

                printer.Add(Instantiate(printerPrefab, p.transform.position + new Vector3(0, -0.082f, 0), Quaternion.Euler(180, 0, 0), p.transform));
            }
        });
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
        }
            
        if(headSetSnapped)
        {
            if(pole.stateOpen)
                pole.ClosePole();

            if(pole.animationEnded && !pole.stateOpen)
                platformPole.ClosePlatform("PlatformPole");

            if(!platformPole.stateOpen && !platformArm.stateOpen)
            {
                platformArm.OpenPlatform("PlatformArm");
                delay = StartCoroutine(arm.OpenScanDelay(5.0f));
                
                hasRun = true;
            }

            if(hasRun && platformArm.stateOpen && arm.animationEnded && arm.stateOpen && QRCodeHited)
            {
                hasRun = false;
                arm.LaunchScan();
                scanEffetInstance = Instantiate(scanEffectPrefab, new Vector3(player.transform.localPosition.x, 1, player.transform.localPosition.z), Quaternion.identity);
                delay = StartCoroutine(arm.CloseScanDelay(10.0f));
            }

            if(QRCodeHited && platformArm.stateOpen && arm.animationEnded && !arm.stateOpen)
            {
                platformArm.ClosePlatform("PlatformArm");

                /* 
                    *****************************************
                    *****************************************
                        Here Launch Visual Data Transmission
                    *****************************************
                    *****************************************
                */

                dataTransmitted = true;
            }
        }

        if(dataTransmitted)
        {
            foreach(Platform p in platformPrinter)
                p.OpenPlatform("PlatformPrinter");
            
            delay = StartCoroutine(LookAtRetard());
        }
    }

    IEnumerator LookAtRetard()
    {
        yield return new WaitForSeconds(3.0f);

        printer.ForEach((t) => {Vector3 relativePos = player.transform.position - t.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            t.transform.rotation = Quaternion.RotateTowards(t.transform.rotation, rotation , 100.0f * Time.deltaTime);

            t.GetComponent<VolumicVR.PrinterStand>().StartPrinting();
        });
    }
  
    
}
