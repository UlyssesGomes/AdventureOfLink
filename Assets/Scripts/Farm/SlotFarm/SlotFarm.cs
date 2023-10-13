using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;      // sprite to show current slote state
    [SerializeField]
    private Sprite hole;                        // hole sprite
    [SerializeField]            
    private Sprite carrot;                      // carrot sprite 

    [Header("Components")]
    private int digAmount;                      // amount of hits a players need to dig util the hole appears
    [SerializeField]
    private int maxDigAmount;                   // amount of hit the slot farm have when it is full
    public bool detectWater;                    // when true, player is watering this hole.

    private float currentRespownTime;           // if slotfarm have no digAmount, start timer by add elapsedTime each frame
    private const float RESPOWN_TIME = 10.0f;   // when currentRespownTime reach this amount, the hole must be close

    // Start is called before the first frame update
    void Start()
    {
        digAmount = maxDigAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (digAmount <= 0)
        {
            currentRespownTime += Time.deltaTime;
            if (currentRespownTime >= RESPOWN_TIME)
            {
                digAmount = maxDigAmount;
                spriteRenderer.sprite = null;
            }
        }
    }

    public void onHit()
    {
        if(digAmount > 0)
        {
            digAmount--;

            if(digAmount <= 0)
            {
                spriteRenderer.sprite = hole;
                currentRespownTime = 0.0f;
            }
            // TODO - implement grow system to harvest fuits and vegetables.
        }
    }

    /// <summary>
    /// Check collision with shovel and water to call watering and digging actions.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
        {
            onHit();
        }

        if (collision.CompareTag("WaterCan"))
        {
            detectWater = true;
        }
    }


    /// <summary>
    /// When player its no more watering, stop watering this hole.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
            detectWater = false;
    }
}
