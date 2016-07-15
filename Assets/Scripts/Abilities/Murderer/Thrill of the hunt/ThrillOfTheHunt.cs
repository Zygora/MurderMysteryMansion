using UnityEngine;
using System.Collections;

public class ThrillOfTheHunt : MonoBehaviour
{
    public float boostTime = 30.0f;     // Time showing for how long bonus will be active
    public float speedBoost = 20.0f;    // The amount of bonus speed
    float boostActiveTimeLeft;  // Time until bonus is up
    public bool bonusActive;           // Flag showing wether or not bonus is active

    // Update is called once per frame
    void Update()
    {
        // Time left before bonus
        if (boostActiveTimeLeft > 0)
        {
            boostActiveTimeLeft -= Time.deltaTime;
        }
        // If bonus is active and time is up
        if ((boostActiveTimeLeft <= 0) && (bonusActive))
        {
            gameObject.GetComponent<Controls>().playerSpeed -= speedBoost;
            bonusActive = false;
            boostActiveTimeLeft = 0;
        }
        // If took down a wimp
        
    }
    public void ActivateBonus()
    {
            gameObject.GetComponent<Controls>().playerSpeed += speedBoost;
            bonusActive = true;
            boostActiveTimeLeft = boostTime;
    }
}
