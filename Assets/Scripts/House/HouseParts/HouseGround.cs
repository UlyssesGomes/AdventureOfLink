using UnityEngine;
using UnityEngine.Events;

public class HouseGround : MonoBehaviour
{
    private UnityEvent<int, PlayerInventory> unityEvents;        // subject to send to observer when ground is hited by axe
    
    private void Awake()
    {
        unityEvents = new UnityEvent<int, PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
        {
            unityEvents.Invoke(1, null);
        }
        else if(collision.CompareTag("BuilderHammer"))
        {
            PlayerInventory playerInventory = collision.gameObject.GetComponentInParent<PlayerInventory>();
            if(playerInventory.countItemAmountByItemId(ObjectIdEnum.PLANK) >= 1)
                unityEvents.Invoke(-1, playerInventory);
        }
    }

    public void addListeners(UnityAction<int, PlayerInventory> callback)
    {
        unityEvents.AddListener(callback);
    }
}
