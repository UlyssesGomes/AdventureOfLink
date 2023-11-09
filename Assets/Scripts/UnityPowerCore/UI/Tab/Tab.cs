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
    private Color unselectedColor;
    
    [SerializeField]
    private Image image;

    public Text text;

    private void Start()
    {
        // image.sprite = unselectedSprite;
    }

    public void select(bool doSelect)
    {
        if (doSelect)
            image.sprite = selectedSprite;
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
    //private void OnMouseDown()
    //{
    //    // Coloque aqui o código a ser executado quando o GameObject for clicado.
    //    Debug.Log("Clicou no GameObject: " + gameObject.name);
    //}
}
