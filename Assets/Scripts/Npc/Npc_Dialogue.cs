﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    bool isDialoguing;

    public DialogueSettings dialogue;

    private List<string> sentences = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GetNpcTexts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isDialoguing)
        {
            DialogueControl.instance.Speech(sentences.ToArray());
        }
    }

    // Called by Unity Physics System
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetNpcTexts()
    {
        for(int u = 0; u < dialogue.dialogues.Count; u++)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.Idiom.PT:
                    sentences.Add(dialogue.dialogues[u].senteces.portuguese);
                    break;
                case DialogueControl.Idiom.EN:
                    sentences.Add(dialogue.dialogues[u].senteces.english);
                    break;
                case DialogueControl.Idiom.SP:
                    sentences.Add(dialogue.dialogues[u].senteces.spanish);
                    break;
                default:
                    sentences.Add(dialogue.dialogues[u].senteces.portuguese);
                    break;
            }
        }
    }

    /*
     * Create a hitbox to detect player collision
     */
    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null)
        {
            isDialoguing = true;
        }
        else
        {
            isDialoguing = false;
            // verificar se o DialogueControl vai encerrar as falas.
            //DialogueControl.instance.dialogueObj.SetActive(false);
        }
    }

    /*
     * Show in Unity Scene a hitbox range when
     * Gizmos is activated.
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
