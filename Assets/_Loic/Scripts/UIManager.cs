using System;
using System.Collections;
using System.Collections.Generic;
//using System.DateTime.Now;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI hourDisplay;
    public TextMeshProUGUI timerDisplay;
    public Image image;
    public Image logo;
    public string[] sentencesMain;
    public string[] sentences;
    public Sprite[] sprites;
    public Sprite[] logos;
    [SerializeField] private int index;
    public float typingSpeed;
    private float gameTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        GameManager.instance.UpdateUIEvent += NextSentence;
    }



    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence(object o, EventArgs e)
    {
        GameManager.instance.UpdateUIEvent -= NextSentence;
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            ShowImage();
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";    
        }
        GameManager.instance.UpdateUIEvent += NextSentence;
    }

    public void ShowImage()
    {
        image.sprite = sprites[index];
        logo.sprite = logos[index];
    }

    public void Timer()
    {
        gameTimer += Time.deltaTime;

        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;

        timerDisplay.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        hourDisplay.text = System.DateTime.UtcNow.ToString("dd MMMM yyyy  HH:mm");
        Timer();        
    }
}
