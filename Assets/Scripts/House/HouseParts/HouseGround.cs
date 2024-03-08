using UnityEngine;
using UnityEngine.Events;

public class HouseGround : MonoBehaviour
{
    private UnityEvent<int> unityEvents;

    private void Awake()
    {
        unityEvents = new UnityEvent<int>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
        {
            unityEvents.Invoke(1);
        }
    }

    public void addListeners(UnityAction<int> callback)
    {
        unityEvents.AddListener(callback);
    }
}
