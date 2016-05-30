using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetCurrentRes : MonoBehaviour {
    public Dropdown dropdown;
    public Dropdown dropdown2;
    // Use this for initialization
    void Awake () {
	    if((Screen.width == 2560)&&(Screen.height == 1440))
        {
            dropdown.value = 4;
        }
        if(Screen.fullScreen)
        {
            dropdown2.value = 0;
        }
        else
        {
            dropdown2.value = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
