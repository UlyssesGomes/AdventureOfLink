using UnityEngine;

public class FurniturePlaceDetect : MonoBehaviour
{
    [SerializeField]
    private FurniturePlacement furniture;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        furniture.enablePlace(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        furniture.enablePlace(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        furniture.enablePlace(false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        furniture.enablePlace(true);
    }
}
