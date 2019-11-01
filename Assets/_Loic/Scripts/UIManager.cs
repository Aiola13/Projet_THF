﻿using System;
using System.Collections;
using System.Collections.Generic;
//using System.DateTime.Now;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshPro textDisplay;
    public TextMeshPro hourDisplay;
    public Image image;
    public string[] sentences;
    public Sprite[] sprites;
    [SerializeField] private int index;
    public float typingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        index = 1;
        GameManager.instance.UpdateUIEvent += NextSentence;
    }



    IEnumerator Type()
    {
        GameManager.instance.UpdateUIEvent -= NextSentence;
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        GameManager.instance.UpdateUIEvent += NextSentence;
    }

    public void NextSentence(object o, EventArgs e)
    {
        if(index < sentences.Length - 1)
        {
            //index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            ShowImage();
        }
        else
        {
            textDisplay.text = "";    
        }
    }

    public void ShowImage()
    {
        image.sprite = sprites[index];
    }

    void Update()
    {
        hourDisplay.text = System.DateTime.UtcNow.ToString("dd MMMM yyyy\nHH:mm");
    }
}