using UnityEngine;

public class DoingBar : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private void OnEnable()
    {
        if (player.transform.rotation.eulerAngles.y != 0f || transform.rotation.eulerAngles.y != 0f)
        {
            Quaternion quaternion = Quaternion.Euler(player.transform.rotation.eulerAngles.x, 0f, player.transform.rotation.eulerAngles.z);
            transform.rotation = quaternion;
        }
    }
}
