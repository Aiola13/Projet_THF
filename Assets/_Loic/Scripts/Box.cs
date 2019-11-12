using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{

    public event EventHandler WifiEvent;
    public string levelToLoad = "EndScene";
    public List<Light> lights = new List<Light>();
    public GameObject UIBox;
    
    public float waitingTime = 0.05f;
    public int counter = 0;
    public bool test = false;






    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float maxRange = 10.0f;
    public bool repeatRange = false;

    // Intensity Variables
    public bool changeIntensity = false;
    public float intensitySpeed = 1.0f;
    public float maxIntensity = 10.0f;
    public bool repeatIntensity = false;

    // Color variables
    public bool changeColors = false;
    public float colorSpeed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatColor = false;
    public AudioSource audioSource;

    [SerializeField] private float startTime;
    [SerializeField] private float gameTimer;
    

    public Image blackFade;
    private void Start()
    {
        startTime = Time.time;
    }

    private void OnCollisionEnter(Collision other) {
        audioSource.PlayOneShot(audioSource.clip);
        this.gameObject.tag = "Box";
        this.gameObject.layer = 9;
        StartCoroutine(DataSend());

    }

    IEnumerator DataSend()
    {

        WifiEvent?.Invoke(this, EventArgs.Empty);
        foreach(var lt in lights)
        {
            lt.gameObject.SetActive(true);
        }
        
        Debug.Log("Je suis dans DATA SEND !!!!!!!!!!!!!!!!!!!!");
        while(gameTimer <= 30f)
        {
           /*  foreach(var lt in lights)
            {
                lt.SetActive(test);
            }
            test = !test;
            */
            foreach(Light lt in lights)
            {
                if(changeRange) 
                {            
                    if(repeatRange) 
                    {
                        lt.range = Mathf.PingPong(Time.time * rangeSpeed, maxRange);   
                    } 
                    else 
                    {
                        lt.range = Time.time * rangeSpeed;
                        if(lt.range >= maxRange) 
                        {
                            changeRange = false;
                        }
                    }
                }

                if(changeIntensity) 
                {
                    if(repeatIntensity) 
                    {
                        lt.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);    
                    } 
                    else 
                    {
                        lt.intensity = Time.time * intensitySpeed;
                        if(lt.intensity >= maxIntensity) 
                        {
                            changeIntensity = false;
                        }
                    }
                }

                if(changeColors) 
                {
                    if(repeatColor) 
                    {
                        float t = (Mathf.Sin(Time.time - startTime * colorSpeed));
                        lt.color = Color.Lerp(startColor, endColor, t);   
                    } 
                    else 
                    {
                        float t = Time.time - startTime * colorSpeed;
                        lt.color = Color.Lerp(startColor, endColor, t);
                    }
                }
            }
            gameTimer += Time.deltaTime;
            yield return new WaitForSeconds(waitingTime);
        } 
              

        yield return new WaitForSeconds(1.0f);

    
        Color c = blackFade.color;
        c.a = Mathf.Lerp( 0f, 1f, 100.0f * Time.deltaTime);
        blackFade.color = c;
        Debug.Log("Je modifie la transparence !!!!!!");

        SceneManager.LoadScene(levelToLoad);

    }
}
