using UnityEngine;
using System.Collections;

public class Squeler : MonoBehaviour
{
    public GameObject player1 = null;          // Reference to the first player          
    public GameObject player2 = null;          // Reference to the second player
    public GameObject murderer;
    public GameObject arrow;            // Arrow prefab
    public Vector3 arrowSpawn1;      // Arrow spawner for the first player
    public Vector3 arrowSpawn2;      // Arrow spawner for the second player
    public new Camera camera;           // Reference to the camera

    public float squealerCooldown = 30; // Cooldown of the ability

    float timePassed;                   // Time passed since the ability was last used

    void Start()
    {
        if (gameObject.tag == "Player1")
        {
            if (GameObject.FindGameObjectWithTag("Player2") != null)
            {
                player1 = GameObject.FindGameObjectWithTag("Player2");
            }
            if ((GameObject.FindGameObjectWithTag("Player3") != null)&&(player1==null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player3");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null)&&(player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player4");
            }

            if ((GameObject.FindGameObjectWithTag("Player3") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player3")!=player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player3");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player4") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player4");
            }
            camera = GameObject.Find("Player 1 Camera").GetComponent<Camera>();
        }

        if (gameObject.tag == "Player2")
        {
            if (GameObject.FindGameObjectWithTag("Player1") != null)
            {
                player1 = GameObject.FindGameObjectWithTag("Player1");
            }
            if ((GameObject.FindGameObjectWithTag("Player3") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player3");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player4");
            }

            if ((GameObject.FindGameObjectWithTag("Player3") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player3") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player3");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player4") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player4");
            }
            camera = GameObject.Find("Player 2 Camera").GetComponent<Camera>();
        }
        if (gameObject.tag == "Player3")
        {
            if (GameObject.FindGameObjectWithTag("Player1") != null)
            {
                player1 = GameObject.FindGameObjectWithTag("Player1");
            }
            if ((GameObject.FindGameObjectWithTag("Player2") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player2");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player4");
            }

            if ((GameObject.FindGameObjectWithTag("Player2") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player2") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player3");
            }
            if ((GameObject.FindGameObjectWithTag("Player4") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player4") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player4");
            }
            camera = GameObject.Find("Player 3 Camera").GetComponent<Camera>();
        }
        if (gameObject.tag == "Player4")
        {
            if (GameObject.FindGameObjectWithTag("Player1") != null)
            {
                player1 = GameObject.FindGameObjectWithTag("Player1");
            }
            if ((GameObject.FindGameObjectWithTag("Player2") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player2");
            }
            if ((GameObject.FindGameObjectWithTag("Player3") != null) && (player1 == null))
            {
                player1 = GameObject.FindGameObjectWithTag("Player3");
            }

            if ((GameObject.FindGameObjectWithTag("Player2") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player2") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player2");
            }
            if ((GameObject.FindGameObjectWithTag("Player3") != null) && (player2 == null) &&
                ((GameObject.FindGameObjectWithTag("Player3") != player1)))
            {
                player2 = GameObject.FindGameObjectWithTag("Player3");
            }
            camera = GameObject.Find("Player 4 Camera").GetComponent<Camera>();
        }
        if (GameObject.FindGameObjectWithTag("Murderer1") != null)
        {
            murderer = GameObject.FindGameObjectWithTag("Murderer1");
        }
        else 
        if(GameObject.FindGameObjectWithTag("Murderer2")!=null)
        {
            murderer = GameObject.FindGameObjectWithTag("Murderer2");
        }else 
        if(GameObject.FindGameObjectWithTag("Murderer3")!=null)
        {
            murderer = GameObject.FindGameObjectWithTag("Murderer3");
        }
        else
        {
            murderer = GameObject.FindGameObjectWithTag("Murderer4");
        }

        arrowSpawn1 = player1.transform.position;

        arrowSpawn2 = player2.transform.position;

        arrow = Resources.Load("Arrow", typeof(GameObject)) as GameObject;
    }

    void Update()
    {

        arrowSpawn1 = new Vector3(player1.transform.position.x, player1.transform.position.y + 60, player1.transform.position.z);
        arrowSpawn2 = new Vector3(player2.transform.position.x, player2.transform.position.y + 60, player2.transform.position.z);
        if (timePassed > 0)
        {
            timePassed -= Time.deltaTime;
        }

        if (timePassed <= 0)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(player1.transform.position);
            // If a wimp/murderer is in the view of the camera 
            if ((viewPos.x > 0) && (viewPos.y > 0) && (viewPos.x < 1) && (viewPos.y < 1))
            {
                // Draw arrows that point to the target
                GameObject arrow1 = Instantiate(arrow);
                arrow1.transform.parent = player1.transform;
                arrow1.transform.position = arrowSpawn1;
                Vector3 dir1 = arrowSpawn1 - gameObject.transform.position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);

                GameObject arrow2 = Instantiate(arrow);
                arrow2.transform.parent = player2.transform;
                arrow2.transform.position = arrowSpawn2;
                Vector3 dir2 = arrowSpawn2 - gameObject.transform.position;
                float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                arrow2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
                timePassed = squealerCooldown;
            }

            Vector3 viewPos2 = camera.WorldToViewportPoint(player2.transform.position);
            // If a wimp/murderer is in the view of the camera 
            if ((viewPos2.x > 0) && (viewPos2.y > 0) && (viewPos2.x < 1) && (viewPos2.y < 1))
            {
                // Draw arrows that point to the target
                GameObject arrow1 = Instantiate(arrow);
                arrow1.transform.parent = player1.transform;
                arrow1.transform.position = arrowSpawn1;
                Vector3 dir1 = arrowSpawn1 - gameObject.transform.position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);

                GameObject arrow2 = Instantiate(arrow);
                arrow2.transform.parent = player2.transform;
                arrow2.transform.position = arrowSpawn2;
                Vector3 dir2 = arrowSpawn2 - gameObject.transform.position;
                float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                arrow2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
                timePassed = squealerCooldown;
            }

            Vector3 viewPos3 = camera.WorldToViewportPoint(murderer.transform.position);
            // If a wimp/murderer is in the view of the camera 
            if ((viewPos3.x > 0) && (viewPos3.y > 0) && (viewPos3.x < 1) && (viewPos3.y < 1))
            {
                // Draw arrows that point to the target
                GameObject arrow1 = Instantiate(arrow);
                arrow1.transform.parent = player1.transform;
                arrow1.transform.position = arrowSpawn1;
                Vector3 dir1 = arrowSpawn1 - gameObject.transform.position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);

                GameObject arrow2 = Instantiate(arrow);
                arrow2.transform.parent = player2.transform;
                arrow2.transform.position = arrowSpawn2;
                Vector3 dir2 = arrowSpawn2 - gameObject.transform.position;
                float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                arrow2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
                timePassed = squealerCooldown;
            }
        }       
    }
}
