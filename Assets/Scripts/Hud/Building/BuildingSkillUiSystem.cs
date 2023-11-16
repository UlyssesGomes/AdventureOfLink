using UnityEngine;
using UnityEngine.UI;

public class BuildingSkillUiSystem : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;

    private SlotBuilding[] slots;

    private void Awake()
    {
        slots = grid.GetComponentsInChildren<SlotBuilding>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int u = 0; u < transform.childCount; u++)
        {
            slots[u].setContentSprite(null);
        }
    }


    /// <summary>
    /// Set building skill sprite to a slot building image.
    /// </summary>
    /// <param name="skills"></param>
    public void setSkills(BuildingSkill[] skills)
    {
        if(skills != null)
        {
            for(int u = 0; u < slots.Length; u++)
            {
                if (u < skills.Length)
                    slots[u].setContentSprite(skills[u].image);
                else
                    slots[u].setContentSprite(null);
            }
        }
    }
}
