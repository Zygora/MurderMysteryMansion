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
    // Use this for initialization
    void Start () {
        isMurderer = true;
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
	
	// Update is called once per frame
	void Update () {
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

        if (Input.GetButtonDown(attack)) {
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
        if (other.gameObject.tag == "Sink" && Input.GetButtonDown(interact)) {
            Debug.Log("clothes washed");
            if (gameObject.tag == "Murderer1")
            {
                GameObject.FindGameObjectWithTag("MurdererShirt1").GetComponent<Animator>().enabled = false;
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

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sink" && Input.GetButtonDown(interact))
        {
            Debug.Log("clothes washed");
            if (gameObject.tag == "Murderer1")
            {
                GameObject.FindGameObjectWithTag("MurdererShirt1").GetComponent<Animator>().enabled = false;
                Shirts = GameObject.FindGameObjectsWithTag("Blood");
                for (int i = 0; i < Shirts.Length; i++)
                {
                    Shirts[i].GetComponent<SpriteRenderer>().enabled = false;
                }
                if (Controls.bloodStained == false)
                {
                    TorsoAnimator.Play("TorsoMurdererWash");
                }

                if (Controls.bloodStained == true) {
                    TorsoAnimator.Play("TorsoMurdererBloodyWash");
                    Controls.bloodStained = false;
                }

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

        }
    }
}
