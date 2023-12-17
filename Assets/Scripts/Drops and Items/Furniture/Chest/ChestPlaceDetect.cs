using UnityEngine;

public class ChestPlaceDetect : MonoBehaviour
{
    [SerializeField]
    private Chest chest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        chest.enablePlace(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chest.enablePlace(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        chest.enablePlace(false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        chest.enablePlace(true);
    }
}
