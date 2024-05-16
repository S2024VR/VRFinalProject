using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneSelectionManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject startPanel;
    public Button[] levelButtons; // Assuming you have buttons for each level

    // Dictionary to map display names to scene names
    private Dictionary<string, string> sceneNames = new Dictionary<string, string>()
    {
        {"Desert", "Anthony"},
        {"Trench", "Yzer"},
        {"Bunker", "James"},
        {"Mountain", "Wei"},
        {"Forest", "ZG"}
    };

    private Button selectedButton; // Keep track of the currently selected button

    private void Start()
    {
        // Hide startPanel initially
        startPanel.SetActive(false);
        
        // Hide all level buttons initially
        foreach (Button button in levelButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Show startPanel and hide menuPanel
        startPanel.SetActive(true);
        menuPanel.SetActive(false);
        
        // Show level buttons
        foreach (Button button in levelButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void SelectScene(Button button)
    {
        // Update selected button appearance
        if (selectedButton != null)
        {
            selectedButton.GetComponent<Image>().color = Color.white; // Reset previous button color
        }
        selectedButton = button;
        selectedButton.GetComponent<Image>().color = Color.green; // Highlight current button
    }

    public void LoadScene(string sceneDisplayName)
    {
        // Load the selected scene using the dictionary
        if (sceneNames.ContainsKey(sceneDisplayName))
        {
            SceneManager.LoadScene(sceneNames[sceneDisplayName]);
        }
        else
        {
            Debug.LogError("Scene " + sceneDisplayName + " not found in sceneNames dictionary.");
        }
    }

    public void QuitGame()
    {
        // Hide startPanel and show menuPanel
        startPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
