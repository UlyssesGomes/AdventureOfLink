using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackInventory : MonoBehaviour
{
    private bool isVisible;

    private void Start()
    {
        isVisible = false;
        gameObject.SetActive(isVisible);
    }

    /*
     * Call this method to switch between active and inactive and vice versa.
     */
    public void changeVisibility()
    {
        isVisible = !isVisible;
        gameObject.SetActive(isVisible);
    }
}
