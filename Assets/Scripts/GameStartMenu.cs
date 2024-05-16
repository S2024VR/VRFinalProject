using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject start;
    public GameObject options;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

    [Header("Level Selection")]
    public List<Button> levelButtons;
    public Button playButton;

    private string selectedLevelName;

    [Header("Return Buttons")]
    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Only enable the main menu at the beginning
        EnableMainMenu();

        // Hook events
        startButton.onClick.AddListener(EnableStart);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var button in levelButtons)
        {
            // Create a local variable to capture the button
            var capturedButton = button;

            // Add listener for level buttons
            button.onClick.AddListener(() => SelectLevel(capturedButton));
        }

        // Disable play button initially
        playButton.interactable = false;
        playButton.onClick.AddListener(StartGame);

        // Hook return button events
        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedLevelName))
        {
            HideAll();
            // Load the scene corresponding to the selected level
            Debug.Log(GetSceneIndex(selectedLevelName.Trim()));
            SceneTransitionManager.singleton.GoToSceneAsync(GetSceneIndex(selectedLevelName.Trim()));
        }
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        start.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        start.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableOption()
    {
        mainMenu.SetActive(false);
        start.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
    }

    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        start.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }

    public void EnableStart()
    {
        mainMenu.SetActive(false);
        start.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }

    void SelectLevel(Button button)
    {
        // Get the text component of the button to get the level name
        selectedLevelName = button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

        // Reset all buttons' colors
        foreach (Button levelButton in levelButtons)
        {
            levelButton.GetComponent<Image>().color = Color.white;
        }

        // Highlight the selected button with blue color
        button.GetComponent<Image>().color = Color.blue;

        // Enable play button
        playButton.interactable = true;
    }


    // Method to get the scene index based on the selected level name
    int GetSceneIndex(string levelName)
    {
        switch (levelName)
        {
            case "Desert":
                return 1;
            case "Trench":
                return 2;
            case "Mountain":
                return 3;
            case "Bunker":
                return 4;
            case "Forest":
                return 5;
            default:
                return 0; // Default scene index if level name not recognized
        }
    }
}
