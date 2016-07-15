using UnityEngine;
using System.Collections;

public class SpawnPlayers : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    private bool player2Spawned = false;
    private bool player3Spawned = false;
    private bool player4Spawned = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Spawn_P2") && player2Spawned == false) {
            GameObject clone = Instantiate(player2, this.transform.position, Quaternion.identity) as GameObject;
            player2Spawned = true;
        }

        if (Input.GetButtonDown("Spawn_P3") && player3Spawned == false)
        {
            Instantiate(player3, this.transform.position, Quaternion.identity);
            player3Spawned = true;
        }

        if (Input.GetButtonDown("Spawn_P4") && player4Spawned == false)
        {
            Instantiate(player4, this.transform.position, Quaternion.identity);
            player4Spawned = true;
        }
    }
}
