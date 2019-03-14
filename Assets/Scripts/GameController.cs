using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Vector3 playerSpawnPosition = new Vector3(0, 0, -10);
    [SerializeField] string roomName = "Test Room";

    /* If isOnline is checked, GameController will connect to
    the PUN Network in Start() */
    [SerializeField] bool isOnline = false;



    #region UNITY CALLBACKS

    void Awake() {
        // Adds this class as a callback target for the PUN Networking Client
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        // Sets the app's Id in the PUN Networking Client
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
    }

    void Start() {
        if (!isOnline) SpawnPlayer();
        else {
            ConnectToServer();
        }
    }

    #endregion



    #region PUN CALLBACKS

    void ConnectToServer() {
        Debug.Log("Connecting to server...");
        // Connects to server, automatically choosing the one with lowest ping
        PhotonNetwork.ConnectToBestCloudServer();
    }

    override public void OnConnectedToMaster() {
        Debug.Log("Connected to server! Joining or creating room " + roomName + "...");
        /* Joins a room named roomName, or creates one if it doesn't exist.
        The other parameters don't matter yet */
        PhotonNetwork.JoinOrCreateRoom(roomName, 
            new Photon.Realtime.RoomOptions(),
            Photon.Realtime.TypedLobby.Default);
    }

    override public void OnJoinedRoom() {
        Debug.Log("Connected to room! Spawning player...");
        SpawnNetworkPlayer();
    }

    #endregion



    public void SpawnPlayer() {
        // Spawn a player at playerSpawnPosition
        GameObject player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FPLook>().AttachCamera(mainCamera);
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
        player.GetComponent<FPLook>().AttachCamera(mainCamera);
    }
}
