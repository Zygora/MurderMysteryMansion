using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    int currentRoom = 0;
    string[] roomList;

    // Use this for initialization
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Connect()
    {
        Debug.Log("Connect()");
        PhotonNetwork.ConnectUsingSettings("1.01.0");
        Debug.Log("Connect2()");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnConnectedToMaster()
    {
        if (PhotonNetwork.countOfPlayers == 4)
        {

        }
        else
            PhotonNetwork.JoinRandomRoom();
            
        Debug.Log("OnConnectedToMaster");
    }

    void OnJoiledLobby()
    {
        Debug.Log("room length : " + PhotonNetwork.GetRoomList().Length);
    }

    void OnPhotonRandomJoinFailed()
    {
        currentRoom++;
        Debug.Log("OnPhotonRandomJoinFailed");
        Debug.Log(PhotonNetwork.ServerAddress);
        //PhotonNetwork.GetRoomList();
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 1}; //Set max players
        PhotonNetwork.CreateRoom("Room" + currentRoom, roomOptions, TypedLobby.Default);

    }
    

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom" + PhotonNetwork.room.name);
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        GameObject MyPlayerGO = (GameObject)PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        ((MonoBehaviour)MyPlayerGO.GetComponent("Controls")).enabled = true;
    }

}
