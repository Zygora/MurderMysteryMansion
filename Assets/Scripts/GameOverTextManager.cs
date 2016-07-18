using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverTextManager : MonoBehaviour {
    public GameObject gameOverTextP1;
    public GameObject gameOverTextP2;
    public GameObject gameOverTextP3;
    public GameObject gameOverTextP4;
    public static bool gameOver = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (OrbCount.wimpsExited == 3) {
            gameOverTextP1.GetComponent<Text>().enabled = true;
            gameOverTextP2.GetComponent<Text>().enabled = true;
            gameOverTextP3.GetComponent<Text>().enabled = true;
            gameOverTextP4.GetComponent<Text>().enabled = true;
            gameOver = true;
        }

        if (Controls.wimpsDowned == 3) {
            gameOverTextP1.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP2.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP3.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP4.GetComponent<Text>().text = "Murderer Wins";

            gameOverTextP1.GetComponent<Text>().enabled = true;
            gameOverTextP2.GetComponent<Text>().enabled = true;
            gameOverTextP3.GetComponent<Text>().enabled = true;
            gameOverTextP4.GetComponent<Text>().enabled = true;
            gameOver = true;
        }
	}
}
