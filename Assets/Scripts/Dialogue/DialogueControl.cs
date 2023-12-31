﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]           // used to show in inspector
    public enum Idiom               // enum with 3 possible idiom 
    {
        PT, 
        EN,
        SP
    }
    public Idiom language;          // current selected language

    // Create a header with name "Componentes" in Unity Inspect Editor
    [Header("Componentes")]
    public GameObject dialogueObj;  // window dialogue
    public Image profileSprite;     // profile sprite
    public Text speechText;         // dialogue text
    public Text actorNameText;      // actor name text

    // Create a header with name "Settings" in Unity Inspect Editor
    [Header("Settings")]
    public float typingSpeed;       // speed dialogue text that will be shown

    private bool isShowing;         // window visibility flag
    private int index;              // current dialogue array index
    private string [] sentences;    // dialogues array

    public static DialogueControl instance;

    #region
    public bool IsShowing { get => isShowing; set => isShowing = value; }
    #endregion

    /*
     * Awake is called before every Start() in scripts hyerarchy execution.
     * This method is responsible to make this class Singleton.
     */
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isShowing = false;
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Skip to npc next sentence.
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else  // when finished texts
            {
                index = 0;
                speechText.text = "";
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    // Call npc dialogue
    public void Speech(string[] npcDialogues)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = npcDialogues;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
