using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private void Step()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Characters/Mecha/Footsteps", this.gameObject.transform.position);
    }
}
