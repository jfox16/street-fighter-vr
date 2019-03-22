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
            { "Unity-Chan", UnityChan },
            { "Mecha", Mecha },
            { "Fighter", Fighter }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
