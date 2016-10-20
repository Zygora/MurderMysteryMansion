using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverTextManager : MonoBehaviour {
    public GameObject gameOverTextP1;
    public GameObject gameOverTextP2;
    public GameObject gameOverTextP3;
    public GameObject gameOverTextP4;

    public float GameLengthMinutes;
    public float AbilityTextUptimeSeconds;
    public static bool gameOver = false;
    private bool wimpsWin = false;
    // Use this for initialization
    void Start () {
        //reset game values
        GameLengthMinutes = GameLengthMinutes * 60;
        Controls.wimpsDowned = 0;
        OrbCount.wimpsExited = 0;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        GameLengthMinutes -= Time.smoothDeltaTime;
        //game over if time is up
        if (GameLengthMinutes <= 0)
        {
            gameOver = true;
            wimpsWin = true;
        }
        //game over if all wimps exited. displays text
        if (OrbCount.wimpsExited == 3 || wimpsWin == true) {
            EnableText("  Wimps Win");
        }

        //game over if all wimps downed. displays text
        if (Controls.wimpsDowned == 3) {
            
            EnableText("Murderer Wins");
        }
	}

    void EnableText(string gameOverText)
    {
        gameOverTextP1.GetComponent<Text>().text = gameOverText;
        gameOverTextP2.GetComponent<Text>().text = gameOverText;
        gameOverTextP3.GetComponent<Text>().text = gameOverText;
        gameOverTextP4.GetComponent<Text>().text = gameOverText;

        gameOverTextP1.GetComponent<Text>().enabled = true;
        gameOverTextP2.GetComponent<Text>().enabled = true;
        gameOverTextP3.GetComponent<Text>().enabled = true;
        gameOverTextP4.GetComponent<Text>().enabled = true;

        Invoke("ResetLevel", 5.0f);
        gameOver = true;
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(0); // load level generator
    }
}
