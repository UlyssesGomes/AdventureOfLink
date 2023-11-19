using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private Text description;

    [SerializeField]
    private SlotBuildingMaterial[] materialSlots;

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
