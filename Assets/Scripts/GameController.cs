using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 playerSpawnPosition = new Vector3(0, 0, -10);
    [SerializeField] string roomName = "Test Room";

    [SerializeField] GameObject spawnButtons;

    GameObject offlinePlayerPrefab;



    #region UNITY CALLBACKS

    void Awake() {
        offlinePlayerPrefab = (GameObject)Resources.Load("Fighter");

        // Adds this class as a callback target for PUN Networking
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        // Sets the app's Id for PUN Networking
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
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



    #region PUN CALLBACKS

    void ConnectToServer() {
        Debug.Log("Connecting to server...");
        // Connects to server, automatically choosing the one with lowest ping
        PhotonNetwork.ConnectToBestCloudServer();
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

    override public void OnJoinedRoom() {
        Debug.Log("Connected to room! Spawning player...");
        SpawnNetworkPlayer();
    }

    void SpawnNetworkPlayer() {
        /* When instantiating an object on the network, use PhotonNetwork.Instantiate instead.
        This will properly initialize it on the network. Also, instead of using direct prefab
        reference, this method takes the name of the prefab as a string, and the prefab will 
        be retrieved from the Assets/Resources folder. */
        GameObject player = PhotonNetwork.Instantiate("Network Fighter", playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);
    }



    #region PUBLIC METHODS

    public void SpawnPlayer() {
        // Spawn a player at playerSpawnPosition
        GameObject player = Instantiate(offlinePlayerPrefab, playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);

        spawnButtons.SetActive(false);
    }

    public void ConnectAndSpawnNetworkPlayer() {
        ConnectToServer();
        spawnButtons.SetActive(false);
    }

    #endregion


}
