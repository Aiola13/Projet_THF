using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    public GameObject Arm;
    public GameObject Base;
    public GameObject Platform;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().name)
        {
            Platform.GetComponent<Animator>().SetBool("Open", true);
            Arm.GetComponent<Animator>().SetBool("In", true);
        }
    }

    void LaunchScan()
    {
        if(Platform.tag == "PlatformBras")
            Arm.GetComponent<Animator>().SetBool("Scan", true);
    }
}
