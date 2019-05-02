﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static GameController Instance;

    [SerializeField] Vector3 playerSpawnPosition = new Vector3(0, 0, -10);
    [SerializeField] public string roomName = "Test Room";
    [SerializeField] GameObject spawnButtons;
    [SerializeField] GameObject healthbar;

    GameObject offlinePlayerPrefab;

    public List<Spawner> spawners = new List<Spawner>();
    public int currentScene;
    public int room;
    private int index;


    #region UNITY CALLBACKS

    void Awake() {
        if (GameController.Instance == null)
        {
            Instance = this;
        }
        else 
        {
            if ((GameController.Instance != null))
            {
                Destroy(GameController.Instance.gameObject);
                GameController.Instance = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);

        healthbar.SetActive(false);

        offlinePlayerPrefab = (GameObject)Resources.Load("Blue Guy");

         //Adds this class as a callback target for PUN Networking
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        // Sets the app's Id for PUN Networking
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
    }
    void Start()
    {
        
        SpawnNetworkPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Spawner spawner in spawners)
            {
                PhotonNetwork.Instantiate(spawner.resourceName, spawner.transform.position, spawner.transform.rotation);
            }
        }
    }


    void Update() {
        // Unlock cursor when Cancel is pressed
        if (Input.GetButtonDown("Cancel")) {
            Cursor.lockState = CursorLockMode.None;
        }
        // Lock cursor when Mouse clicked
        if (Input.GetMouseButtonDown(0)) {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    #endregion

    override public void OnConnectedToMaster() {
        Debug.Log("Connected to server! Joining or creating room " + roomName + "...");
        /* Joins a room named roomName, or creates one if it doesn't exist.
        The other parameters don't matter */
        PhotonNetwork.JoinOrCreateRoom(
            roomName, 
            new Photon.Realtime.RoomOptions(),
            Photon.Realtime.TypedLobby.Default
        );
    }

    void OnSceneFinishLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;

    }

    public override void OnEnable()
    {
        base.OnEnable();
       // PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishLoading;
    }


    public override void OnDisable()
    {
        base.OnDisable();
      //  PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishLoading;

    }

    override public void OnJoinedRoom() {
        base.OnJoinedRoom();
        Debug.Log("Connected to room! Spawning player...");
          
        if (!PhotonNetwork.IsMasterClient)
            return;
       StartGame();
    }

    public void SpawnNetworkPlayer() {
        /* When instantiating an object on the network, use PhotonNetwork.Instantiate instead.
        This will properly initialize it on the network. Also, instead of using direct prefab
        reference, this method takes the name of the prefab as a string, and the prefab will 
        be retrieved from the Assets/Resources folder. */
        GameObject player;
        if (PhotonNetwork.IsMasterClient)
        {
            if (PlayerInfo.PI.selectedCharacter == 0)
                player = PhotonNetwork.Instantiate("Network Red Guy", new Vector3(5, 0, -10), Quaternion.Euler(0, -90, 0));

            else if (PlayerInfo.PI.selectedCharacter == 1)
                player = PhotonNetwork.Instantiate("Network Blue Guy", new Vector3(-5, 0, -10), Quaternion.Euler(0, 90, 0));

            else
                player = PhotonNetwork.Instantiate("Network Blue Guy", new Vector3(-5, 0, -10), Quaternion.Euler(0, 90, 0));
        }
        else
        {
            if (PlayerInfo.PI.selectedCharacter == 0)
                player = PhotonNetwork.Instantiate("Network Red Guy", new Vector3(5, 0, -10), Quaternion.Euler(0, -90, 0));

            else if (PlayerInfo.PI.selectedCharacter == 1)
                player = PhotonNetwork.Instantiate("Network Blue Guy", new Vector3(-5, 0, -10), Quaternion.Euler(0, 90, 0));

            else
                player = PhotonNetwork.Instantiate("Network Blue Guy", new Vector3(-5, 0, -10), Quaternion.Euler(0, 90, 0));
        }
        
        Debug.Log("Player spawned!");
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);
        
        healthbar.SetActive(true);
        healthbar.GetComponent<Bar>().setFighter(player.GetComponent<Fighter>());
    }

    void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(1);
    }


    #region PUBLIC METHODS

    public void SpawnPlayer() {
        // Spawn a player at playerSpawnPosition
        GameObject player = Instantiate(offlinePlayerPrefab, playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);

        healthbar.SetActive(true);
        healthbar.GetComponent<Bar>().setFighter(player.GetComponent<Fighter>());
        spawnButtons.SetActive(false);
    }

    public void ConnectAndSpawnNetworkPlayer() {
        spawnButtons.SetActive(false);
    }

    #endregion


}
