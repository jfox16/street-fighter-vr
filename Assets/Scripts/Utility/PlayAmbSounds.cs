using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAmbSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance soundEvent;
    private bool playedCheering;
    // Start is called before the first frame update
    void Start()
    {
        playedCheering = false;
        if (SceneManager.GetActiveScene().name.Equals("VR Japan"))
            soundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Environment/Ambience/JapanAmb");
        else if(SceneManager.GetActiveScene().name.Equals("VR Arena"))
            soundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Environment/Ambience/ArenaAmb");
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (SceneManager.GetActiveScene().name.Equals("VR Arena") && !playedCheering)
            playCheering();
        else if (playedCheering && Time.timeSinceLevelLoad > 30)
        {
            soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            soundEvent.start();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    private void playCheering()
    {
        soundEvent.start();
        playedCheering = true;
    }

    private void OnDestroy()
    {
        soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
