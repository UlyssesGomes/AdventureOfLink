using UnityEngine;
using UnityEngine.UI;

public class BuildingSkillUiSystem : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;

    private SlotBuilding[] slots;

    public int amountSkills;
    public int currentSkillIndex;

    public BuildingSkill[] currentBuildingSkills;

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
            slots[u].setContentSprite(null);
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
                    slots[u].setContentSprite(skills[u].image);
                else
                    slots[u].setContentSprite(null);
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
