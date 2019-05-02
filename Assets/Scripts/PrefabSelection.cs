using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSelection : MonoBehaviour
{
    public Dictionary<string, GameObject> Roster;
    [SerializeField] GameObject UnityChan;
    [SerializeField] GameObject Mecha;
    [SerializeField] GameObject Fighter;
    // Start is called before the first frame update
    void Start()
    {
        Roster = new Dictionary<string, GameObject>
        {
            { "Mecha", Mecha },
            { "unitychan_humanoid", UnityChan },
            { "Fighter", Fighter }
        };

        //PlayerPrefs.SetString("CharacterSelection", "Mecha");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, GameObject> getRoster()
    {
        return Roster;
    }
}
