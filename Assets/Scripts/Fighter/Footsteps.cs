using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private void Step()
    {
        //footSteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Characters/Mecha/Footsteps");
        //footSteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject.transform));
        //Debug.Log("footsteps");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Characters/Mecha/Footsteps", this.gameObject.transform.position);
        // check if the previous footstep sound finished
        //footSteps.getPlaybackState(out playbackState);
        //if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        //{
        //    footSteps.start();
        //}
    }
}
