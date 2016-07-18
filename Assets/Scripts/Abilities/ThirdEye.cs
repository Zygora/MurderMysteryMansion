using UnityEngine;
using System.Collections;

public class ThirdEye : MonoBehaviour
{
    private float time = 0;
    private float cooldown = 0;
    private string ability;
    public int k;

    void Start() {
        //set input from input manager
        if (gameObject.tag == "Player1")
        {
            ability = "Ability_P1";
        }

        if (gameObject.tag == "Player2")
        {
            ability = "Ability_P2";
        }

        if (gameObject.tag == "Player3")
        {
            ability = "Ability_P3";
        }

        if (gameObject.tag == "Player4")
        {
            ability = "Ability_P4";
        }
    }
    void Update()
    {
           // Physics.OverlapSphere
           
       
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, k);
    }
}
