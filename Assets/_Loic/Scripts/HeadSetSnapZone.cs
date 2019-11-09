using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadSetSnapZone : MonoBehaviour
{
    private bool hasRun = true;
    private bool hasKeyPressed = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && !GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject())
        {
            hasRun = false;
            this.gameObject.GetComponent<VRTK_SnapDropZone>().ForceSnap(GameManager.instance.headSetInstance);
            Debug.Log("headSet Snapped");
            GameManager.instance.headSetSnapped = true;
        }

        if(GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject() && !hasKeyPressed)
        {
            hasRun = false;
            hasKeyPressed = true;
            Debug.Log("headSet Snapped");
            GameManager.instance.headSetSnapped = true;
        }
    }
}
