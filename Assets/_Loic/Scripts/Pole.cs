using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPole()
    {
        Debug.Log("OpenPole");
        Instantiate(prefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
        this.GetComponent<Animator>().SetBool("Out", false);
        this.GetComponent<Animator>().SetBool("In", true);
    }

    void ClosePole()
    {
        Debug.Log("ClosePole");
        this.GetComponent<Animator>().SetBool("In", false);
        this.GetComponent<Animator>().SetBool("Out", true);
    }
}
