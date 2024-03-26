using UnityEngine;
using System.Collections.Generic;

public class PlayerInteractionProgress : MonoBehaviour
{
    private IDictionary<int, int> npcsProgressionDictionary;        // dictionary to associate npc progression

    private void Awake()
    {
        npcsProgressionDictionary = new Dictionary<int, int>();
    }

    /// <summary>
    /// Update interaction value associated with npcId.
    /// </summary>
    /// <param name="npcId">npc id</param>
    /// <param name="interactionValue">interaction value to be associated</param>
    public void updateNpcInteraction(int npcId, int interactionValue)
    {
        if (npcsProgressionDictionary.ContainsKey(npcId))
            npcsProgressionDictionary[npcId] = interactionValue;
        else
            npcsProgressionDictionary.Add(npcId, interactionValue);

    }

    /// <summary>
    /// Get interaction value associated with npc id received by param.
    /// </summary>
    /// <param name="npcId">npc id</param>
    /// <returns>interaction value</returns>
    public int getNpcInteractionProgress(int npcId)
    {
        if (npcsProgressionDictionary.ContainsKey(npcId))
            return npcsProgressionDictionary[npcId];

        return -1;
    }
}
