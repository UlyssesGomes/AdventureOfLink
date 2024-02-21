using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class BuildingBlockedArea : MonoBehaviour
{
    [SerializeField]
    private Color areaBlockColor;               // color to be shown when house is placed in a forbidden place (its in collision with something)
    [SerializeField]
    private float maxConstructionAmount;        // maximum value of hits that the area will receive for the house to be built
    private float constructionAmount;           // current hit value that this house received

    [SerializeField]
    private Image blueBar;                      // bar to represent current amount of building hits this house has received
    [SerializeField]
    private GameObject blueBarObject;           // blue bar game object instance 

    [SerializeField]
    private SpriteRenderer baseHouseSprite;     // base house sprite renderer of this house

    [SerializeField]
    private BoxCollider2D interactArea;         // area to interact when house is in blocked construction mode

    private UnityEvent buildEvent;              // subject that emit event when house become ready after construction

    private AgentExecutor executor;             // agent executor to run agents

    private bool _isBlocked;                    // flag to control when house construction is blocked (house is in collision with something)

    public bool isBlocked
    {
        get { return _isBlocked; }
    }

    private void Awake()
    {
        buildEvent = new UnityEvent();
        executor = new AgentExecutor();
        
        _isBlocked = false;
        interactArea.enabled = false;
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
        if (collision.gameObject.CompareTag("BuilderHammer") && !_isBlocked)
        {
            updateFillBar(1f);
            executor.addAgent(new HouseBuildBarAgent(blueBarObject));
        }
        else if(collision.gameObject.CompareTag("BuilderHammer") && _isBlocked)
        {
            // TODO - when toast system were implemented, use here to notify the player.
            Debug.LogWarning("This house need to be moved before continue with construction.");
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

    /// <summary>
    /// Disable house construction and show house base in red color.
    /// </summary>
    public void ativateInvalidPlace()
    {
        _isBlocked = true;
        baseHouseSprite.color = areaBlockColor;
        interactArea.enabled = true;
    }
}
