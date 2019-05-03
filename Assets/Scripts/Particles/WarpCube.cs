using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpCube : MonoBehaviour
{
    GameObject cube;
    [SerializeField] float height = 1;
    [SerializeField] float loopTime = 1;
    float theta = 0;
    float twopi = 2*Mathf.PI;

    void Awake() {
        cube = transform.Find("Cube").gameObject;
    }

    void Update() {
        if (cube == null) return;
        // This makes the cube float up and down
        if (loopTime != 0) theta += Time.deltaTime * twopi / loopTime;
        if (theta > twopi) theta -= twopi;

        cube.transform.localPosition = new Vector3(0, height*Mathf.Sin(theta), 0);
        cube.transform.eulerAngles = new Vector3(0, theta*Mathf.Rad2Deg, 0);
    }
}
