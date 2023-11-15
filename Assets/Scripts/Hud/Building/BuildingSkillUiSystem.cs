using UnityEngine;
using UnityEngine.UI;

public class BuildingSkillUiSystem : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            grid.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
