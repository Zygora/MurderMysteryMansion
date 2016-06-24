using UnityEngine;
using System.Collections;


public class PlayerColors : MonoBehaviour
{
    public Color32 color;
    public GameObject[] gameObjects;
    public void changePlayerColor()
    {
        gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        for (int i=0;i<gameObjects.Length;i++)
        {
            if(gameObjects[i].name.Contains("Shirt"))
            {
                if (gameObjects[i].GetComponent<SpriteRenderer>() != null)
                {
                    gameObjects[i].GetComponent<SpriteRenderer>().color = color;
                }
            }
        } 
    }


}
