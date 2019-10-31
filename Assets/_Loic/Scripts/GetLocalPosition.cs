using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocalPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GetLocalPosition");
        var render = GetComponentsInChildren<Renderer>();
        Debug.Log(render.Length);
        
        for(int i = 0; i > render.Length; i++)
        {
            Debug.Log(render[i].bounds.center);
        }
    }
}
