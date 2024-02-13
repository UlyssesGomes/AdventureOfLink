using UnityEngine;

public interface Door
{
    /// <summary>
    /// Method to open or close door according to the player's access. 
    /// It receives the player and if allowed, door state is changed.
    /// </summary>
    /// <param name="player">player who try to open this door</param> 
    void openDoor(Player player);
}
