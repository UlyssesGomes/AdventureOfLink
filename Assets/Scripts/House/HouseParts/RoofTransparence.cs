using UnityEngine;

public class RoofTransparence : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;          // roof sprite
    private AgentExecutor executor;         // executor to run chest bar agent

    // Start is called before the first frame update
    void Start()
    {
        executor = new AgentExecutor();
    }

    // Update is called once per frame
    void Update()
    {
        executor.update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            executor.addAgent(new RoofTransparentAgent(sprite));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if(!p.furniturePlacement.isActiveAndEnabled)
                executor.addAgent(new RoofOpaqueAgent(sprite));
        }
    }
}
