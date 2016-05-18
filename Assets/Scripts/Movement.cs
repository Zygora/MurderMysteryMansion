using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    Vector3 move;

    public float speed = 1.0f;
    Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    void Update()
    {

        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.position += move * speed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
        {
            Animator.SetBool("Running", false);
            Animator.SetBool("Idle", true);
        }

        if (Input.GetAxis("Horizontal") < - 0.1f)
        {
            Animator.SetBool("Running", true);
            Animator.SetBool("Idle", false);
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            Animator.SetBool("Running", true);
            Animator.SetBool("Idle", false);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Animator.SetBool("Dead", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("Dead", false);
        }
    }
}
