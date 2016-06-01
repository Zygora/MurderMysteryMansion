using UnityEngine;
using System.Collections;

public class CrazedAlchemist : MonoBehaviour
{
    public GameObject redPotionLeft;
    public GameObject redPotion;
    public GameObject bluePotionLeft;
    public GameObject bluePotion;
    Vector3 potionStartPoint;
    public int numberOfPotions;
    public float potionCooldown = 5;
    float timeSincePotionUsed;
    bool potionUsed;

    void Update()
    {
        if(potionUsed)
        {
            timeSincePotionUsed += Time.deltaTime;
            if(timeSincePotionUsed>potionCooldown)
            {
                timeSincePotionUsed = 0;
                potionUsed = false;
            }
        }
        if ((Input.GetKeyDown(KeyCode.P))&&(!potionUsed))
        {
            potionUsed = true;
            if (numberOfPotions > 0)
            {
                potionStartPoint = gameObject.transform.position;
                float a = Random.Range(-10.0f, 10.0f);
                if (a <= 0)
                {
                    // player moves left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        GameObject potion = Instantiate(redPotionLeft);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potion.transform.position = spawn;
                    }
                    else
                    {
                        GameObject potion = Instantiate(redPotion);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x += 1;
                        potion.transform.position = spawn; 
                    }
                }
                else
                {
                    // player moves left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        GameObject potionBlue = Instantiate(bluePotionLeft);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potionBlue.transform.position = spawn;
                    }
                    else
                    {
                        GameObject potionBlue = Instantiate(bluePotion);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x += 1;
                        potionBlue.transform.position = spawn;
                    }
                }
                numberOfPotions--;
            }
        }
    }
}
