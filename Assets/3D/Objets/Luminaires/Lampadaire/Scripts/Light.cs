using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public GameObject light;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive == true)
        {
            GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", Color.black);
            light.gameObject.SetActive(false);
            isActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isActive == false)
        {
            GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", Color.white);
            light.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
