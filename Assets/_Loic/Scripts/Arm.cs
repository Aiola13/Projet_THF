using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;
    [SerializeField] public AudioSource sound;
    [SerializeField] private bool hasRun = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenScan()
    {
        Debug.Log("OpenScan");
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("Scan", false);
        this.GetComponent<Animator>().SetBool("In", true);
        this.GetComponent<Animator>().SetBool("Out", false);
            
        LaunchScan();
    }

    public void LaunchScan()
    {
        Debug.Log("LaunchScan");
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("Scan", true);
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Out", false);
    }

    public void CloseScan()
    {
        Debug.Log("CloseScan");
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Scan", false);
        this.GetComponent<Animator>().SetBool("Out", true);
    }


    public IEnumerator OpenScanDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("OpenScanDelay");
        if(!hasRun)
        {
            sound.PlayOneShot(sound.clip);
            hasRun = true;
        }
        this.GetComponent<Animator>().SetBool("Scan", false);
        this.GetComponent<Animator>().SetBool("In", true);
        this.GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(time);
        //LaunchScan();
    }

    public IEnumerator LaunchScanDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("LaunchScanDelay");
        sound.PlayOneShot(sound.clip);
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("Scan", true);
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Out", false);
    }

    public IEnumerator CloseScanDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(GameManager.instance.scanEffetInstance);
        Debug.Log("CloseScanDelay");
        sound.PlayDelayed(2.7f);
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Scan", false);
        this.GetComponent<Animator>().SetBool("Out", true);
        yield return new WaitForSeconds(time);
        
    }


    void AlertEvents(string message)
    {
        if (message.Equals("AnimationOver"))
        {
            Debug.Log("Animation Over");
            animationEnded = true;
            stateOpen = !stateOpen;
        } 
    }
}
