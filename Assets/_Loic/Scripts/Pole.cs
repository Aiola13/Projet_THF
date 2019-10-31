using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;

    public void OpenPole()
    {

        Debug.Log("OpenPole");
        this.GetComponent<Animator>().SetBool("Out", false);
        this.GetComponent<Animator>().SetBool("In", true);
    }

    public void ClosePole()
    {
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
