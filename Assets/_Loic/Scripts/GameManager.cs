using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region INSPECTOR FIELDS

        public string alert = "";
        [SerializeField] private bool hasRun = true;
        public bool headSetSnapped = false;

        [Header("DEBUG")]
        [SerializeField] public Pole pole;
        [SerializeField] public Arm arm;
        [SerializeField] private List<Platform> platformList = new List<Platform>();
        [SerializeField] private Platform platformPole;
        [SerializeField] private Platform platformArm;
        [SerializeField] public GameObject headSetPrefab;
        [SerializeField] public GameObject headSetInstance;
        [SerializeField] public GameObject scanEffectPrefab;
        [SerializeField] public GameObject scanEffetInstance;
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
        foreach(Platform p in platformList)
        {
            if(p.tag == "PlatformArm")
                platformArm = p;

            if(p.tag == "PlatformPole")
            {
                platformPole = p;
                delay = StartCoroutine(platformPole.OpenPlatformDelay("PlatformPole", 10.0f));  
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

            if(hasRun && platformArm.stateOpen && arm.animationEnded && arm.stateOpen)
            {
                hasRun = false;
                scanEffetInstance = Instantiate(scanEffectPrefab, new Vector3(player.transform.localPosition.x, 1, player.transform.localPosition.z), Quaternion.identity);
            }
        }

    }

    public void GetAlertEvents(string _alert)
    {
        alert = _alert;
    }
  
    
}
