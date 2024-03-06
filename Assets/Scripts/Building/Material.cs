using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Material", menuName = "LocalGame/Building/new Material")]
public class Material : ScriptableObject
{
    public ObjectIdEnum itemId;       // itemId of this item
    public Sprite image;            // material sprite
    public string materialName;     // material name
    public int amount;              // required amount material
}
