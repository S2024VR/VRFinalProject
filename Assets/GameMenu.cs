using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
