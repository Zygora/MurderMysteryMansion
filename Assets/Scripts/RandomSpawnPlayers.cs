using UnityEngine;
using System.Collections.Generic;

public class RandomSpawnPlayers : MonoBehaviour {
    public List<Transform> RoomPositions;
    public bool player1Spawned = false;
    public bool player2Spawned = false;
    public bool player3Spawned = false;
    public bool player4Spawned = false;
    private GameObject player1Camera;
    private GameObject player2Camera;
    private GameObject player3Camera;
    private GameObject player4Camera;
    private int random;
    private int randomMax;
    // Use this for initialization
    void Start () {
        RoomPositions = new List<Transform>();
        for (int x = 0; x < 25; x++)
        {
            RoomPositions.Add(this.gameObject.transform.GetChild(x));
        }
       randomMax = 25;

        player1Camera = GameObject.FindGameObjectWithTag("Player1Camera");
        player2Camera = GameObject.FindGameObjectWithTag("Player2Camera");
        player3Camera = GameObject.FindGameObjectWithTag("Player3Camera");
        player4Camera = GameObject.FindGameObjectWithTag("Player4Camera");
    }
	
	// Update is called once per frame
	void Update () {
        if(player1Spawned == false) {
            random = Random.Range(0, randomMax);
            Instantiate(Resources.Load("MainPlayer_1"), RoomPositions[random].position, Quaternion.identity);
            player1Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            RoomPositions.RemoveAt(random);
            randomMax -= 1;
            player1Spawned = true;
        }

        if (player2Spawned == false)
        {
            random = Random.Range(0, randomMax);
            Instantiate(Resources.Load("MainPlayer_2"), RoomPositions[random].position, Quaternion.identity);
            player2Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            RoomPositions.RemoveAt(random);
            randomMax -= 1;
            player2Spawned = true;
        }

        if (player3Spawned == false)
        {
            random = Random.Range(0, randomMax);
            Instantiate(Resources.Load("MainPlayer_3"), RoomPositions[random].position, Quaternion.identity);
            player3Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            RoomPositions.RemoveAt(random);
            randomMax -= 1;
            player3Spawned = true;
        }

        if (player4Spawned == false)
        {
            random = Random.Range(0, randomMax);
            Instantiate(Resources.Load("MainPlayer_4"), RoomPositions[random].position, Quaternion.identity);
            player4Camera.transform.position = new Vector3(RoomPositions[random].position.x, RoomPositions[random].position.y, -15);
            RoomPositions.RemoveAt(random);
            randomMax -= 1;
            player4Spawned = true;
        }

    }
}
