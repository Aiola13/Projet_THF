using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    public GameObject pole;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenPole(GameObject _pole)
    {
        Instantiate(prefab, new Vector3(0.172f, 0, -2.03f), Quaternion.identity);
        pole.GetComponent<Animator>().SetBool("In", true);
    }
}
