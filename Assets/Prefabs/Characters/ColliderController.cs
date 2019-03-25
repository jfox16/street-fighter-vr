using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider armCollider;
    [SerializeField] Collider hurtBox;
    void Start()
    {
        armCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableArms()
    {
       // armCollider.enabled = false;
    }
    private void EnableArms()
    {
       // armCollider.enabled = true;
    }
}
