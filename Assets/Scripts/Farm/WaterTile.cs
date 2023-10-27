using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FishingRod"))
        {
            Debug.Log("Pode pescar");
            Player p = collision.transform.GetComponentInParent<Player>();
            p.isFishing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FishingRod"))
        {
            Player p = collision.transform.GetComponentInParent<Player>();
            p.isFishing = false;
        }
    }
}
