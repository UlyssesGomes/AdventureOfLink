using UnityEngine;

public class PlayerGizmosGuide : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gizmoSprite;
    [SerializeField]
    private Color whiteColor;
    [SerializeField]
    private Color redColor;

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
        if(gizmoSprite.color != whiteColor)
            gizmoSprite.color = whiteColor;

        Debug.Log("white called.");
    }

    /// <summary>
    /// Change gizmo color to red
    /// </summary>
    public void changeToRed()
    {
        if(gizmoSprite.color != redColor)
            gizmoSprite.color = redColor;

        Debug.Log("red called.");
    }
}
