using UnityEngine;
using System.Collections.Generic;

public class RandomSpawnPlayers : MonoBehaviour {
    public List<Transform> RoomPositions;
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
    private GameObject buctherRoom;
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
    private int ladderXOffset = 90;
    private int ladderYoffset = 50;
    // Use this for initialization
    void Start () {
        RoomPositions = new List<Transform>();
        RoomPositions2 = new List<Transform>();
        PlayerAbilities = new List<int>();
        ladderSpawnPositions = new List<int>();
    
        //add rooms created into list that this script can access
        for (int x = 0; x < 25; x++)
        {
            //ignore butcher room for list of rooms player can spawn into
            if (this.gameObject.transform.GetChild(x).name != "ButcherRoom(Clone)")
            {
                RoomPositions.Add(this.gameObject.transform.GetChild(x));
            }
        }

        //add rooms created into list that this script can access
        for (int x = 0; x < 25; x++)
        {
                RoomPositions2.Add(this.gameObject.transform.GetChild(x));
        }

        //set max range value for random.range equal to size of list;
        randomMax = 24;
        //set number of player and murderer abilities
        numberOfPlayerAbilities = 6;
        numberOfMurdererAbilities = 6;
        //grab cameras in scene
        player1Camera = GameObject.FindGameObjectWithTag("Player1Camera");
        player2Camera = GameObject.FindGameObjectWithTag("Player2Camera");
        player3Camera = GameObject.FindGameObjectWithTag("Player3Camera");
        player4Camera = GameObject.FindGameObjectWithTag("Player4Camera");
        //grab butcher room from scene
        buctherRoom = GameObject.FindGameObjectWithTag("MurdererStart");
        //random number for deciding the murderer
        randomMurdererNumber = Random.Range(0, 4);
        //used for changing who the murderer is(just for testing purposes)
        //randomMurdererNumber = 0;
        //random number for deciding murderer ability
        randomMurdererAbility = Random.Range(0, numberOfMurdererAbilities);
        //set the number of the player that is the murderer
        murdererPlayerNumber = randomMurdererNumber + 1;

     }
	
	// Update is called once per frame
	void Update () {
        //spawn ladders
       if (laddersSpawned < 8) {
            if (laddersSpawned == 0) {
                randomLadderNumber = Random.Range(0, 5);
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * -ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 1) {
                randomLadderNumber = Random.Range(0, 5);
                while (ladderSpawnPositions.Contains(randomLadderNumber)) {
                    randomLadderNumber = Random.Range(0, 5);
                }
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * -ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if(laddersSpawned == 2) {
                randomLadderNumber = Random.Range(5, 10);
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 3)
            {
                randomLadderNumber = Random.Range(5, 10);
                while (ladderSpawnPositions.Contains(randomLadderNumber))
                {
                    randomLadderNumber = Random.Range(5, 10);
                }
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 4)
            {
                randomLadderNumber = Random.Range(10, 15);
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * -ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 5)
            {
                randomLadderNumber = Random.Range(10, 15);
                while (ladderSpawnPositions.Contains(randomLadderNumber))
                {
                    randomLadderNumber = Random.Range(10, 15);
                }
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * -ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 6)
            {
                randomLadderNumber = Random.Range(15, 20);
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

            if (laddersSpawned == 7)
            {
                randomLadderNumber = Random.Range(15, 20);
                while (ladderSpawnPositions.Contains(randomLadderNumber))
                {
                    randomLadderNumber = Random.Range(15, 20);
                }
                Instantiate(Resources.Load("Ladder"), RoomPositions2[randomLadderNumber].position + (transform.right * ladderXOffset) + (transform.up * ladderYoffset), Quaternion.identity);
                ladderSpawnPositions.Add(randomLadderNumber);
                laddersSpawned += 1;
            }

        }
        
        //spawn player1 if not murderer in a random room not containing another player
        if (player1Spawned == false && randomMurdererNumber != 0)
        {
            random = Random.Range(0, randomMax);
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_1"), RoomPositions[random].position, Quaternion.identity);
            //move player 1 camera to same position as player1
            player1Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            //remove room from list so that nothing else can spawn in that same room
            RoomPositions.RemoveAt(random);
            //reduce randomMax by 1 so that it matches the size of the roomPositions list
            randomMax -= 1;
            player1Spawned = true;
            //give player randomly generated ability
            randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            PlayerAbilities.Add(randomPlayerAbility);
            if(randomPlayerAbility == 0){
                GameObject.FindGameObjectWithTag("Player1").AddComponent<CrazedAlchemist>();
            }
            if (randomPlayerAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Player1").AddComponent<Doppelganger>();
            }
            if (randomPlayerAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Player1").AddComponent<Revive>();
            }
            if (randomPlayerAbility == 3)
            {
                GameObject.FindGameObjectWithTag("Player1").AddComponent<ScaredCat>();
            }
            if (randomPlayerAbility == 4)
            {
                GameObject.FindGameObjectWithTag("Player1").AddComponent<Squeler>();
            }
            if (randomPlayerAbility == 5)
            {
                GameObject.FindGameObjectWithTag("Player1").AddComponent<ThirdEye>();
            }
        }

        //spawn player 1 if murderer in butcher room
        else if (player1Spawned == false && randomMurdererNumber == 0) {
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_1"), buctherRoom.transform.position, Quaternion.identity);
            //move player 1 camera to same position as player1
            player1Camera.transform.position = new Vector3(buctherRoom.transform.position.x, buctherRoom.transform.position.y, -15);
            player1Spawned = true;
        }

        //spawn player2 if not murderer  in a random room not containing another player
        if (player2Spawned == false && randomMurdererNumber != 1)
        {
            random = Random.Range(0, randomMax);
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_2"), RoomPositions[random].position, Quaternion.identity);
            //move player 2 camera to same position as player2
            player2Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            //remove room from list so that nothing else can spawn in that same room
            RoomPositions.RemoveAt(random);
            //reduce randomMax by 1 so that it matches the size of the roomPositions list
            randomMax -= 1;
            player2Spawned = true;
            //give player randomly generated ability
            randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            //makes sure the same player ability doesn't get repeated
            while(PlayerAbilities.Contains(randomPlayerAbility)) {
                randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            }
            PlayerAbilities.Add(randomPlayerAbility);
            if (randomPlayerAbility == 0)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<CrazedAlchemist>();
            }
            if (randomPlayerAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<Doppelganger>();
            }
            if (randomPlayerAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<Revive>();
            }
            if (randomPlayerAbility == 3)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<ScaredCat>();
            }
            if (randomPlayerAbility == 4)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<Squeler>();
            }
            if (randomPlayerAbility == 5)
            {
                GameObject.FindGameObjectWithTag("Player2").AddComponent<ThirdEye>();
            }
        }

        //spawn player 1 if murderer in butcher room
        else if (player2Spawned == false && randomMurdererNumber == 1)
        {
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_2"), buctherRoom.transform.position, Quaternion.identity);
            //move player 2 camera to same position as player2
            player2Camera.transform.position = new Vector3(buctherRoom.transform.position.x, buctherRoom.transform.position.y, -15);
            player2Spawned = true;
        }

        //spawn player3 if not murderer  in a random room not containing another player
        if (player3Spawned == false && randomMurdererNumber != 2)
        {
            random = Random.Range(0, randomMax);
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_3"), RoomPositions[random].position, Quaternion.identity);
            //move player 3 camera to same position as player3
            player3Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            //remove room from list so that nothing else can spawn in that same room
            RoomPositions.RemoveAt(random);
            //reduce randomMax by 1 so that it matches the size of the roomPositions list
            randomMax -= 1;
            player3Spawned = true;
            //give player randomly generated ability
            randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            //makes sure the same player ability doesn't get repeated
            while (PlayerAbilities.Contains(randomPlayerAbility))
            {
                randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            }
            PlayerAbilities.Add(randomPlayerAbility);
            if (randomPlayerAbility == 0)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<CrazedAlchemist>();
            }
            if (randomPlayerAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<Doppelganger>();
            }
            if (randomPlayerAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<Revive>();
            }
            if (randomPlayerAbility == 3)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<ScaredCat>();
            }
            if (randomPlayerAbility == 4)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<Squeler>();
            }
            if (randomPlayerAbility == 5)
            {
                GameObject.FindGameObjectWithTag("Player3").AddComponent<ThirdEye>();
            }
        }

        //spawn player 1 if murderer in butcher room
        else if (player3Spawned == false && randomMurdererNumber == 2)
        {
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_3"), buctherRoom.transform.position, Quaternion.identity);
            //move player 3 camera to same position as player3
            player3Camera.transform.position = new Vector3(buctherRoom.transform.position.x, buctherRoom.transform.position.y, -15);
            player3Spawned = true;
        }

        //spawn player4 if not murderer in a random room not containing another player
        if (player4Spawned == false && randomMurdererNumber != 3)
        {
            random = Random.Range(0, randomMax);
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_4"), RoomPositions[random].position, Quaternion.identity);
            //move player 4 camera to same position as player4
            player4Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            //remove room from list so that nothing else can spawn in that same room
            RoomPositions.RemoveAt(random);
            //reduce randomMax by 1 so that it matches the size of the roomPositions list
            randomMax -= 1;
            player4Spawned = true;
            //give player randomly generated ability
            randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            //makes sure the same player ability doesn't get repeated
            while (PlayerAbilities.Contains(randomPlayerAbility))
            {
                randomPlayerAbility = Random.Range(0, numberOfPlayerAbilities);
            }
            PlayerAbilities.Add(randomPlayerAbility);
            if (randomPlayerAbility == 0)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<CrazedAlchemist>();
            }
            if (randomPlayerAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<Doppelganger>();
            }
            if (randomPlayerAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<Revive>();
            }
            if (randomPlayerAbility == 3)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<ScaredCat>();
            }
            if (randomPlayerAbility == 4)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<Squeler>();
            }
            if (randomPlayerAbility == 5)
            {
                GameObject.FindGameObjectWithTag("Player4").AddComponent<ThirdEye>();
            }
        }

        //spawn player 1 if murderer in butcher room
        else if (player4Spawned == false && randomMurdererNumber == 3)
        {
            //instantiate from resources folder
            Instantiate(Resources.Load("MainPlayer_4"), buctherRoom.transform.position, Quaternion.identity);
            //move player 4 camera to same position as player4
            player4Camera.transform.position = new Vector3(buctherRoom.transform.position.x, buctherRoom.transform.position.y, -15);
            player4Spawned = true;
        }

        //spawm orb in random room not containing a player
        if (orbsSpawned < 3) {
            random = Random.Range(0, randomMax);
            //prevent orb from spawning in altar room
            if (RoomPositions[random].name != "Altar Room(Clone)")
            {
                //instantiate from resources folder
                Instantiate(Resources.Load("Orb"), RoomPositions[random].position, Quaternion.identity);
                //remove room from list so that nothing else can spawn in that same room
                RoomPositions.RemoveAt(random);
                //reduce randomMax by 1 so that it matches the size of the roomPositions list
                randomMax -= 1;
                orbsSpawned += 1;
            }
        }

        //enable murderer script if player is the murderer
        if (randomMurdererNumber == 0 && murdererSpawned == false)
        {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<MurdererScripts>().enabled = true;
            murdererSpawned = true;
        }
        if (randomMurdererNumber == 1 && murdererSpawned == false)
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<MurdererScripts>().enabled = true;
            murdererSpawned = true;
        }
        if (randomMurdererNumber == 2 && murdererSpawned == false)
        {
            GameObject.FindGameObjectWithTag("Player3").GetComponent<MurdererScripts>().enabled = true;
            murdererSpawned = true;
        }
        if (randomMurdererNumber == 3 && murdererSpawned == false)
        {
            GameObject.FindGameObjectWithTag("Player4").GetComponent<MurdererScripts>().enabled = true;
            murdererSpawned = true;
        }

        if (murdererHasAbility == false && murdererSpawned == true)
        {
            //give murderer player randomly generated ability
            
            if (randomMurdererAbility == 0)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<GoodNose>();
            }

           else  if (randomMurdererAbility == 1)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<Masterofdisguise>();
            }

           else if (randomMurdererAbility == 2)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<Polterkamikaze>();
            }

           else if (randomMurdererAbility == 3)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<ShittyPossum>();
            }

           else if (randomMurdererAbility == 4)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<ThrillOfTheHunt>();
            }

           else if (randomMurdererAbility == 5)
            {
                GameObject.FindGameObjectWithTag("Murderer" + murdererPlayerNumber).AddComponent<Traps>();
            }
            murdererHasAbility = true;
        }
    }
}
