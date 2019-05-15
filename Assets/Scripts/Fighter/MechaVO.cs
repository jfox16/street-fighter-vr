using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaVO : CharacterVO
{
    public override void Defeat()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Mecha/Defeat", this.gameObject.transform.position);
    }
    public override void Grunts()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Mecha/Grunts", this.gameObject.transform.position);
    }
    public override void Intros()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Mecha/Intros", this.gameObject.transform.position);
    }
    public override void Victory()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Characters/Mecha/Victory", this.gameObject.transform.position);
    }
}
