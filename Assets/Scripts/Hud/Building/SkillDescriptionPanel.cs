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
    private Button buildButton;                     // button to build selected skill

    [SerializeField]
    private SlotBuildingMaterial[] materialSlots;   // material array of that skill

    private BuildingSkill _currentBuildingSkill;    // intance of current selected BuildingSkill
    public BuildingSkill currentBuildingSkill { get { return _currentBuildingSkill; } }

    /// <summary>
    /// Display selected building skill to show its datails.
    /// </summary>
    /// <param name="skill">Skill to be shown</param>
    public void setDescriptionPanel(BuildingSkill skill, int []playerItemAmount)
    {
        _currentBuildingSkill = skill;
        name.text = skill.skillName;
        skillImage.sprite = skill.image;
        skillImage.preserveAspect = true;
        description.text = skill.description;
        bool isButtonEnable = true;
        for(int u = 0; u < materialSlots.Length; u++)
        {
            if(u < skill.material.Length)
            {
                materialSlots[u].setContentSprite(skill.material[u].image);
                materialSlots[u].setText(skill.material[u].amount, playerItemAmount[u]);
                materialSlots[u].gameObject.SetActive(true);

                if (skill.material[u].amount > playerItemAmount[u])
                    isButtonEnable = false;
            }
            else
                materialSlots[u].gameObject.SetActive(false);
        }

        buildButton.interactable = isButtonEnable;
    }

    /// <summary>
    /// Enable and disable building skill creation button.
    /// </summary>
    /// <param name="isEnable">Param to passa enable value</param>
    public void setEnableCreationButton(bool isEnable)
    {
        buildButton.interactable = isEnable;
    }
}
