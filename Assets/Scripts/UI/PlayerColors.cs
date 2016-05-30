using UnityEngine;
using System.Collections;


public class PlayerColors : MonoBehaviour {
    public GameObject player;
    public Color32 color;

    public void changePlayerColor()
    {
        player.GetComponent<SpriteRenderer>().color = color;
    }
}
