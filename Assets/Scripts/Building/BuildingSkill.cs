using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildingSkill", menuName = "LocalGame/Building/new BuildingSkill")]
public class BuildingSkill : ScriptableObject
{
    public Sprite image;                    // skill sprite
    public string skillName;                // skill name
    [TextArea(3, 10)]
    public string description;              // skill description
    public bool isEnabled;                  // flag to control the use of the skill
    public BuildingSkillGroupType type;     // skill type

    public Material [] material;            // material required to craft with this skill
}
