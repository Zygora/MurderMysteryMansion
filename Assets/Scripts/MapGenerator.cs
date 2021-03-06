﻿using UnityEngine;
using System.Collections.Generic;


public class MapGenerator : MonoBehaviour {

	public string TileSpriteRootGameObjectName = "Tiled Object";

	public float XTiles;
	public float YTiles;
    
	public GameObject Room1;
	public GameObject Room2;
	public GameObject Room3;
	public GameObject Room4;
	public GameObject Room5;
	public GameObject Room6;
	public GameObject Room7;
	public GameObject Room8;
	public GameObject Room9;
	public GameObject Room10;
	public GameObject Room11;
	public GameObject Room12;
	public GameObject Room13;
	public GameObject Room14;
	public GameObject Room15;
	public GameObject Room16;
	

    bool isMaster = false;

    [SerializeField] public string seed = "";
    void Start () {
		TileSpriteRootGameObjectName = "Rooms";

		CreateSpriteTiledGameObject (XTiles, YTiles, 
			Room1, Room2,Room3,Room4,Room5, 
			Room6, Room7,Room8,Room9,Room10,
			Room11, Room12,Room13,Room14,Room15,
			Room16, TileSpriteRootGameObjectName);
		
	}


	public static void CreateSpriteTiledGameObject(float XTiles, float YTiles,
		GameObject SpriteLevelFile1, GameObject SpriteLevelFile2, GameObject SpriteLevelFile3, GameObject SpriteLevelFile4, GameObject SpriteLevelFile5,
		GameObject SpriteLevelFile6, GameObject SpriteLevelFile7, GameObject SpriteLevelFile8, GameObject SpriteLevelFile9, GameObject SpriteLevelFile10,
		GameObject SpriteLevelFile11, GameObject SpriteLevelFile12, GameObject SpriteLevelFile13, GameObject SpriteLevelFile14, GameObject SpriteLevelFile15,
		GameObject SpriteLevelFile16, string RootObjectName) {

		float spriteX = SpriteLevelFile1.GetComponent<Renderer>().bounds.size.x;
		float spriteY = SpriteLevelFile1.GetComponent<Renderer>().bounds.size.y;

		GameObject rootObject = new GameObject ();
		rootObject.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		rootObject.name = RootObjectName;

		//list holding all rooms
		List<GameObject> Rooms = new List<GameObject> ();
		Rooms.Add (SpriteLevelFile1);
		Rooms.Add (SpriteLevelFile2);
		Rooms.Add (SpriteLevelFile3);
		Rooms.Add (SpriteLevelFile4);
		Rooms.Add (SpriteLevelFile5);
		Rooms.Add (SpriteLevelFile6);
		Rooms.Add (SpriteLevelFile7);
		Rooms.Add (SpriteLevelFile8);
		Rooms.Add (SpriteLevelFile9);
		Rooms.Add (SpriteLevelFile10);
		Rooms.Add (SpriteLevelFile11);
		Rooms.Add (SpriteLevelFile12);
		Rooms.Add (SpriteLevelFile13);
		Rooms.Add (SpriteLevelFile14);
		Rooms.Add (SpriteLevelFile15);
		Rooms.Add (SpriteLevelFile16);

		int currentObjectCount = 0;
		int currentColumn = 0;
		int currentrow = 0;
        int roomCoordinateX = 0;
        int roomCoordinateY = 0;
        int librarySecondHalfReference = 1;

        bool hallofPortraitsCreated = false;

        Vector3 currentLocation = new Vector3(0.0f, 0.0f, 0.0f);

        //MapGenerator MasterObj = new MapGenerator();

        //create map
        while (currentrow < YTiles && currentObjectCount!=16) {
            int maxRandomRange = Rooms.Count + 1;
			int x = Random.Range (3, maxRandomRange);

            if (currentObjectCount >= 14)
            {
                x = Random.Range(1, maxRandomRange);
            }


            // spawn the double room hall of portraits
            if (Rooms[x - 1].name == "Hall of Portraits")
            {
                if (currentColumn != 3)
                {
                    GameObject gridObject1 = Instantiate(Rooms[x - 1], currentLocation, Quaternion.identity) as GameObject;
                    currentObjectCount++;
                    gridObject1.transform.SetParent(rootObject.transform);
                    gridObject1.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                    gridObject1.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                    Rooms.RemoveAt(x - 1);
                    currentLocation.x = currentLocation.x + spriteX;
                    currentColumn++;
                    roomCoordinateX += 1;

                    //instantiate second half of hall of portraits room

                    GameObject gridObject2 = Instantiate(Rooms[0], currentLocation, Quaternion.identity) as GameObject;
                    currentObjectCount++;
                    gridObject2.transform.SetParent(rootObject.transform);
                    gridObject2.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                    gridObject2.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                    Rooms.RemoveAt(0);
                    currentLocation.x = currentLocation.x + spriteX;
                    currentColumn++;
                    roomCoordinateX += 1;
                    hallofPortraitsCreated = true;
                }
            }

            //spawn the double room library 
            else if (Rooms[x - 1].name == "Library")
            {
                if (currentColumn != 3)
                {
                    GameObject gridObject1 = Instantiate(Rooms[x - 1], currentLocation, Quaternion.identity) as GameObject;
                    currentObjectCount++;
                    gridObject1.transform.SetParent(rootObject.transform);
                    gridObject1.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                    gridObject1.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                    Rooms.RemoveAt(x - 1);
                    currentLocation.x = currentLocation.x + spriteX;
                    currentColumn++;
                    roomCoordinateX += 1;

                    //instantiate second half of  library room
                    if (hallofPortraitsCreated == true)
                    {
                        librarySecondHalfReference = 0;
                    }
                    GameObject gridObject2 = Instantiate(Rooms[librarySecondHalfReference], currentLocation, Quaternion.identity) as GameObject;
                    currentObjectCount++;
                    gridObject2.transform.SetParent(rootObject.transform);
                    gridObject2.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                    gridObject2.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                    Rooms.RemoveAt(librarySecondHalfReference);
                    currentLocation.x = currentLocation.x + spriteX;
                    currentColumn++;
                    roomCoordinateX += 1;
                }
            }

            //spawn room
            else if (Rooms[x - 1].name != "Library" && Rooms[x - 1].name != "Hall of Portraits")
            {
                GameObject gridObject = Instantiate(Rooms[x - 1], currentLocation, Quaternion.identity) as GameObject;
                currentObjectCount++;
                gridObject.transform.SetParent(rootObject.transform);
                gridObject.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                gridObject.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                Rooms.RemoveAt(x - 1);
                currentLocation.x = currentLocation.x + spriteX;
                currentColumn++;
                roomCoordinateX += 1;
            }

            //move to next row
            if (currentColumn >= XTiles)
            { 
				currentColumn = 0;
                roomCoordinateX = 0;
				currentrow++;
                roomCoordinateY += 1;
                currentLocation.x = 0;
                currentLocation.y = currentLocation.y + spriteY;
			}

            //spawn players after level has been created
            if (currentObjectCount == 16) {
               rootObject.AddComponent<RandomSpawnPlayers>();
            }
		}

        
        //Debug.Log(MasterObj.seed);

       /* if (PhotonNetwork.player.isMasterClient)
        {
            //MasterObj.isMaster = true;
            //Debug.Log("Is Master:"+MasterObj.isMaster);
        }*/
    }


   /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        
        if (stream.isWriting) // Our player. We need to send our actual position to the network.
        {
            if(isMaster)
            {
                stream.SendNext(seed);
            }
          
        }
        else
        {
            //this is someone else's player. We need to receive their position (as of a few milliseconds ago, and update our version of that player.
            seed = (string)stream.ReceiveNext();

        }

    }*/
}
