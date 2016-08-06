using UnityEngine;
using System.Collections;

public class ThrillOfTheHunt : MonoBehaviour
{
    public float boostTime = 30.0f;     // Time showing for how long bonus will be active
    public float speedBoost = 20.0f;    // The amount of bonus speed
    float boostActiveTimeLeft;  // Time until bonus is up
    public bool bonusActive;           // Flag showing wether or not bonus is active

    void Start() {
        MurdererScripts.thrillActive = true;
        this.gameObject.GetComponent<Controls>().thrillSpeedBoost = speedBoost;
        this.gameObject.GetComponent<MurdererScripts>().thrillTime = boostTime;
        this.gameObject.GetComponent<MurdererScripts>().originalThrillTime = boostTime;
    }

}
