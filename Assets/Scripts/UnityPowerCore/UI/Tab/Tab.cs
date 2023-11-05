using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
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
}
