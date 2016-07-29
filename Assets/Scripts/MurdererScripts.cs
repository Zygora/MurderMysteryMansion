using UnityEngine;
using System.Collections;

public class MurdererScripts : MonoBehaviour {
    public Animator ShirtAnimator;
    public Animator TorsoAnimator;
    public Animator LegsAnimator;
   
    public static bool isMurderer;
    public string attack;
    public string interact;
    private GameObject[] Shirts;
    private GameObject[] Shirts2;
    public static bool washingClothes = false;
    public bool diseased;
    public float diseasedTime;
    public float diseasedForSeconds = 10;
    public int lastPlayerWhileNotCarryingOrbSpeed;
    // Use this for initialization
    void Start () {
        lastPlayerWhileNotCarryingOrbSpeed = gameObject.GetComponent<Controls>().speedWhileNotCarryOrb;
        isMurderer = true;
        //change inputs in input manager
        //change tag of player
        if (gameObject.tag == "Player1")
        {
            gameObject.tag = "Murderer1";
            attack = "Attack/Revive_P1";
            interact = "Interact_P1";
        }

        if (gameObject.tag == "Player2")
        {
            gameObject.tag = "Murderer2";
            attack = "Attack/Revive_P2";
            interact = "Interact_P2";
        }

        if (gameObject.tag == "Player3")
        {
            gameObject.tag = "Murderer3";
            attack = "Attack/Revive_P3";
            interact = "Interact_P3";
        }

        if (gameObject.tag == "Player4")
        {
            gameObject.tag = "Murderer4";
            attack = "Attack/Revive_P4";
            interact = "Interact_P4";
        }
        gameObject.layer = 9;
	}

	public void Diseased()
    {
        
        //lastPlayerSpeed = gameObject.GetComponent<Controls>().playerSpeed;
        //gameObject.GetComponent<Controls>().playerSpeed = 0.1f;
        gameObject.GetComponent<Controls>().speedWhileNotCarryOrb = 0;
        diseasedTime = diseasedForSeconds;
        diseased = true;
    }

	// Update is called once per frame
	void Update () {
        //play dead animtion and disable bloody shirt if possum used
        if (ShittyPossum.possumed == true) {
            TorsoAnimator.SetBool("Dead", true);
            LegsAnimator.SetBool("Dead", true);
            GameObject.FindGameObjectWithTag("Player1Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player2Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player3Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player4Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
        }

        if (ShittyPossum.possumed == false)
        {
            TorsoAnimator.SetBool("Dead", false);
            LegsAnimator.SetBool("Dead", false);
            if (Controls.bloodStained == true)
            {
                GameObject.FindGameObjectWithTag("Player1Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
                GameObject.FindGameObjectWithTag("Player2Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
                GameObject.FindGameObjectWithTag("Player3Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
                GameObject.FindGameObjectWithTag("Player4Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
            }
        }
        if (diseased)
        {
            diseasedTime -= Time.deltaTime;
            if(diseasedTime<=0)
            {
                //gameObject.GetComponent<Controls>().playerSpeed = lastPlayerSpeed;
                gameObject.GetComponent<Controls>().speedWhileNotCarryOrb = lastPlayerWhileNotCarryingOrbSpeed;
                diseased = false;
            }
        }

        //play attack animations for murderer
        if (Controls.murderTransitioning == false)
        {
            if (Input.GetButtonDown(attack) && Controls.wimpKilled == false && GameOverTextManager.gameOver == false)
            {
                TorsoAnimator.Play("TorsoMurdererAttack");
                LegsAnimator.Play("LegsMurdererAttack");
                ShirtAnimator.Play("MurdererAttack");
            }
        }


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        // If ray hit something player is on ground
        if (hit.collider != null && GameOverTextManager.gameOver == false) 
        {
            if (hit.collider.tag == "Ground" && hit.distance < 1.3f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", false);
            }
            // otherwise the player is in the air
            else if (hit.distance >= 1.3f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //check collision with sink
        if (other.gameObject.tag == "Sink" && Input.GetButtonDown(interact) && ShittyPossum.possumed == false) {
            washingClothes = true;
            GameObject.FindGameObjectWithTag("Player1Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player2Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player3Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            GameObject.FindGameObjectWithTag("Player4Camera").GetComponent<Camera>().cullingMask &= ~(1 << 16);
            if (Controls.bloodStained == false)
            {
                TorsoAnimator.Play("TorsoMurdererWash");
            }

            if (Controls.bloodStained == true)
            {
                TorsoAnimator.Play("TorsoMurdererBloodyWash");
                Controls.bloodStained = false;
            }
            LegsAnimator.Play("LegsMurdererWashing");
            //makes washing clothes false after animations play
            Invoke("StopWashingClothes", 1.2f);
        }
    }

    //function to make washing clothes false used as a global static variable to disable movement while washing clothes
    void StopWashingClothes() {
        washingClothes = false;
    }
}
