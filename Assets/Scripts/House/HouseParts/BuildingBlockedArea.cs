using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class BuildingBlockedArea : MonoBehaviour
{
    [SerializeField]
    private float maxConstructionAmount;        // maximum value of hits that the area will receive for the house to be built
    private float constructionAmount;           // current hit value that this house received

    [SerializeField]
    private Image blueBar;                      // bar to represent current amount of building hits this house has received
    [SerializeField]
    private GameObject blueBarObject;           // blue bar game object instance 

    private UnityEvent buildEvent;              // subject that emit event when house become ready after construction

    private AgentExecutor executor;             // agent executor to run agents

    private void Awake()
    {
        buildEvent = new UnityEvent();
        executor = new AgentExecutor();
    }

    private void Start()
    {
        constructionAmount = 0f;
        blueBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        executor.update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BuilderHammer"))
        {
            updateFillBar(2f);
            executor.addAgent(new HouseBuildBarAgent(blueBarObject));
        }
    }

    /// <summary>
    /// Add callback to be invoked when house is built
    /// </summary>
    /// <param name="call">callback to be called</param>
    public void addListener(UnityAction call)
    {
        buildEvent.AddListener(call);
    }

    /// <summary>
    /// Update construictionAmount and blueBar with new value.
    /// </summary>
    /// <param name="value">value to be added after hit</param>
    private void updateFillBar(float value)
    {
        constructionAmount += value;
        blueBar.fillAmount = constructionAmount / maxConstructionAmount;

        if (constructionAmount >= maxConstructionAmount)
            buildEvent.Invoke();
    }
}
