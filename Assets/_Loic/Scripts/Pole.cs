using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;
    [SerializeField] public AudioSource sound;
    private bool hasRun = false;

    public void OpenPole()
    {
        if(!hasRun)
        {
            sound.PlayOneShot(sound.clip);
            hasRun = true;
        }
        
        Debug.Log("OpenPole");
        this.GetComponent<Animator>().SetBool("Out", false);
        this.GetComponent<Animator>().SetBool("In", true);
    }

    public void ClosePole()
    {
        if(hasRun)
        {
            sound.PlayDelayed(1f);
            hasRun = false;
        }
        Debug.Log("ClosePole");
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Out", true);
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
