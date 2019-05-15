using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsVO : CharacterVO
{
    public override void Defeat()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Unity-chan/Defeat", this.gameObject.transform.position);
    }
    public override void Grunts()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Unity-chan/Grunts", this.gameObject.transform.position);
    }
    public override void Intros()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Unity-chan/Intros", this.gameObject.transform.position);
    }
    public override void Victory()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Unity-chan/Victory", this.gameObject.transform.position);
    }
}
