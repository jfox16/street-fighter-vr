using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public enum Team {Neutral, Red, Blue, Green, Yellow}
    public Team team = Team.Neutral;

    public abstract void Hurt(float damage);

    public virtual void MakeHitParticle(Vector3 hitPosition) {
        GameObject _hitSpark = (GameObject)Resources.Load("Particles/HitSpark");
        Instantiate(_hitSpark, hitPosition, transform.rotation);
    }

    public virtual void PlayHitSound(Vector3 hitPosition) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", hitPosition);
    }
}
