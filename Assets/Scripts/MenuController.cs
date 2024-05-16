using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VRMenuController : MonoBehaviour
{
    public GameObject menuUI;
    public Button resumeButton;
    public Button optionsButton;
    public Button exitButton;

    public Slider volumeSlider;
    public TMP_Dropdown turnDropdown;
    public SetTurnTypeFromPlayerPref turnTypeFromPlayerPref;

    void Start()
    {
        // Hook up button events
        resumeButton.onClick.AddListener(Resume);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(Exit);

        // Hide the menu initially
        menuUI.SetActive(false);

        // Set up options UI
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.value = PlayerPrefs.GetInt("turn");
    }

    void Update()
    {
        // Check for input to open the menu
        if (Input.GetButtonDown("OpenMenu"))
        {
            // Toggle menu visibility
            menuUI.SetActive(!menuUI.activeSelf);
        }
    }

    void Resume()
    {
        // Hide the menu
        menuUI.SetActive(false);
    }

    void OpenOptions()
    {
        // Show the options UI
        // You might need to implement this part according to your UI design
        volumeSlider.gameObject.SetActive(true);
        turnDropdown.gameObject.SetActive(true);
    }

    void SetGlobalVolume(float value)
    {
        // Set the global volume
        AudioListener.volume = value;
    }

    void SetTurnPlayerPref(int value)
    {
        // Set the turn player preference
        PlayerPrefs.SetInt("turn", value);
        turnTypeFromPlayerPref.ApplyPlayerPref();
    }

    void Exit()
    {
        // Load the start scene
        SceneManager.LoadScene(0);
    }
}
