using UnityEngine;
using UnityEngine.UI;

using System;

public enum ChangeTabsOptionsEnum : int { PREVIOUS, NEXT };

public class Tabs : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup tabGrid;        // grid layout to position tabs

    private Tab[] tabs;                     // all tabs instance

    private int _selectedTabIndex;          // current selected tab

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
        for(int u = 1; u < tabs.Length; u++)
        {
            tabs[u].select(false);
        }
    }

    /// <summary>
    /// Change tab by direction, moving to nearby tab on left or right side.
    /// </summary>
    /// <param name="direction">Direction to the next tab</param>
    /// <returns>Return true if tab was changed.</returns>
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

    /// <summary>
    /// Set all tab labels.
    /// </summary>
    /// <param name="labels"></param>
    public void setLabelsTabs(string [] labels)
    {
        if (tabs.Length == labels.Length)
        {
            for (int u = 0; u < labels.Length; u++)
            {
                tabs[u].text.text = labels[u];
            }
        }
        else
            throw new Exception("[Tabs.setLabelsTabs] - diferents sizes in labels and tabs arrays.");
    }
}
