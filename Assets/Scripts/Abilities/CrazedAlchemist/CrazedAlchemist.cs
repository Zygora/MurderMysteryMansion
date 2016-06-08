using UnityEngine;
using System.Collections;

public class CrazedAlchemist : MonoBehaviour
{
    public int numberOfPotionsAvailable; // The number of potions available to a wimp to use
    public float potionCooldown = 5;     // Time that must pass before the next potion can be thrown
    float timeSincePotionUsed;           // Time that passed since a potion was last used
    bool potionUsed;                     // Flag that shows if the potion was used

    void Update()
    {
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
        if ((Input.GetKeyDown(KeyCode.P)) && (!potionUsed))
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
                        GameObject potion = Instantiate(Resources.Load("RedPotionLeft") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potion.transform.position = spawn;
                    }
                    else
                    // If player moves right throw potion to right
                    {
                        GameObject potion = Instantiate(Resources.Load("RedPotion") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x += 1;
                        potion.transform.position = spawn;
                    }
                }
                // Throw blue potion
                else
                {
                    // If player moves left throw potion to left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotionLeft") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potionBlue.transform.position = spawn;
                    }
                    // If player moves right throw potion to right
                    else
                    {
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotion") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x += 1;
                        potionBlue.transform.position = spawn;
                    }
                }
                // Decrease the amount of potions available by one
                numberOfPotionsAvailable--;
            }
        }
    }
}
