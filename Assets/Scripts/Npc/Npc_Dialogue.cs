using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called by Unity Physics System
    void FixedUpdate()
    {
        ShowDialogue();
    }

    /*
     * Create a hitbox to detect player collision
     */
    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null)
        {
            Debug.Log("Player na área de dialogo...");
        }
        else
        {
            Debug.Log("Outro tipo de colisão");
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
