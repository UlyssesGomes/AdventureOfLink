using UnityEngine;

public class PlayerBuilldingSkills : MonoBehaviour
{
    public BuildingSkill[] housesSkills;
    public BuildingSkill[] refinedSkills;
    public BuildingSkill[] toolsSkills;

    // Start is called before the first frame update
    void Start()
    {
        loadHouseSkills();
        loadRefinedSkills();
        loadToolsSkills();
    }

    /// <summary>
    /// Load all skills from House folder and instantiate into houseSkills[].
    /// </summary>
    private void loadHouseSkills()
    {
        Object[] objSkills = Resources.LoadAll("ScriptableObjects/Building/Skills/Houses", typeof(BuildingSkill));
        housesSkills = new BuildingSkill[objSkills.Length];

        for (int u = 0; u < objSkills.Length; u++)
        {
            housesSkills[u] = (BuildingSkill)Instantiate(objSkills[u]);
        }
    }

    /// <summary>
    /// Load all skills from House folder and instantiate into houseSkills[].
    /// </summary>
    private void loadRefinedSkills()
    {
        Object[] objSkills = Resources.LoadAll("ScriptableObjects/Building/Skills/Refined", typeof(BuildingSkill));
        refinedSkills = new BuildingSkill[objSkills.Length];

        for (int u = 0; u < objSkills.Length; u++)
        {
            refinedSkills[u] = (BuildingSkill)Instantiate(objSkills[u]);
        }
    }

    /// <summary>
    /// Load all skills from House folder and instantiate into houseSkills[].
    /// </summary>
    private void loadToolsSkills()
    {
        Object[] objSkills = Resources.LoadAll("ScriptableObjects/Building/Skills/Tools", typeof(BuildingSkill));
        toolsSkills = new BuildingSkill[objSkills.Length];

        for (int u = 0; u < objSkills.Length; u++)
        {
            toolsSkills[u] = (BuildingSkill)Instantiate(objSkills[u]);
        }
    }
}
