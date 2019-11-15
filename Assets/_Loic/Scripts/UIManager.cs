using System;
using System.Collections;
using System.Collections.Generic;
//using System.DateTime.Now;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public event EventHandler EndingEvent;
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI hourDisplay;
    public TextMeshProUGUI timerDisplay;
    public Image image;
    public Image logo;
    public Image wifi;
    public string[] sentences;
    public Sprite[] sprites;
    public Sprite[] logos;
    [SerializeField] private int index;
    public float typingSpeed;
    [SerializeField] public float gameTimer = 0f;
    private Coroutine delay;
    public GameObject box;

    public AudioSource audioSource;

    public bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 250f;
        index = 0;
        //GameManager.instance.UpdateUIEvent += NextSentence;
        this.GetComponentInParent<HeadSet>().HitEvent += NextSentence;
        box = GameManager.instance.boxPrefab;
        box.GetComponent<Box>().WifiEvent += LaunchWifiIcon;
    }



    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void NextSentence(object o, int i)
    {
        //GameManager.instance.UpdateUIEvent -= NextSentence;
       /*if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            ShowImage();
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";    
        }*/
        //GameManager.instance.UpdateUIEvent += NextSentence;
        if(i > 0 && i < 8)
        {
            //StopCoroutine(delay);
            index = 0;
            textDisplay.text = "";
            index = i;
            ShowImage();
            textDisplay.text = sentences[i];
            //delay = StartCoroutine(Type());
            //StopCoroutine(delay);
        }
        else
        {
            
            //StopCoroutine(delay);
            index = 0;
            textDisplay.text = "";    
        }
            

        
    }

    public void ShowImage()
    {
        image.sprite = sprites[index];
        logo.sprite = logos[index];
    }

    public void Timer()
    {
        //gameTimer += Time.deltaTime;
        gameTimer -= Time.deltaTime;

        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;

        timerDisplay.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        hourDisplay.text = System.DateTime.Now.ToString("dd MMMM yyyy  HH:mm");
        Timer();     

        if(gameTimer <= 0)
        {
            if(once)
            {
                audioSource.Play();
                once = false;
            }
            gameTimer = 0;
            StartCoroutine(AlertEnding());
        }
        /*if(gameTimer >= 360f)
        {
            EndingEvent();
        }*/
    }

    
    public void LaunchWifiIcon(object o,EventArgs e)
    {
        wifi.gameObject.SetActive(true);
    }

    public void EndingEvent()
    {

    }

    IEnumerator AlertEnding()
    {
        timerDisplay.enabled = !timerDisplay.enabled;
        yield return new WaitForSeconds(1f);
    }

}
