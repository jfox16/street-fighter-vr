﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicContinue : MonoBehaviour
{
    private static MusicContinue instance = null;
    public static MusicContinue Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if((instance != null && instance != this))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("VR Start") && !SceneManager.GetActiveScene().name.Equals("SinglePlayer") && !SceneManager.GetActiveScene().name.Equals("MultiplayerLobby"))
        {
            Destroy(this.gameObject);
        }
    }

}
