using UnityEngine;
using System.Collections;

public class Squeler : MonoBehaviour
{
    public GameObject player1 = null;          // Reference to the first player          
    public GameObject player2 = null;          // Reference to the second player
    public static GameObject murderer = null;
    private GameObject[] gameObjects;
    float timePassed;                   // Time passed since the ability was last used
    public Vector2 currentPos;
    public Vector2 murdererPos;
    public bool screaming = false;
    public static bool destroyArrow = false;
    public AudioClip scream;
    public AudioSource Audio;
    private float timeSinceEnteringRoom;
    private float transitionWaitTime = 1f;
    void Start() {
        scream = gameObject.GetComponent<Controls>().scream;
        Audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timePassed < 2)
        {
            timePassed += Time.deltaTime;
        }

        if (murderer == null || player1 == null || player2 == null)
        {
            gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (player1 == null && gameObjects[i].tag.Contains("Player") &&
                    gameObjects[i] != gameObject)
                {
                    player1 = gameObjects[i];
                }
                if (player2 == null && gameObjects[i].tag.Contains("Player") &&
                    player1 != gameObjects[i] &&
                    gameObjects[i] != gameObject)
                {
                    player2 = gameObjects[i];
                }
                if (murderer == null &&
                    gameObjects[i].tag.Contains("Murderer") && (
                    gameObjects[i].GetComponent<MurdererScripts>() != null))
                {
                    murderer = gameObjects[i];
                }
            }
        }

        murdererPos = Controls.murdererPosition;
        currentPos = gameObject.GetComponent<Controls>().currentPos;

        if (currentPos == murdererPos && screaming == false && timePassed > 2) 
        {
            timeSinceEnteringRoom += Time.deltaTime;
            if(timeSinceEnteringRoom > transitionWaitTime) {
                Instantiate(Resources.Load("SquelerArrow"), player1.transform.position + (transform.up * 60), Quaternion.identity);
                Instantiate(Resources.Load("SquelerArrow"), player2.transform.position + (transform.up * 60), Quaternion.identity);
                Audio.PlayOneShot(scream);
                screaming = true;
                timeSinceEnteringRoom = 0;
            }
        }

        if(currentPos != murdererPos)
        {
            screaming = false;
        }
    }
}

