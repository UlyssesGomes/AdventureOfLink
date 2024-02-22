using UnityEngine;

public class HouseController : House<BuildingItemIdEnum>
{
    [Header("House Settings")]
    [SerializeField]
    private bool isBuilt;                               // flag that represents whether the house is already built or not
    [SerializeField]
    public bool isPositionedCorrecty;                   // flag to tell if this house is placed correctly

    [Header("House Building Component")]
    [SerializeField]
    private BuildingBlockedArea buildingBlockedArea;    // instance to control when house is in building mode

    [Header("House Objectes")]
    [SerializeField]
    private GameObject roof;                            // roof object of this house
    [SerializeField]
    private GameObject walls;                           // walls object of this house
    [SerializeField]
    private GameObject ground;                          // ground object of this house
    [SerializeField]
    private GameObject door;                            // door object of this house
    [SerializeField]
    private GameObject constructionFance;               // construction object collection of this house

    // Start is called before the first frame update
    void Start()
    {
        construictionMode();
        if(!isBuilt)
            buildingBlockedArea.addListener(builtHouse);

        if (!isPositionedCorrecty)
            buildingBlockedArea.ativateInvalidPlace();
    }

    /// <summary>
    /// When isBuilt is false, enable construction block area and disble houses object part, 
    /// and when isBuilt is true, the situation reverses.
    /// </summary>
    private void construictionMode()
    {
        constructionFance.SetActive(!isBuilt);

        roof.SetActive(isBuilt);
        walls.SetActive(isBuilt);
        ground.SetActive(isBuilt);
        door.SetActive(isBuilt);
    }

    /// <summary>
    /// Event to be called when house end construction mode.
    /// </summary>
    public void builtHouse()
    {
        isBuilt = true;
        construictionMode();
        GameObject puff = AssetFactory.getInstance().instanceFxGameObjectByType((int)FxEnum.PUFF_SMOKE);
        puff.transform.position = transform.position;
        puff.transform.localScale = new Vector3(4f, 4f, 4f);
    }

    // TODO - This method can be removed later.
    void OnDestroy()
    {
        // Este código será executado quando o MonoBehaviour for destruído
        Debug.Log("O MonoBehaviour de id " + gameObject.GetInstanceID() + "está sendo destruído!");
    }
}
