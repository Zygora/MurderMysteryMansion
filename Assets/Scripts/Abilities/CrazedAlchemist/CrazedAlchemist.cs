using UnityEngine;
using System.Collections;

public class CrazedAlchemist : MonoBehaviour
{
    Vector3 potionStartPoint;
    public int numberOfPotionsAvailable;
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
            if (numberOfPotionsAvailable > 0)
            {
                potionStartPoint = gameObject.transform.position;
                float a = Random.Range(-10.0f, 10.0f);
                if (a <= 0)
                {
                    // player moves left
                    if (gameObject.GetComponent<Controls>().direction < 0)
                    {
                        GameObject potion = Instantiate(Resources.Load("RedPotionLeft") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potion.transform.position = spawn;
                    }
                    else
                    {
                        GameObject potion = Instantiate(Resources.Load("RedPotion") as GameObject);
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
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotionLeft") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x -= 1;
                        potionBlue.transform.position = spawn;
                    }
                    else
                    {
                        GameObject potionBlue = Instantiate(Resources.Load("BluePotion") as GameObject);
                        Vector3 spawn = gameObject.transform.position;
                        spawn.x += 1;
                        potionBlue.transform.position = spawn;
                    }
                }
                numberOfPotionsAvailable--;
            }
        }
    }
}
