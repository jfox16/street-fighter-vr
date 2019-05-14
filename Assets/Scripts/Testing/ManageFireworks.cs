using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageFireworks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-2.64f, 8.45f, 0.0f);
        Destroy(this.gameObject, 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
