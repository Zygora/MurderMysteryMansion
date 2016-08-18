using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomSpawnPlayers : MonoBehaviour {
    public List<Transform> RoomPositions;
    public List<Transform> SpawnPositions;
    public List<Transform> RoomPositions2;
    public List<int> PlayerAbilities;
    public List<int> ladderSpawnPositions;

    public bool player1Spawned = false;
    public bool player2Spawned = false;
    public bool player3Spawned = false;
    public bool player4Spawned = false;
    public bool murdererSpawned = false;
    private bool murdererHasAbility = false;

    private GameObject player1Camera;
    private GameObject player2Camera;
    private GameObject player3Camera;
    private GameObject player4Camera;
    private GameObject butcherRoom;

    public static Transform Player1Spawn;
    public static Transform Player2Spawn;
    public static Transform Player3Spawn;
    public static Transform Player4Spawn;

    private Text Player1AbilityText;
    private Text Player2AbilityText;
    private Text Player3AbilityText;
    private Text Player4AbilityText;

    public float AbilityTextTime;

    private int orbsSpawned = 0;
    private int laddersSpawned = 0;
    private int random;
    private int randomMurdererNumber;
    private int randomMax;
    private int numberOfPlayerAbilities;
    private int randomPlayerAbility;
    private int numberOfMurdererAbilities;
    private int randomMurdererAbility;
    private int murdererPlayerNumber;
    private int randomLadderNumber;
    private int ladderXOffset = 89;
    private int ladderYoffset = 33;

    // Use this for initialization
    void Start () {
        SpawnPositions = new List<Transform>();
        RoomPositions = new List<Transform>();
        RoomPositions2 = new List<Transform>();
        PlayerAbilities = new List<int>();
        ladderSpawnPositions = new List<int>();
    
        //add rooms created into list that this script can access
        for (int x = 0; x < 16; x++)
        {
            //ignore butcher room for list of rooms player can spawn into
            if (this.gameObject.transform.GetChild(x).name != "ButcherRoom(Clone)")
            {
                RoomPositions.Add(this.gameObject.transform.GetChild(x));
            }
        }

        //add rooms created into list that this script can access
        for (int x = 0; x < 16; x++)
        {
                RoomPositions2.Add(this.gameObject.transform.GetChild(x));
        }

        //set max range value for random.range equal to size of list;
        randomMax = 15;
        //set number of player and murderer abilities
        numberOfPlayerAbilities = 7;
        numberOfMurdererAbilities = 3;
        //grab cameras in scene
        player1Camera = GameObject.FindGameObjectWithTag("Player1Camera");
        player2Camera = GameObject.FindGameObjectWithTag("Player2Camera");
        player3Camera = GameObject.FindGameObjectWithTag("Player3Camera");
        player4Camera = GameObject.FindGameObjectWithTag("Player4Camera");
        //grab butcher room from scene
        butcherRoom = GameObject.FindGameObjectWithTag("MurdererStart");
        //random number for deciding the murderer
        //randomMurdererNumber = Random.Range(0, 4);
        //used for changing who the murderer is(just for testing purposes)
        randomMurdererNumber = 0;
        //random number for deciding murderer ability
        randomMurdererAbility = Random.Range(0, numberOfMurdererAbilities);
        //set the number of the player that is the murderer
        murdererPlayerNumber = randomMurdererNumber + 1;
        //grab text to display ability name
        Player1AbilityText = GameObject.FindGameObjectWithTag("Player1AbilityText").GetComponent<Text>();
        Player2AbilityText = GameObject.FindGameObjectWithTag("Player2AbilityText").GetComponent<Text>();
        Player3AbilityText = GameObject.FindGameObjectWithTag("Player3AbilityText").GetComponent<Text>();
        Player4AbilityText = GameObject.FindGameObjectWithTag("Player4AbilityText").GetComponent<Text>();
        //set time text stays up for
        AbilityTextTime 
            = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<GameOverTextManager>().AbilityTextUptimeSeconds;
    }
	
	// Update is called once per frame
	void Update () {       
        //spawn ladders
        if (laddersSpawned < 6) {
            if (laddersSpawned == 0) {
                SpawnLadder(0, 4, -1);
            }

            if (laddersSpawned == 1) {
                SpawnLadderSameFloor(0, 4, -1);
            }

            if(laddersSpawned == 2) {
                SpawnLadder(4, 8, 1);
            }

            if (laddersSpawned == 3)
            {
                SpawnLadderSameFloor(4, 8, 1);
            }

            if (laddersSpawned == 4)
            {
                SpawnLadder(8, 12, -1);
            }

            if (laddersSpawned == 5)
            {
                SpawnLadderSameFloor(8, 12, -1);
            }
        }
        
        //spawn player1 if not murderer in a random room not containing another player
        if (player1Spawned == false && randomMurdererNumber != 0)
        {
            SpawnWimp("MainPlayer_1", "Player1", player1Camera, player2Camera, player3Camera,
                       player4Camera , Player1AbilityText);
            player1Spawned = true;
        }

        //spawn player 1 if murderer in butcher room
        else if (player1Spawned == false && randomMurdererNumber == 0) {
            Instantiate(Resources.Load("MainPlayer_1"), butcherRoom.transform.position, Quaternion.identity);
            SpawnPositions.Add(butcherRoom.transform);
            player1Camera.transform.position = new Vector3(butcherRoom.transform.position.x, butcherRoom.transform.position.y, -15);
            player1Spawned = true;
        }

        //spawn player2 if not murderer  in a random room not containing another player
        if (player2Spawned == false && randomMurdererNumber != 1)
        {
            SpawnWimp("MainPlayer_2", "Player2", player2Camera, player1Camera, player3Camera,
                       player4Camera, Player2AbilityText);
            player2Spawned = true;
        }

        //spawn player 2 if murderer in butcher room
        else if (player2Spawned == false && randomMurdererNumber == 1)
        {
            Instantiate(Resources.Load("MainPlayer_2"), butcherRoom.transform.position, Quaternion.identity);
            SpawnPositions.Add(butcherRoom.transform);
            player2Camera.transform.position = new Vector3(butcherRoom.transform.position.x, butcherRoom.transform.position.y, -15);
            player2Spawned = true;
        }

        //spawn player3 if not murderer  in a random room not containing another player
        if (player3Spawned == false && randomMurdererNumber != 2)
        {
            SpawnWimp("MainPlayer_3", "Player3", player3Camera, player1Camera, player2Camera,
                       player4Camera, Player3AbilityText);
            player3Spawned = true;
        }

        //spawn player 3 if murderer in butcher room
        else if (player3Spawned == false && randomMurdererNumber == 2)
        {
            Instantiate(Resources.Load("MainPlayer_3"), butcherRoom.transform.position, Quaternion.identity);
            SpawnPositions.Add(butcherRoom.transform);
            player3Camera.transform.position = new Vector3(butcherRoom.transform.position.x, butcherRoom.transform.position.y, -15);
            player3Spawned = true;
        }

        //spawn player4 if not murderer in a random room not containing another player
        if (player4Spawned == false && randomMurdererNumber != 3)
        {
            SpawnWimp("MainPlayer_4", "Player4", player4Camera, player1Camera, player2Camera,
                       player3Camera, Player4AbilityText);
            player4Spawned = true;
        }

        //spawn player 4 if murderer in butcher room
        else if (player4Spawned == false && randomMurdererNumber == 3)
        {
            Instantiate(Resources.Load("MainPlayer_4"), butcherRoom.transform.position, Quaternion.identity);
            SpawnPositions.Add(butcherRoom.transform);
            player4Camera.transform.position = new Vector3(butcherRoom.transform.position.x, butcherRoom.transform.position.y, -15);
            player4Spawned = true;
        }

        //spawm orb in random room not containing a player
        if (orbsSpawned < 3) {
            random = Random.Range(0, randomMax);
            //prevent orb from spawning in altar room
            if (RoomPositions[random].name != "Altar Room(Clone)")
            {
                Instantiate(Resources.Load("Orb"), RoomPositions[random].position, Quaternion.identity);
                RoomPositions.RemoveAt(random);
                randomMax -= 1;
                orbsSpawned += 1;
            }
        }

        //enable murderer script if player is the murderer also change player tag and disable visibility of squeler arrows
        if (randomMurdererNumber == 0 && murdererSpawned == false)
        {
            EnableMurder("Player1","Murderer1",true,player1Camera);
        }
        if (randomMurdererNumber == 1 && murdererSpawned == false)
        {
            EnableMurder("Player2", "Murderer2", true, player2Camera);
        }
        if (randomMurdererNumber == 2 && murdererSpawned == false)
        {
            EnableMurder("Player3", "Murderer3", true, player3Camera);
        }
        if (randomMurdererNumber == 3 && murdererSpawned == false)
        {
            EnableMurder("Player4", "Murderer4", true, player4Camera);
        }

        //give the murderer a random ability
        if (murdererHasAbility == false && murdererSpawned == true)
        {
           if (randomMurdererAbility == 0)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<ShittyPossum>();
                GameObject.FindGameObjectWithTag("Player" + murdererPlayerNumber + "AbilityText").GetComponent<Text>().text
                    = "Possum";
            }

           else if (randomMurdererAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<ThrillOfTheHunt>();
                GameObject.FindGameObjectWithTag("Player" + murdererPlayerNumber + "AbilityText").GetComponent<Text>().text
                    = "Thrill of The Hunt";
            }

           else if (randomMurdererAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<Traps>();
                //allow player to see arrows associated with ability
                GameObject.FindGameObjectWithTag("Player" + murdererPlayerNumber+"Camera").GetComponent<Camera>().cullingMask |= (1 << 17);
                GameObject.FindGameObjectWithTag("Player" + murdererPlayerNumber + "AbilityText").GetComponent<Text>().text
                    = "Trapper";
            }
            murdererHasAbility = true;
        }

        if (AbilityTextTime > 0)
        {
            AbilityTextTime -= Time.deltaTime;
            //toggle player ability text
            if (SpawnPositions[0].position.x != player1Camera.transform.position.x ||
                SpawnPositions[0].position.y != player1Camera.transform.position.y)
            {
                Player1AbilityText.enabled = false;
            }

            else
            {
                Player1AbilityText.enabled = true;
            }

            if (SpawnPositions[1].position.x != player2Camera.transform.position.x ||
                SpawnPositions[1].position.y != player2Camera.transform.position.y)
            {
                Player2AbilityText.enabled = false;
            }

            else
            {
                Player2AbilityText.enabled = true;
            }

            if (SpawnPositions[2].position.x != player3Camera.transform.position.x ||
                SpawnPositions[2].position.y != player3Camera.transform.position.y)
            {
                Player3AbilityText.enabled = false;
            }

            else
            {
                Player3AbilityText.enabled = true;
            }

            if (SpawnPositions[3].position.x != player4Camera.transform.position.x ||
                SpawnPositions[3].position.y != player4Camera.transform.position.y)
            {
                Player4AbilityText.enabled = false;
            }

            else
            {
                Player4AbilityText.enabled = true;
            }
        }

        else
        {
            Player1AbilityText.enabled = false;
            Player2AbilityText.enabled = false;
            Player3AbilityText.enabled = false;
            Player4AbilityText.enabled = false;
        }

    }
    //FUNCTIONS
    void SpawnLadder(int min, int max, int side)
    {
        randomLadderNumber = Random.Range(min, max);
        Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset * side) + (transform.up * ladderYoffset), Quaternion.identity);
        ladderSpawnPositions.Add(randomLadderNumber);
        laddersSpawned += 1;
    }

    void SpawnLadderSameFloor(int min, int max, int side)
    {
        randomLadderNumber = Random.Range(min, max);
        while (ladderSpawnPositions.Contains(randomLadderNumber))
        {
            randomLadderNumber = Random.Range(min, max);
        }
        Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset * side) + (transform.up * ladderYoffset), Quaternion.identity);
        ladderSpawnPositions.Add(randomLadderNumber);
        laddersSpawned += 1;
    }

    void SpawnWimp(string prefab , string tag , GameObject camera1, GameObject camera2,
         GameObject camera3, GameObject camera4 ,Text abilityText)
    {
        random = Random.Range(0, randomMax);
        Instantiate(Resources.Load(prefab), RoomPositions[random].position, Quaternion.identity);
        //move camera to same position as player
        camera1.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
        SpawnPositions.Add(RoomPositions[random]);
        //remove room from list so that nothing else can spawn in that same room
        RoomPositions.RemoveAt(random);
        //reduce randomMax by 1 so that it matches the size of the roomPositions list
        randomMax -= 1;
        //give player randomly generated ability
        randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
        while (PlayerAbilities.Contains(randomPlayerAbility))
        {
            randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
        }
        PlayerAbilities.Add(randomPlayerAbility);
        if (randomPlayerAbility == 0)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<CrazedAlchemist>();
            abilityText.text = "Crazed Alchemist";
        }
        if (randomPlayerAbility == 1)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<Doppelganger>();
            abilityText.text = "Doppelganger";
        }

        if (randomPlayerAbility == 2)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<Squeler>();
            //allow other players to see arrows associated with ability
            camera2.GetComponent<Camera>().cullingMask |= (1 << 18);
            camera3.GetComponent<Camera>().cullingMask |= (1 << 18);
            camera4.GetComponent<Camera>().cullingMask |= (1 << 18);
            abilityText.text = "Squeler";
        }
        if (randomPlayerAbility == 3)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<ThirdEye>();
            //make player able to see arrows associated with ability
            camera1.GetComponent<Camera>().cullingMask |= (1 << 15);
            abilityText.text = "Third Eye";
        }

        if (randomPlayerAbility == 4)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<DrNerd>();
            abilityText.text = "Dr. Nerd";
        }

        if (randomPlayerAbility == 5)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<Diseased>();
            abilityText.text = "Diseased";
        }

        if (randomPlayerAbility == 6)
        {
            GameObject.FindGameObjectWithTag(tag).AddComponent<ScaredCat>();
            abilityText.text = "Scaredy Cat";
        }
        
    }

    void EnableMurder(string previousTag, string nextTag, bool spawned, GameObject camera)
    {
        GameObject.FindGameObjectWithTag(previousTag).GetComponent<MurdererScripts>().enabled = true;
        GameObject.FindGameObjectWithTag(previousTag).gameObject.tag = nextTag;
        murdererSpawned = spawned;
        camera.GetComponent<Camera>().cullingMask &= ~(1 << 18);
    }
}
