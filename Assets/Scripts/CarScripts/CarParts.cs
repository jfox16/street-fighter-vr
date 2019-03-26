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
        _carParts = new Dictionary<string, GameObject>();
        _carParts.Add("body", body);
        _carParts.Add("dashboard", dashboard);
        _carParts.Add("doorL", doorL);
        _carParts.Add("doorR", doorR);
        _carParts.Add("hood", hood);
        _carParts.Add("lightGlassL", lightGlassL);
        _carParts.Add("lightGlassR", lightGlassR);
        _carParts.Add("roof", roof);
        _carParts.Add("tailLights", tailLights);
        _carParts.Add("trunk", trunk);
        _carParts.Add("windowB", windowB);
        _carParts.Add("windowF", windowF);
        _carParts.Add("windowLB", windowLB);
        _carParts.Add("windowLF", windowLF);
        _carParts.Add("windowRB", windowRB);
        _carParts.Add("windowRF", windowRF);
        _carParts.Add("wheelLB", wheelLB);
        _carParts.Add("wheelLF", wheelLF);
        _carParts.Add("wheelRB", wheelRB);
        _carParts.Add("wheelRF", wheelRF);
        _carParts.Add("bumperB", bumperB);
        _carParts.Add("bumperF", bumperF);
    }

    private void OnMouseDown()
    {
        /*GameObject backWindow = carParts["windowB"];
        Rigidbody backWindowRB = backWindow.AddComponent<Rigidbody>();
        backWindowRB.mass = 0.1f;*/
        GameObject test1 = _carParts["doorR"];
        Rigidbody t = test1.AddComponent<Rigidbody>();
        t.mass = 0.1f;
    }

    public Dictionary<string, GameObject> getCarParts()
    {
        return _carParts;
    }
}
