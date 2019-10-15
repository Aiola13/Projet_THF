using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlatformManager : MonoBehaviour
{
    public GameObject Base;
    public Transform prefab;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Platform")
        {
            Debug.Log("Bonjour");
            gameObject.GetComponent<Animator>().SetBool("Open", true);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenBase()
    {
        Instantiate(prefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
        Base.GetComponent<Animator>().SetBool("In", true);
    }
}
