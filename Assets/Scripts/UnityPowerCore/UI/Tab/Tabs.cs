using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup tabGrid;

    private Tab[] tabs;

    private void Awake()
    {
        tabs = tabGrid.GetComponentsInChildren<Tab>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
