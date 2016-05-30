﻿using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    int currentRoom = 0;
    string[] roomList;
    GUIStyle lobbyFont;
   // public GameObject spawnpoint;
    
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
        //if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        //  print("You clicked the button!");
        if (GUI.Button(new Rect(10, 10, 150, 100), "Join Random")) ;


            

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
        //RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2}; //Set max players
        PhotonNetwork.CreateRoom("Room" + currentRoom);

    }
    

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom" + PhotonNetwork.room.name);
      //  SpawnMyPlayer();
     //   Debug.Log("Name:"+PhotonNetwork.playerName);
    }

    void SpawnMyPlayer()
    {
      //  Vector3 spawnposition = spawnpoint.transform.position;
      //  GameObject MyPlayerGO = (GameObject)PhotonNetwork.Instantiate("Player", spawnposition, Quaternion.identity, 0);
      //  ((MonoBehaviour)MyPlayerGO.GetComponent("Controls")).enabled = true;
    }
    
}
