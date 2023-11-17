using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField]
    private Sprite unselectedSprite;

    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color unselectedColor;
    
    [SerializeField]
    private Image image;

    public Text text;

    private void Start()
    {
        // image.sprite = unselectedSprite;
    }

    /// <summary>
    /// Turns Tab selected or not by passing a boolean param.
    /// If true, turn Tab selected and its color become brightest color.
    /// </summary>
    /// <param name="doSelect"></param>
    public void select(bool doSelect)
    {
        if (doSelect)
        {
            image.sprite = selectedSprite;
            image.color = selectedColor;
        }
        else
        {
            image.sprite = unselectedSprite;
            image.color = unselectedColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicou no GameObject: " + gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entrou no GameObject: " + gameObject.name);
    }
}
