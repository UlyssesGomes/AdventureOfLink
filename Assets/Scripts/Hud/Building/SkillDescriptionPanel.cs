using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    private Text name;                              // name of the selected item
    [SerializeField]
    private Image skillImage;                       // sprite of the selected item
    [SerializeField]
    private Text description;                       // short description of the selected item

    [SerializeField]
    private SlotBuildingMaterial[] materialSlots;   // material array of that skill

    /// <summary>
    /// Display selected building skill to show its datails.
    /// </summary>
    /// <param name="skill">Skill to be shown</param>
    public void setDescriptionPanel(BuildingSkill skill)
    {
        name.text = skill.skillName;
        skillImage.sprite = skill.image;
        description.text = skill.description;

        for(int u = 0; u < materialSlots.Length; u++)
        {
            if(u < skill.material.Length)
            {
                materialSlots[u].setContentSprite(skill.material[u].image);
                materialSlots[u].setText(skill.material[u].amount, 0);
                materialSlots[u].gameObject.SetActive(true);
            }
            else
                materialSlots[u].gameObject.SetActive(false);
        }
    }
}
