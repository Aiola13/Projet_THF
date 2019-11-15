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
        private bool launch = true;
    #endregion

    #region EVENTS
        public event EventHandler HighLighterEndedEvent;
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
        BoxCollider col = this.gameObject.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = new Vector3(0.5f, 1.10f, 0.5f);

        while(true)
        {
            material.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            color.a = 1.0f;
            material.color = color;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hand" && launch)
        {
            HighLighterEndedEvent?.Invoke ( this, EventArgs.Empty );
            launch = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hand" && launch)
        {
            HighLighterEndedEvent?.Invoke ( this, EventArgs.Empty );
            launch = false;
        }
    }
}
