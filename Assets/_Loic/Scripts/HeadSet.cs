using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadSet : MonoBehaviour
{
    private bool hasRun = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(hasRun && gameObject.tag == GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().tag)
        {
            Debug.Log(GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().name + "Snapped");
            hasRun = false;
        }*/


        if(Input.GetKey(KeyCode.Space) && !GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject())
        {
            hasRun = false;
            this.gameObject.GetComponent<VRTK_SnapDropZone>().ForceSnap(GameManager.instance.headSetInstance);
            Debug.Log("headSet Snapped");
            GameManager.instance.headSetSnapped = true;
        }
    }
}
