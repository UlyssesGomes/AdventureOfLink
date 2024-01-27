using UnityEngine;

public class FurniturePlaceDetect : MonoBehaviour
{
    [SerializeField]
    private FurniturePlacement furniture;

    private int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
        if(count > 0)
            furniture.enablePlace(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        count--;
        if(count == 0)
            furniture.enablePlace(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count++;
        if (count > 0)
            furniture.enablePlace(false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        count--;
        if (count == 0)
            furniture.enablePlace(true);
    }
}
