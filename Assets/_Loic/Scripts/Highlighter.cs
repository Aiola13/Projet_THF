using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumicVR;
using System;

public class Highlighter : MonoBehaviour
{
    #region INSPECTOR PROPERTIES
        [Header("PROPERTIES")]
        public Color color;
    #endregion

    #region PRIVATE FIELDS
        [Header("DEBUG")]
        [SerializeField] private MeshRenderer _renderer;
       // [SerializeField] private PrinterStand printerStand;
        private Coroutine _delay;
    #endregion
    

    // Start is called before the first frame update
    void Start()
    {
        //printerStand = GetComponent<PrinterStand>();
        //printerStand.PrintEndedEvent += OnPrintingEnded;
        _renderer = GetComponent<MeshRenderer>();
        _delay = StartCoroutine(Flashing(_renderer.material));
    }

    IEnumerator Flashing(Material material)
    {
        while(true)
        {
            material.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            color.a = 1.0f;
            material.color = color;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
