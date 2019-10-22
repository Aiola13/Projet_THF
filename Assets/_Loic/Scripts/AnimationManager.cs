using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AnimationManager : MonoBehaviour
{
    public GameObject arm;
    public GameObject pole;
    public GameObject platform;
    public Transform prefab;
    public GameObject platformPole;
    public List<GameObject> platforms = new List<GameObject>();
    
    Coroutine maCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        if(platform != null)
            OpenPlatform(platform);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.tag == "Pole")
        {
            if(this.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().tag == prefab.tag)
            {
                ClosePole(pole);
            }
        }
    }

    void OpenPlatform(GameObject _platform)
    {
        if(_platform.tag == "Platform")
        {
            Debug.Log("OpenPlatform");
            _platform.GetComponent<Animator>().SetBool("Close", false);
            _platform.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    void ClosePlatform()
    {
        if(platform.tag == "Platform")
        {
            Debug.Log("ClosePlatform");
            platform.GetComponent<Animator>().SetBool("Open", false);
            platform.GetComponent<Animator>().SetBool("Close", true);
        }
    }

    public void OpenPole()
    {
        Debug.Log("OpenPole");
        Instantiate(prefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
        pole.GetComponent<Animator>().SetBool("Out", false);
        pole.GetComponent<Animator>().SetBool("In", true);
    }

    void ClosePole(GameObject _pole)
    {
        Debug.Log("ClosePole");
        _pole.GetComponent<Animator>().SetBool("In", false);
        _pole.GetComponent<Animator>().SetBool("Out", true);
    }

    void OpenScan()
    {
        Debug.Log("OpenScan");
        //if(platform.tag == "PlatformBras")
        maCoroutine = StartCoroutine(MaCouroutine(platformPole));
        arm.GetComponent<Animator>().SetBool("Scan", false);
        arm.GetComponent<Animator>().SetBool("In", true);
        arm.GetComponent<Animator>().SetBool("Out", false);
            
    }

    void LaunchScan(GameObject _arm)
    {
        Debug.Log("LaunchScan");
        //if(platform.tag == "PlatformBras")
            _arm.GetComponent<Animator>().SetBool("Scan", true);
            _arm.GetComponent<Animator>().SetBool("In", false);
            _arm.GetComponent<Animator>().SetBool("Out", false);
    }

    void CloseScan(GameObject _arm)
    {
        Debug.Log("CloseScan");
        //if(platform.tag == "PlatformBras")
            _arm.GetComponent<Animator>().SetBool("In", false);
            _arm.GetComponent<Animator>().SetBool("Scan", false);
            _arm.GetComponent<Animator>().SetBool("Out", true);
    }

    IEnumerator MaCouroutine(GameObject _go)
    {
        _go.GetComponent<Animator>().SetBool("In", true);
        _go.GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(10.0f);




        
        yield return null;
    }
}
