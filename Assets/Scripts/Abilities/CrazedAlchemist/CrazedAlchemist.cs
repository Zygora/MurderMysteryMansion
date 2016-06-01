using UnityEngine;
using System.Collections;

public class CrazedAlchemist : MonoBehaviour {
    public GameObject redPotionLeft;
    public GameObject redPotion;
    public GameObject bluePotionLeft;
    public GameObject bluePotion;
    Vector3 potionStartPoint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            potionStartPoint = gameObject.transform.position;
            float a = Random.Range(-10.0f, 10.0f);
            if(a<=0)
            {
                // player moves left
                if (gameObject.GetComponent<Controls>().direction < 0)
                {
                    GameObject potion = Instantiate(redPotionLeft);
                    potion.transform.position = gameObject.transform.position;
                }
                else
                {
                    GameObject potion = Instantiate(redPotion);
                    potion.transform.position = gameObject.transform.position;

                }
            }
            else
            {
                // player moves left
                if (gameObject.GetComponent<Controls>().direction < 0)
                {
                    GameObject potionBlue = Instantiate(bluePotionLeft);
                    potionBlue.transform.position = gameObject.transform.position;

                }
                else
                {
                    GameObject potionBlue = Instantiate(bluePotion);
                    potionBlue.transform.position = gameObject.transform.position;

                }
            }
        }
    }
}
