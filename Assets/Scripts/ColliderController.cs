using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider rightArmCollider;
    [SerializeField] Collider leftArmCollider;
    [SerializeField] Collider rightLegCollider;
    [SerializeField] Collider leftLegCollider;
    [SerializeField] Collider hurtBox;
    void Start()
    {
        rightArmCollider.enabled = false;
        leftArmCollider.enabled = false;
        rightLegCollider.enabled = false;
        leftLegCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableArms()
    {
        rightArmCollider.enabled = false;
    }
    private void EnableArms()
    {
        rightArmCollider.enabled = true;
    }

    //Will be decoupled later
    private void EnableMechaLightPunch()
    {
        leftArmCollider.enabled = true;
    }
    private void DisableMechaLightPunch()
    {
        leftArmCollider.enabled = false;
    }
    private void EnableMechaHeavyPunch()
    {
        rightArmCollider.enabled = true;
    }
    private void DisableMechaHeavyPunch()
    {
        rightArmCollider.enabled = false;
    }
    private void EnableMechaKick()
    {
        rightLegCollider.enabled = true;
    }
    private void DisableMechaKick()
    {
        rightLegCollider.enabled = false;
    }
}
