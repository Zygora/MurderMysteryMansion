using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {
    private int[] colorArray;
    public GameObject[] colorObjects;
    public GameObject[] gameObjects;
    public int selected;
    public bool playerCanSelect;
    public Color32 color;
    public Image image;
    bool changed;
    // Use this for initialization
    void Start () {
        colorArray = new int[11];
        selected = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (playerCanSelect)
        {
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                selected++;
                if (selected > 10)
                {

                    selected = 0;
                    changed = true;
                }
                else
                {

                    changed = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                selected--;
                if (selected < 0)
                {

                    selected = 10;
                    changed = true;
                }
                else
                {
                    changed = true;
                }
            }
            if (changed)
            {
                image.GetComponent<Image>().color =
                                colorObjects[selected].GetComponent<Image>().color;
                gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].name.Contains("Shirt"))
                    {
                        if (gameObjects[i].GetComponent<SpriteRenderer>() != null)
                        {
                            gameObjects[i].GetComponent<SpriteRenderer>().color =
                                colorObjects[selected].GetComponent<Image>().color;
                        }
                    }
                }
                changed = false;
            }
        }
    }
}
