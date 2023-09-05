using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
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
    private int index;              // dialogue array index
    private string [] sentences;    // 

    public static DialogueControl instance;

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
