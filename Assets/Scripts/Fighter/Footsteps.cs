using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private void Step()
    {
        if(this.gameObject.name.Contains("Mecha"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Characters/Mecha/Footsteps", this.gameObject.transform.position);
        }
        else if(this.gameObject.name.Contains("Unitychan"))
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Characters/Unity-chan/Footsteps", this.gameObject.transform.position);
    }

    private void FallDown()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/PlayerFallToGround", this.gameObject.transform.position);
    }
}
