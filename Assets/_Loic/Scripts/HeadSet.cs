using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class HeadSet : MonoBehaviour
{
    public UnityEvent MyEvent;
    private bool hasRun = true;
    RaycastHit hit;
    public LayerMask layerMask;
    private bool QRCodeEverHited = false;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Include Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        if(!QRCodeEverHited)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit" + hit.collider.name);
                GameManager.instance.QRCodeHited = true;
                QRCodeEverHited = true;
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
                //GameManager.instance.QRCodeHited = false;
            }
        } 
    }
}
