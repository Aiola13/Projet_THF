using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float rotateSpeed = 1.2f;
    public Material skybox;

    // Update is called once per frame
    void Update()
    {
        skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
