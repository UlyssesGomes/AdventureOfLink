using UnityEngine;
using System.Collections.Generic;

public class PlayerBuildingSkills : MonoBehaviour
{
    public BuildingSkill[] housesSkills;                        // houses skills array
    public BuildingSkill[] refinedSkills;                       // refined items skills array
    public BuildingSkill[] toolsSkills;                         // tools skills array

    private Dictionary<int, BuildingSkill> skillDictionary;     // hash table to find any skill by its itemId

    private AssetFactory assetFactory;

    // Start is called before the first frame update
    void Start()
    {
        assetFactory = AssetFactory.getInstance();
        skillDictionary = new Dictionary<int, BuildingSkill>();
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
            housesSkills[u].fillMaterialAmount();
            skillDictionary.Add((int)housesSkills[u].itemId, housesSkills[u]);
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
            refinedSkills[u].fillMaterialAmount();
            skillDictionary.Add((int)refinedSkills[u].itemId, refinedSkills[u]);
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
            toolsSkills[u].fillMaterialAmount();
            skillDictionary.Add((int)toolsSkills[u].itemId, toolsSkills[u]);
        }
    }

    public List<GameItem> getAllMaterialToGenerateAnItem(int itemId)
    {
        BuildingSkill skill = skillDictionary[itemId];
        List<GameItem> materialItems = new List<GameItem>();

        for (int u = 0; u < skill.material.Length; u++)
        {
            int materialItemId = (int)skill.material[u].itemId;
            int amount = skill.materialAmount[u];
            GameItem item;
            do
            {
                item = assetFactory.instanceGameItemByItemId(materialItemId);
                if (amount <= item.total)
                {
                    item.amount = amount;
                    amount = 0;
                }
                else
                {
                    item.amount = item.total;
                    amount -= item.total;
                }

                if(item != null)
                    materialItems.Add(item);
                item = null;
            } while (amount > 0);
        }

        return materialItems;
    }
}
