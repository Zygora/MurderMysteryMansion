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

    void Start() {
        arrow = Resources.Load("Arrow", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            if (currentRoomPos == Controls.currentPlayer2Pos || currentRoomPos == Controls.currentPlayer3Pos ||
            currentRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrow();
            }

            if (LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
            LeftRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrow();
            }

            if (RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer3Pos ||
            RightRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrow();
            }

            if (UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer3Pos ||
            UpRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrow();
            }

            if ( BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
            BottomRoomPos == Controls.currentPlayer4Pos)
            {
                CreateArrow();
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

    void CreateArrow() {
        arrowSpawn = new Vector3(this.transform.position.x, this.transform.position.y + 60, this.transform.position.z);
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = arrowSpawn;
        Vector3 dir1 = arrowSpawn - gameObject.transform.position;
        float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
        arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);
    }
}
