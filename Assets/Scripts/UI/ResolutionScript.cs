using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour {
    public Dropdown dropdown;

    public void ChangeResolution()
    {
        int index = dropdown.value;
        if(index == 0) {
            if (Screen.fullScreen)
                Screen.SetResolution(1280, 720, true);
            else
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
        if (index == 1)
        {
            if (Screen.fullScreen)
                Screen.SetResolution(1366, 768, true);
            else
            {
                Screen.SetResolution(1366, 768, false);
            }
        }
        if (index == 2)
        {
            if (Screen.fullScreen)
                Screen.SetResolution(1600, 900, true);
            else
            {
                Screen.SetResolution(1600, 900, false);
            }
        }
        if (index == 3)
        {
            if (Screen.fullScreen)
                Screen.SetResolution(1920, 1080, true);
            else
            {
                Screen.SetResolution(1920, 1080, false);
            }
        }
        if (index == 4)
        {
            if (Screen.fullScreen)
                Screen.SetResolution(2560, 1440, true);
            else
            {
                Screen.SetResolution(2560, 1440, false);
            }
        }
        print(Screen.currentResolution);
    }
    public void ChageScreenMode()
    {
        if(Screen.fullScreen)
        {
            Screen.SetResolution(Screen.width, Screen.height, false);
        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
    }
}
