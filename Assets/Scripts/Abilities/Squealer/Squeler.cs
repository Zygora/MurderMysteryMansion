using UnityEngine;
using System.Collections;

public class Squeler : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject arrow;
    public GameObject arrowSpawn1;
    public GameObject arrowSpawn2;
    public new Camera camera;
    public float squealerCooldown = 30;
    float timePassed;
    // Update is called once per frame
    void Update()
    {
        if(timePassed>0)
        {
            timePassed -= Time.deltaTime;
        }
        if (timePassed <= 0)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(player1.transform.position);
            if ((viewPos.x > 0) && (viewPos.y > 0) && (viewPos.x < 1) && (viewPos.y < 1))
            {
                GameObject arrow1 = Instantiate(arrow);
                arrow1.transform.parent = player1.transform;
                arrow1.transform.position = arrowSpawn1.transform.position;
                Vector3 dir1 = arrowSpawn1.transform.position - gameObject.transform.position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);

                GameObject arrow2 = Instantiate(arrow);
                arrow2.transform.parent = player2.transform;
                arrow2.transform.position = arrowSpawn2.transform.position;
                Vector3 dir2 = arrowSpawn2.transform.position - gameObject.transform.position;
                float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                arrow2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
                timePassed = squealerCooldown;
            }
            Vector3 viewPos2 = camera.WorldToViewportPoint(player2.transform.position);
            if ((viewPos2.x > 0) && (viewPos2.y > 0) && (viewPos2.x < 1) && (viewPos2.y < 1))
            {
                GameObject arrow1 = Instantiate(arrow);
                arrow1.transform.parent = player1.transform;
                arrow1.transform.position = arrowSpawn1.transform.position;
                Vector3 dir1 = arrowSpawn1.transform.position - gameObject.transform.position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                arrow1.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);

                GameObject arrow2 = Instantiate(arrow);
                arrow2.transform.parent = player2.transform;
                arrow2.transform.position = arrowSpawn2.transform.position;
                Vector3 dir2 = arrowSpawn2.transform.position - gameObject.transform.position;
                float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                arrow2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
                timePassed = squealerCooldown;
            }
        }
    }
}
