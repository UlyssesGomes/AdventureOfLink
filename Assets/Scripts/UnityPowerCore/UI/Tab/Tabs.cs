using UnityEngine;
using UnityEngine.UI;

public enum ChangeTabsOptionsEnum : int { PREVIOUS, NEXT };

public class Tabs : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup tabGrid;

    private Tab[] tabs;

    private int _selectedTabIndex;

    public int selectedTabIndex
    {
        get => _selectedTabIndex;
    }

    private void Awake()
    {
        tabs = tabGrid.GetComponentsInChildren<Tab>();
    }

    private void Start()
    {
        _selectedTabIndex = 0;

        tabs[0].select(true);
        tabs[1].select(false);
        tabs[2].select(false);
        
        tabs[0].text.text = "Casa";
        tabs[1].text.text = "Refinados";
        tabs[2].text.text = "Ferramentas";
    }

    public bool changeTab(ChangeTabsOptionsEnum direction)
    {
        if (direction == ChangeTabsOptionsEnum.PREVIOUS)
        {
            if (_selectedTabIndex > 0)
            {
                tabs[_selectedTabIndex].select(false);
                tabs[--_selectedTabIndex].select(true);
                return true;
            }
        }
        else if (direction == ChangeTabsOptionsEnum.NEXT)
        {
            if (_selectedTabIndex < tabs.Length - 1)
            {
                tabs[_selectedTabIndex].select(false);
                tabs[++_selectedTabIndex].select(true);
                return true;
            }
        }

        return false;
    }
}
