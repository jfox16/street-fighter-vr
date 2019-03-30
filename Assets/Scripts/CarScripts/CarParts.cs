using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParts : MonoBehaviour
{
    private Dictionary<string, GameObject> _carParts;
    [SerializeField] GameObject body;
    [SerializeField] GameObject dashboard;
    [SerializeField] GameObject doorL;
    [SerializeField] GameObject doorR;
    [SerializeField] GameObject hood;
    [SerializeField] GameObject lightGlassL;
    [SerializeField] GameObject lightGlassR;
    [SerializeField] GameObject roof;
    [SerializeField] GameObject tailLights;
    [SerializeField] GameObject trunk;
    [SerializeField] GameObject windowB;
    [SerializeField] GameObject windowF;
    [SerializeField] GameObject windowLB;
    [SerializeField] GameObject windowLF;
    [SerializeField] GameObject windowRB;
    [SerializeField] GameObject windowRF;
    [SerializeField] GameObject wheelLB;
    [SerializeField] GameObject wheelLF;
    [SerializeField] GameObject wheelRB;
    [SerializeField] GameObject wheelRF;
    [SerializeField] GameObject bumperB;
    [SerializeField] GameObject bumperF;

    // Start is called before the first frame update
    void Start()
    {
        _carParts = new Dictionary<string, GameObject>
        {
            { "body", body },
            { "dashboard", dashboard },
            { "doorL", doorL },
            { "doorR", doorR },
            { "hood", hood },
            { "lightGlassL", lightGlassL },
            { "lightGlassR", lightGlassR },
            { "roof", roof },
            { "tailLights", tailLights },
            { "trunk", trunk },
            { "windowB", windowB },
            { "windowF", windowF },
            { "windowLB", windowLB },
            { "windowLF", windowLF },
            { "windowRB", windowRB },
            { "windowRF", windowRF },
            { "wheelLB", wheelLB },
            { "wheelLF", wheelLF },
            { "wheelRB", wheelRB },
            { "wheelRF", wheelRF },
            { "bumperB", bumperB },
            { "bumperF", bumperF }
        };
    }

    /*private void OnMouseDown()
    {
        GameObject backWindow = carParts["windowB"];
        Rigidbody backWindowRB = backWindow.AddComponent<Rigidbody>();
        backWindowRB.mass = 0.1f;
        GameObject test1 = _carParts["doorR"];
        Rigidbody t = test1.AddComponent<Rigidbody>();
        t.mass = 0.1f;
    }*/

    public Dictionary<string, GameObject> getCarParts()
    {
        return _carParts;
    }
}
