using UnityEngine;
using UnityEngine.UI;

public class BuildingSkillUiSystem : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;                       // building slot grid

    private SlotBuilding[] slots;                       // building slot array

    public int amountSkills;                            // amount of skills in selected tab
    public int currentSkillIndex;

    public BuildingSkill[] currentBuildingSkills;       // building skill array of the current selected tab

    // Awake is called before Start method
    private void Awake()
    {
        slots = grid.GetComponentsInChildren<SlotBuilding>();
    }

    // Start is called before the first frame update
    void Start()
    {
        amountSkills = 0;
        for (int u = 0; u < transform.childCount; u++)
        {
            slots[u].setContentBuildSkill(null);
        }

        currentSkillIndex = 0;
    }

    /// <summary>
    /// Set building skill sprite to a slot building image.
    /// </summary>
    /// <param name="skills"></param>
    public void setSkills(BuildingSkill[] skills)
    {
        if (skills != null)
        {
            for (int u = 0; u < slots.Length; u++)
            {
                if (u < skills.Length)
                    slots[u].setContentBuildSkill(skills[u]);
                else
                    slots[u].setContentBuildSkill(null);
            }
        }

        amountSkills = skills.Length;
        if(currentSkillIndex != 0)
        {
            slots[currentSkillIndex].setSelection(false);
        }
        currentSkillIndex = 0;
        slots[currentSkillIndex].setSelection(true);
        
        currentBuildingSkills = skills;
    }

    /// <summary>
    /// Moves the building slot selection according to the direction 
    /// passed by parameter.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>Return skill selected and null if any skill was selected.</returns>
    public BuildingSkill setSkillSelected(MoveOptionsEnum direction)
    {
        int previousIndex = -1;
        if(direction == MoveOptionsEnum.RIGHT)
        {
            if (currentSkillIndex + 1 < amountSkills)
            {
                previousIndex = currentSkillIndex++;
            }
        }
        else if(direction == MoveOptionsEnum.LEFT)
        {
            if (currentSkillIndex - 1 >= 0)
            {
                previousIndex = currentSkillIndex--;
            }
        }

        if(previousIndex != -1)
        {
            slots[previousIndex].setSelection(false);
            slots[currentSkillIndex].setSelection(true);
            return currentBuildingSkills[currentSkillIndex];
        }

        return null;
    }
}
