﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;

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

    void LaunchScan()
    {
        Debug.Log("LaunchScan");
        //if(platform.tag == "PlatformBras")
        this.GetComponent<Animator>().SetBool("Scan", true);
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Out", false);
    }

    void CloseScan()
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
        this.GetComponent<Animator>().SetBool("Scan", false);
        this.GetComponent<Animator>().SetBool("In", true);
        this.GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(time);
        LaunchScan();
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
