using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ParticleScript can be put on any particle that needs to be destroyed after a period of time.
 * Set lifeTime to how long you want the particle to stay alive.
 */
public class ParticleScript : MonoBehaviour
{
    
    [SerializeField] float lifeTime = 1.0f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
