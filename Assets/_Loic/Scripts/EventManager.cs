using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent EventAnimationEnd;

    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertEvents(string message)
    {
        if (message.Equals("AnimationOver"))
        {
            Debug.Log("Animation Over");
        } 
    }
}
