using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    int currentRoom = 0;
    string[] roomList;
    GUIStyle lobbyFont;
    [SerializeField] public GameObject[] SpawnPoint;

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
       // if (GUI.Button(new Rect(10, 10, 150, 100), "Join Random")) ;


            

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
        currentRoom++;
        CreateRoom(null, 5);
    }

    void OnPhotonRandomJoinFailed()
    {
        currentRoom++;
        Debug.Log("OnPhotonRandomJoinFailed");
        Debug.Log(PhotonNetwork.ServerAddress);
        //PhotonNetwork.GetRoomList();
        //RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2}; //Set max players
      //  PhotonNetwork.CreateRoom("Room" + currentRoom);
        CreateRoom(null, 5);

    }

    public static void CreateRoom(string name, int skillLevel = 5)
    {

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 4;
        roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOptions.customRoomProperties.Add(RoomProperty.MapString, "");
        roomOptions.customRoomProperties.Add(RoomProperty.SkillLevel, skillLevel);

        roomOptions.customRoomPropertiesForLobby = new string[]
        {
            RoomProperty.MapString,
            RoomProperty.SkillLevel,
        };
        PhotonNetwork.CreateRoom(name);

    }


    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom" + PhotonNetwork.room.name);
        PhotonNetwork.room.maxPlayers = 4;
        SpawnMyPlayer();

        Debug.Log("Name:"+PhotonNetwork.playerName);
    }

    void SpawnMyPlayer()
    {
        int RandNum = Random.Range(1, 25);
        while(SpawnPoint[RandNum].GetComponent<SpawnPoint>().SpawnPointUsed)
        {
            RandNum = Random.Range(1, 25);
        }
        Vector3 spawnposition = SpawnPoint[RandNum].transform.position;
        SpawnPoint[RandNum].GetComponent<SpawnPoint>().SpawnPointUsed = true;
        GameObject MyPlayerGO = (GameObject)PhotonNetwork.Instantiate("Player", spawnposition, Quaternion.identity, 0);
        ((MonoBehaviour)MyPlayerGO.GetComponent("Controls")).enabled = true;
    }
    
}
