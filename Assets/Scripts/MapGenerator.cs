using UnityEngine;
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

		//create rooms on script start
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

		// store the size of the gameobject
		float spriteX = SpriteLevelFile1.GetComponent<Renderer>().bounds.size.x;
		float spriteY = SpriteLevelFile1.GetComponent<Renderer>().bounds.size.y;

		//create new gameobject
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



        //starting position of first room
 
        Vector3 currentLocation = new Vector3(0.0f, 0.0f, 0.0f);

        //MapGenerator MasterObj = new MapGenerator();

        //do while rows is still less than meax rows
        while (currentrow < YTiles && currentObjectCount!=16) {
            
            //change max random number range to length of list +1;
            int maxRandomRange = Rooms.Count + 1;
			int x = Random.Range (2, maxRandomRange);

            if (currentObjectCount == 15)
            {
                x = Random.Range(1, maxRandomRange);
            }

            // spawn the double room "hall of portraits together" and only if possible. 
            if (Rooms[x - 1].name == "Hall of Portraits")
            {
                if (currentColumn!=3)
                {
                    //instantiate room based on random number
                    GameObject gridObject1 = Instantiate(Rooms[x-1], currentLocation, Quaternion.identity) as GameObject;
                    currentObjectCount++;
                    //make room child of empty container
                    gridObject1.transform.SetParent(rootObject.transform);
                    //set grid coordinate
                    gridObject1.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                    //set column
                    gridObject1.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                    //remove room from list so that it can't be repeated
                    Rooms.RemoveAt(x-1);
                    //set location to the right of last gameobject
                    currentLocation.x = currentLocation.x + spriteX;
                    //increment number of current columns;
                    currentColumn++;
                    roomCoordinateX += 1;

                    //instantiate second half of  hall of portraits room
                    
                        GameObject gridObject2 = Instantiate(Rooms[0], currentLocation, Quaternion.identity) as GameObject;
                        currentObjectCount++;
                        //make room child of empty container
                        gridObject2.transform.SetParent(rootObject.transform);
                        //set grid coordinate
                        gridObject2.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                        //set column
                        gridObject2.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                        //remove room from list so that it can't be repeated
                        Rooms.RemoveAt(0);
                        //set location to the right of last gameobject
                        currentLocation.x = currentLocation.x + spriteX;
                        //increment number of current columns;
                        currentColumn++;
                        roomCoordinateX += 1;
                    }
                }

            //spawn room
           else
           {
                //instantiate room based on random number
                GameObject gridObject = Instantiate(Rooms[x-1], currentLocation, Quaternion.identity) as GameObject;
                currentObjectCount++;
                //make room child of empty container
                gridObject.transform.SetParent(rootObject.transform);
                //set grid coordinate
                gridObject.GetComponent<AddVectorToRoom>().roomCoordinate = new Vector2(roomCoordinateX, roomCoordinateY);
                //set column
                gridObject.GetComponent<AddVectorToRoom>().currentColumn = currentColumn;
                //remove room from list so that it can't be repeated
                Rooms.RemoveAt(x-1);
                //set location to the right of last gameobject
                currentLocation.x = currentLocation.x + spriteX;
                //increment number of current columns;
                currentColumn++;
                roomCoordinateX += 1;
            }

            if (currentColumn >= XTiles) {
				//reset column to 0 when it reaches max columns
				currentColumn = 0;
                roomCoordinateX = 0;
				//increment rows when it reaches max columns
				currentrow++;
                roomCoordinateY += 1;
                //reset starting location when it reaches max columns
                //currentLocation.x = 0;
                currentLocation.x = 0;
                //move rooms down by height of rooms when it reaches max columns to start a new row
                currentLocation.y = currentLocation.y + spriteY;

			}

            //spawn players after level has been created
            if (currentObjectCount == 16) {
               rootObject.AddComponent<RandomSpawnPlayers>();
            }
		}

        //Debug.Log(MasterObj.seed);

        if (PhotonNetwork.player.isMasterClient)
        {
            //MasterObj.isMaster = true;
            //Debug.Log("Is Master:"+MasterObj.isMaster);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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

    }
}
