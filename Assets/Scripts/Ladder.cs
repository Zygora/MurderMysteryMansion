using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    private Controls controls;
    public GameObject player;
    public GameObject topPlatform;
    void Start()
    {
        controls = player.GetComponent<Controls>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            controls.topPlatform = topPlatform;
        }
    }


}
