using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Platform")
        {
            Debug.Log("Bonjour");
            gameObject.GetComponent<Animator>().SetBool("Rise", true);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
