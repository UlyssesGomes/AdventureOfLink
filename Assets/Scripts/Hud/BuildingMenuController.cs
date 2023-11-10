using UnityEngine;

public class BuildingMenuController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool isOpened;

    private void Start()
    {
        animator.SetBool("isOpened", isOpened);
    }

    /// <summary>
    /// Change between opened and closed state of BuildingMenu.
    /// If its opened this method will close and open if its closed.
    /// </summary>
    public void openClose()
    {
        isOpened = !isOpened;
        animator.SetBool("isOpened", isOpened);
    }
}
