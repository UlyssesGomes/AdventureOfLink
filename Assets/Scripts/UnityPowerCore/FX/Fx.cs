using UnityEngine;

public abstract class Fx : MonoBehaviour
{
    /// <summary>
    /// Implement in derivated class to return type that represents this effect.
    /// </summary>
    /// <returns>type of this effect</returns>
    public abstract int getFxType();
}
