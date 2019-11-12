using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;
using System;

public class EventArgs<T> : EventArgs {

  public T Value { get; private set; }

  public EventArgs(T val) {
     Value = val;
  }

}

public class HeadSet : MonoBehaviour
{
    public delegate void MyEventEventHandler(object o, int i);
    public event MyEventEventHandler HitEvent;

    private bool hasRun = true;
    RaycastHit hit;
    public LayerMask layerMask;
    private bool QRCodeEverHited = false;
    public GameObject UI;

    public AudioSource audioSource;
    public bool once = true;
    

    void Awake()
    {
        layerMask = LayerMask.GetMask("Include Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(!QRCodeEverHited)
        if(this.GetComponent<VRTK_InteractableObject>().IsInSnapDropZone() && once)
        {
            once = false;
            UI.SetActive(true);
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            /*Debug.Log("Did Hit" + hit.collider.name);
            GameManager.instance.QRCodeHited = true;
            QRCodeEverHited = true;*/
            var variable = GetRaycastHit();
            if(variable == 7)
            {
                audioSource.Play();
                GameManager.instance.QRCodeHited = true;
                QRCodeEverHited = true;
            }
            else
                HitEvent?.Invoke(this, variable);

        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            //GameManager.instance.QRCodeHited = false;
        }

    }


    private int GetRaycastHit ()
    {
        switch ( hit.collider.tag ) 
        {
            case "Pole":
                return 1;

            case "Arm":
                return 2;

            case "Printer":
                return 3;

            case "Helicopere":
                return 4;

            case "Data":
                return 5;

            case "Box":
                return 6;

            case "QR":
                return 7;

            default:
                //Debug.LogError ( hit.collider.tag.ToString () + " doe not exists !" );
                return 0;
        }
    }
}
