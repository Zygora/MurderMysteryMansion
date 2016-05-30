using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text title;

    public GameObject controlsButton;
    public GameObject videoButton;
    public GameObject audioButton;

    public GameObject audioScrollText;
    public GameObject audioScroll;
    public GameObject audioBackButton;

    public GameObject videoResolutionText;
    public GameObject videoResolutionDropDown;
    public GameObject videoBackButton;
    public GameObject videoScreenmodeText;
    public GameObject videoModeDropDown;

    public GameObject controlsBackButton;

    public void audioPressed()
    {
        title.text = "Audio";
        controlsButton.SetActive(false);
        videoButton.SetActive(false);
        audioButton.SetActive(false);
        audioScrollText.SetActive(true);
        audioScroll.SetActive(true);
        audioBackButton.SetActive(true);
    }
    public void audioBackPressed()
    {
        title.text = "Options";
        controlsButton.SetActive(true);
        videoButton.SetActive(true);
        audioButton.SetActive(true);
        audioScrollText.SetActive(false);
        audioScroll.SetActive(false);
        audioBackButton.SetActive(false);
    }
    public void videoPressed()
    {
        title.text = "Video";
        controlsButton.SetActive(false);
        videoButton.SetActive(false);
        audioButton.SetActive(false);
        videoResolutionText.SetActive(true);
        videoResolutionDropDown.SetActive(true);
        videoBackButton.SetActive(true);
        videoScreenmodeText.SetActive(true);
        videoModeDropDown.SetActive(true);
    }
    public void videoBackPressed()
    {
        title.text = "Options";
        controlsButton.SetActive(true);
        videoButton.SetActive(true);
        audioButton.SetActive(true);
        videoResolutionText.SetActive(false);
        videoResolutionDropDown.SetActive(false);
        videoBackButton.SetActive(false);
        videoScreenmodeText.SetActive(false);
        videoModeDropDown.SetActive(false);
    }
    public void controlsPressed()
    {
        title.text = "Controls";
        controlsButton.SetActive(false);
        videoButton.SetActive(false);
        audioButton.SetActive(false);
        controlsBackButton.SetActive(true);
    }
    public void controlsBackPressed()
    {
        title.text = "Options";
        controlsButton.SetActive(true);
        videoButton.SetActive(true);
        audioButton.SetActive(true);
        controlsBackButton.SetActive(false);
    }
}
