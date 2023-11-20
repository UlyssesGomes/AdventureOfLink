using UnityEngine;

using System;

[CreateAssetMenu(fileName = "BuildingSkill", menuName = "LocalGame/Building/new BuildingSkill")]
public class BuildingSkill : ScriptableObject
{
    [HideInInspector]
    public int id;                          // unique id that identifies this instance
    public BuildingSkillEnum skillId;       // id that identifies that type of skill
    public Sprite image;                    // skill sprite
    public string skillName;                // skill name
    [TextArea(3, 10)]
    public string description;              // skill description
    
    public bool isEnabled;                  // flag to control the use of the skill
    public bool hasAllMaterials;            // control to enable build button when player has all the materials in the inventory     
   
    public BuildingSkillGroupType type;     // skill type

    public Material [] material;            // material required to craft with this skill
    public int[] materialAmount;            // amount of material linked to material array by the index

    private static int nextId;              // stores the next unique and valid id for the next object

    private void Awake()
    {
        id = getNextUniqueId();
    }

    /// <summary>
    /// Fill amount of this material instance. This method must be called
    /// only in a BuildingSkill intance, never in prefab.
    /// </summary>
    public void fillMaterialAmount()
    {
        if(material.Length != materialAmount.Length)
        {
            throw new Exception("[BuildingSkill.fillMAterialAmount()] - material.Length and materialAmount.Length must be the same size.");
        }

        for(int u = 0; u < material.Length; u++)
        {
            material[u] = Instantiate(material[u]);
            material[u].amount = materialAmount[u];
        }
    }

    /// <summary>
    /// Generate unique skill id.
    /// </summary>
    /// <returns>Unique int id</returns>
    public int getNextUniqueId()
    {
        return nextId++;
    }
}
