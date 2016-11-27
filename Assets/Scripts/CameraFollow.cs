using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public string playerName;

    private Vector3 offset;
    private GameObject player;
    private bool findGameobject = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(findGameobject == false)
        {
            player = GameObject.Find(playerName);
            offset = this.transform.position - player.transform.position;
            offset += new Vector3(0, 16, 0);
            findGameobject = true;
        }
        transform.position = player.transform.position + offset;
    }
}
