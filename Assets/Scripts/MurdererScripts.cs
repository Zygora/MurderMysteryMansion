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
    public static bool diseased = false;
    public static bool thrill = false;
    public static bool thrillActive = false;
    public float diseasedTime;
    public float thrillTime;
    private bool arrowCreated;
    public float originalDiseasedTime;
    public float originalThrillTime;
    // Use this for initialization
    void Start () {
        isMurderer = true;
        //change inputs in input manager
        //change tag of player
        if (gameObject.tag == "Murderer1")
        {
            attack = "Attack/Revive_P1";
            interact = "Interact_P1";
        }

        if (gameObject.tag == "Murderer2")
        {
            attack = "Attack/Revive_P2";
            interact = "Interact_P2";
        }

        if (gameObject.tag == "Murderer3")
        {
            attack = "Attack/Revive_P3";
            interact = "Interact_P3";
        }

        if (gameObject.tag == "Murderer4")
        {
            attack = "Attack/Revive_P4";
            interact = "Interact_P4";
        }
        gameObject.layer = 9;
        originalDiseasedTime = diseasedTime;
	}

	// Update is called once per frame
	void Update () {
        
        if (Controls.Trapped == true && arrowCreated == false)
        {
            GameObject arrow = Instantiate(Resources.Load("TrapArrow"), this.transform.position + (transform.up*60), Quaternion.identity) as GameObject;
            Vector3 dir = GameObject.FindGameObjectWithTag("Trap").transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle- 180, Vector3.forward);
            arrowCreated = true;
            Destroy(arrow.gameObject, 2f);
        }

        if(Controls.Trapped == false)
        {
            arrowCreated = false;
        }

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
            TorsoAnimator.Play("TorsoDizzy");
            LegsAnimator.Play("LegsDeath");
            TorsoAnimator.SetBool("Dizzy", true);
            ShirtAnimator.SetBool("Dizzy", true);
            if (diseasedTime<=0)
            {
                diseased = false;
                diseasedTime = originalDiseasedTime;
                TorsoAnimator.SetBool("Dizzy", false);
                ShirtAnimator.SetBool("Dizzy", false);
            }
        }

        if (thrill)
        {
            thrillTime -= Time.deltaTime;
            if (thrillTime <= 0)
            {
                thrill = false;
                thrillTime = originalThrillTime;
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
            if (hit.collider.tag == "Ground" && hit.distance < 12f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", false);
            }
            // otherwise the player is in the air
            else if (hit.distance >= 12f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //check collision with sink
        if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
        {
            if (other.gameObject.tag == "Sink" && Input.GetButtonDown(interact) && ShittyPossum.possumed == false)
            {
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
    }

    //function to make washing clothes false used as a global static variable to disable movement while washing clothes
    void StopWashingClothes() {
        washingClothes = false;
    }
}
