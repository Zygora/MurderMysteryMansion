using UnityEngine;
using System.Collections;

public class Masterofdisguise : MonoBehaviour {
    bool nearDownedWimp;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If pressed near downed wimp copy their clothes colors
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DownedWimp")
        {
            nearDownedWimp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DownedWimp")
        {
            nearDownedWimp = false;
        }
    }
}
