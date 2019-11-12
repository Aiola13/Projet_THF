﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;
    [SerializeField] public AudioSource sound;
    [SerializeField] private bool hasRun = false;

    void Awake()
    {
        if(this.tag == "Untagged")
        {
            this.enabled = false;
        }
    }

    public void OpenPlatform(string _tag)
    {
        if(_tag == "PlatformArm" && !hasRun)
        {
            sound.PlayDelayed(0.5f);
            hasRun = true;
        }

        if(_tag == "PlatformPrinter" && !hasRun)
        {
            sound.PlayDelayed(0.5f);
            hasRun = true;
        }
        

        if(_tag == "PlatformPrinter")
        {
            Debug.Log("OpenPlatform" + _tag);
            this.GetComponent<Animator>().SetBool("CloseComplete", false);
            this.GetComponent<Animator>().SetBool("OpenComplete", true);
        }
        else
        {
            Debug.Log("OpenPlatform" + this.tag);
            this.GetComponent<Animator>().SetBool("Close", false);
            this.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void ClosePlatform(string _tag)
    {
        if(_tag == "PlatformArm" && hasRun)
        {
            //sound.PlayOneShot(sound.clip);
            sound.PlayDelayed(0.8f);
            hasRun = false;
        }
        else if(_tag != "PlatformArm" && !hasRun)
        {
            sound.PlayDelayed(0.8f);
            hasRun = true;
        }
        

        if(_tag == "PlatformPrinter")
        {
            Debug.Log("ClosePlatform" + _tag);
            this.GetComponent<Animator>().SetBool("CloseComplete", true);
            this.GetComponent<Animator>().SetBool("OpenComplete", false);
        }
        else
        {
            Debug.Log("ClosePlatform" + this.tag);
            this.GetComponent<Animator>().SetBool("Open", false);
            this.GetComponent<Animator>().SetBool("Close", true);
        } 
    }
    
    public IEnumerator OpenPlatformDelay(string _tag, float time)
    {
        if(this.tag == _tag)
        {
            //sound.PlayOneShot(sound.clip);
            sound.PlayDelayed(1f);
            Debug.Log("OpenPlatformDelay");
            this.GetComponent<Animator>().SetBool("Close", false);
            this.GetComponent<Animator>().SetBool("Open", true);
            yield return new WaitForSeconds(time);
        }

        yield return null;
        
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
