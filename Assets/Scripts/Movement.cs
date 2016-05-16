using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    Vector3 move;

    public float speed = 1.0f;

    void Update()
    {

        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.position += move * speed * Time.deltaTime;


    }
}
