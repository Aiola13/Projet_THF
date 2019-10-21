using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().name)
        {
            /*platform.GetComponent<Animator>().SetBool("Open", true);
            arm.GetComponent<Animator>().SetBool("In", true);*/
            Debug.Log("Coucou");
        }
    }
}
