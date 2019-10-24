using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OpenPlatform(string _tag)
    {
        if(this.tag == _tag)
        {
            Debug.Log("OpenPlatform");
            this.GetComponent<Animator>().SetBool("Close", false);
            this.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    void ClosePlatform(string _tag)
    {
        if(this.tag == _tag)
        {
            Debug.Log("ClosePlatform");
            this.GetComponent<Animator>().SetBool("Open", false);
            this.GetComponent<Animator>().SetBool("Close", true);
        }
    }
}
