using UnityEngine;

public class PlayerGizmosGuide : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gizmoSprite;
    [SerializeField]
    private Color whiteColor;
    [SerializeField]
    private Color redColor;

    // Start is called before the first frame update
    void Start()
    {
        whiteColor = new Color(1, 1, 1, 0.8f);
        redColor = new Color(0.741f, 0.031f, 0.031f, 1.0f);
    }

    /// <summary>
    /// Set enable to show and hide player gizmos guide.
    /// </summary>
    /// <param name="enable">enable gizmo</param>
    public void setEnable(bool enable)
    {
        transform.gameObject.SetActive(enable);
    }

    /// <summary>
    /// Change gizmo color to white
    /// </summary>
    public void changeToWhite()
    {
        gizmoSprite.color = whiteColor;
        Debug.Log("Mudou para branco.");
    }

    /// <summary>
    /// Change gizmo color to red
    /// </summary>
    public void changeToRed()
    {
        gizmoSprite.color = redColor;
        Debug.Log("Mudou para vermelho.");
    }
}
