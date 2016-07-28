using UnityEngine;
using System.Collections;

public class MurdererScripts : MonoBehaviour {
    public Animator ShirtAnimator;
    public Animator TorsoAnimator;
    public Animator LegsAnimator;
   
    public static bool isMurderer;
    public string horizontal;
    public string vertical;
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
            Shirts2 = GameObject.FindGameObjectsWithTag("Blood");
            for (int i = 0; i < Shirts2.Length; i++)
            {
                //disable the sprite renderer for each blood splatter in array
                Shirts2[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (ShittyPossum.possumed == false)
        {
            TorsoAnimator.SetBool("Dead", false);
            LegsAnimator.SetBool("Dead", false);
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
        //play blood animations for murderer when enabled;
        if (Input.GetAxis(horizontal) > -0.5f && Input.GetAxis(horizontal) < 0.5f && Controls.murderTransitioning == false)
        {
            ShirtAnimator.SetBool("MurdererRunning", false);
            ShirtAnimator.SetBool("MurdererIdle", true);
        }
        
        if (Input.GetAxis(horizontal) < -0.1f)
        {
            ShirtAnimator.SetBool("MurdererRunning", true);
            ShirtAnimator.SetBool("MurdererIdle", false);
        }

        if (Input.GetAxis(horizontal) > 0.1f)
        {
            ShirtAnimator.SetBool("MurdererRunning", true);
            ShirtAnimator.SetBool("MurdererIdle", false);
        }

        if (Input.GetButtonDown(attack) && Controls.wimpKilled == false) {
            TorsoAnimator.Play("TorsoMurdererAttack");
            LegsAnimator.Play("LegsMurdererAttack");
            if (Controls.bloodStained == true)
            {
                ShirtAnimator.Play("MurdererAttack");
            }
        }


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        // Debug.DrawRay(transform.position, Vector2.down, Color.red);
        // If ray hit something player is on ground
        if (hit.collider != null) 
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
            if (gameObject.tag == "Murderer1")
            {
                //disable the bloody shirt animator
                GameObject.FindGameObjectWithTag("MurdererShirt1").GetComponent<Animator>().enabled = false;
                //find all blood splatter tagged blood and put them into an array
                Shirts = GameObject.FindGameObjectsWithTag("Blood");
                for (int i = 0; i < Shirts.Length; i++)
                {
                    //disable the sprite renderer for each blood splatter in array
                    Shirts[i].GetComponent<SpriteRenderer>().enabled = false;
                }
                //play non bloody washing animation if murderer is not blood stained
                if (Controls.bloodStained == false)
                {
                    TorsoAnimator.Play("TorsoMurdererWash");
                }

                //play  bloody washing animation if murderer is not blood stained
                if (Controls.bloodStained == true)
                {
                    TorsoAnimator.Play("TorsoMurdererBloodyWash");
                    Controls.bloodStained = false;
                }
                //disable the leg sprite of player during animation
                LegsAnimator.Play("LegsMurdererWashing");
            }

            if (gameObject.tag == "Murderer2")
            {
                GameObject.FindGameObjectWithTag("MurdererShirt2").GetComponent<Animator>().enabled = false;
                Shirts = GameObject.FindGameObjectsWithTag("Blood");
                for (int i = 0; i < Shirts.Length; i++)
                {
                    Shirts[i].GetComponent<SpriteRenderer>().enabled = false;
                }

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
            }

            if (gameObject.tag == "Murderer3")
            {
                GameObject.FindGameObjectWithTag("MurdererShirt3").GetComponent<Animator>().enabled = false;
                Shirts = GameObject.FindGameObjectsWithTag("Blood");
                for (int i = 0; i < Shirts.Length; i++)
                {
                    Shirts[i].GetComponent<SpriteRenderer>().enabled = false;
                }

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
            }

            if (gameObject.tag == "Murderer4")
            {
                GameObject.FindGameObjectWithTag("MurdererShirt4").GetComponent<Animator>().enabled = false;
                Shirts = GameObject.FindGameObjectsWithTag("Blood");
                for (int i = 0; i < Shirts.Length; i++)
                {
                    Shirts[i].GetComponent<SpriteRenderer>().enabled = false;
                }

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
            }
            //makes washing clothes false after animations play
            Invoke("StopWashingClothes", 1.2f);
        }
    }

    //function to make washing clothes false used as a global static variable to disable movement while washing clothes
    void StopWashingClothes() {
        washingClothes = false;
    }
}
