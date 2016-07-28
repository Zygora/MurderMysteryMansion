using UnityEngine;
using System.Collections;

public class ThirdEye : MonoBehaviour
{
    public Vector2 currentRoomPos;
    public Vector2 LeftRoomPos;
    public Vector2 RightRoomPos;
    public Vector2 UpRoomPos;
    public Vector2 BottomRoomPos;
    public GameObject arrow;
    public float thirdEyeCooldown = 1;
    private float timepassed;
    public Vector3 arrowSpawn;
    private int leftArrow = 0;
    private int rightArrow = 0;
    private int upArrow = 0;
    private int downArrow = 0;

    void Start() {
        arrow = Resources.Load("Arrow", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            if (LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
            LeftRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrowLeft();
            }

            if (RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer3Pos ||
            RightRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrowRight();
            }

            if (UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer3Pos ||
            UpRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrowUp();
            }

            if ( BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
            BottomRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrowDown();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Room" || other.gameObject.tag == "MurdererStart") {
            currentRoomPos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            LeftRoomPos = currentRoomPos - new Vector2(1, 0);
            RightRoomPos = currentRoomPos + new Vector2(1, 0);
            UpRoomPos = currentRoomPos + new Vector2(0, 1);
            BottomRoomPos = currentRoomPos - new Vector2(0, 1);
        }
    }

    void CreateArrowLeft() {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x-20, this.transform.position.y + 60, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(arrow1.gameObject, 1f);
        leftArrow += 1;
    }

    void CreateArrowRight()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x + 20, this.transform.position.y + 60, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 180);
        Destroy(arrow1.gameObject, 1f);
        rightArrow += 1;
    }

    void CreateArrowDown()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 50, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 90);
        Destroy(arrow1.gameObject, 1f);
        downArrow += 1;
    }

    void CreateArrowUp()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 80, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 270);
        Destroy(arrow1.gameObject, 1f);
        upArrow += 1;
    }
}
