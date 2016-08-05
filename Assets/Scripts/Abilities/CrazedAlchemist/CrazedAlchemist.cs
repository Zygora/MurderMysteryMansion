using UnityEngine;
using System.Collections;

public class CrazedAlchemist : MonoBehaviour
{
    public int numberOfPotionsAvailable = 3; // The number of potions available to a wimp to use
    public float potionCooldown = 5;     // Time that must pass before the next potion can be thrown
    float timeSincePotionUsed;           // Time that passed since a potion was last used
    bool potionUsed;                     // Flag that shows if the potion was used
    private string ability;
    public float potionRefreshTime = 10;
    private float potionRefreshStart;

    void Start() {
        //set input from input manager
        if (gameObject.tag == "Player1")
        {
            ability = "Ability_P1";
        }
        
        if (gameObject.tag == "Player2")
        {
            ability = "Ability_P2";
        }

        if (gameObject.tag == "Player3")
        {
            ability = "Ability_P3";
        }

        if (gameObject.tag == "Player4")
        {
            ability = "Ability_P4";
        }

        this.gameObject.GetComponent<Controls>().crazedAlchemist = true;
    }

    void Update()
    {
        if (numberOfPotionsAvailable < 3)
        {
            potionRefreshStart += Time.deltaTime;
            if(potionRefreshStart > potionRefreshTime)
            {
                numberOfPotionsAvailable += 1;
                potionRefreshStart = 0;
            }
        }
        // Counting time passed since potion was last used
        if (potionUsed)
        {
            timeSincePotionUsed += Time.deltaTime;
            // If time passed since last potion use is more than cooldown allow player to use potions again
            if (timeSincePotionUsed > potionCooldown)
            {
                timeSincePotionUsed = 0;
                potionUsed = false;
            }
        }
        // If player presses P and potion is not on cooldown
        if ((Input.GetButtonDown(ability)) && (!potionUsed))
        {
            potionUsed = true;
            // And if player can use potions
            if (numberOfPotionsAvailable > 0)
            {
                // Randomly choose red or blue potion with a 50/50 chance
                float a = Random.Range(-10.0f, 10.0f);
                // Throw red potion
                if (a <= 0)
                {
                    // If player moves left throw potion to left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        // Load potion prefab from Resources folder
                        GameObject potion = Instantiate(Resources.Load("RedPotionLeft"),this.transform.position + (transform.right * 20) + (transform.up * 20 ), Quaternion.identity) as GameObject;
                    }
                    else
                    // If player moves right throw potion to right
                    {
                        // Load potion prefab from Resources folder
                        GameObject potion = Instantiate(Resources.Load("RedPotion"), this.transform.position + (transform.right * 20) + (transform.up * 20), Quaternion.identity) as GameObject;
                    }
                }
                // Throw blue potion
                else
                {
                    // If player moves left throw potion to left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        // Load potion prefab from Resources folder
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotionLeft"), this.transform.position + (transform.right * 20) + (transform.up * 20), Quaternion.identity) as GameObject;
                    }
                    // If player moves right throw potion to right
                    else
                    {
                        // Load potion prefab from Resources folder
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotion"), this.transform.position + (transform.right * 20) + (transform.up * 20), Quaternion.identity) as GameObject;
                    }
                }
                // Decrease the amount of potions available by one
                numberOfPotionsAvailable--;
            }
        }
    }
}
