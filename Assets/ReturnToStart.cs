using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStart : MonoBehaviour
{
    // Assign the index of the start scene in the Build Settings
    public int startSceneIndex = 0;

    // Update is called once per frame
    void Update()
    {
        // Check if the left menu button is pressed
        if (Input.GetKeyDown(KeyCode.JoystickButton6)) // You may need to change this based on your VR setup
        {
            // Load the start scene
            SceneManager.LoadScene(startSceneIndex);
        }
    }
}