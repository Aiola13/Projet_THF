using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlatformManager : MonoBehaviour
{
    public GameObject platform;
    public Transform prefab;
    

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

    void OpenPole(GameObject _pole)
    {
        Debug.Log("OpenPole");
        Instantiate(prefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
        _pole.GetComponent<Animator>().SetBool("Out", false);
        _pole.GetComponent<Animator>().SetBool("In", true);
    }

    void ClosePlatform(GameObject _platform)
    {
        if(_platform.tag == "Platform")
        {
            Debug.Log("ClosePlatform");
            _platform.GetComponent<Animator>().SetBool("Open", false);
            _platform.GetComponent<Animator>().SetBool("Close", true);
        }
    }

    void OpenAllPlatform(GameObject[] _platform)
    {
       
    }

    void CloseAllPlatform(GameObject[] _platform)
    {
        
    }

    
}
