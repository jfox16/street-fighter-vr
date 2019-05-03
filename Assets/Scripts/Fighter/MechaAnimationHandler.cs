using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAnimationHandler : FighterAnimationHandler
{    
    public void EnableLeftPunch() {
        EnableLimbCollider(Limb.LeftArm, 4);
    }

    public void EnableKick() {
        EnableLimbCollider(Limb.RightLeg, 8);
    }

    public void EnableRightPunch() {
        EnableLimbCollider(Limb.RightArm, 5);
    }

    public void EnableRightSmash() {
        EnableLimbCollider(Limb.RightArm, 10);
    }
}
