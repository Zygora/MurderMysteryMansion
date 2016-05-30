using UnityEngine;
using System.Collections;

public class ServerOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

    }
}
