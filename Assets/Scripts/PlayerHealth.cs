using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int score = 0;
    public Text healthDisplay;
    public Text scoreDisplay; // Add this line
    private Text deathScreenText;  // Reference to the Text component on the DeathText GameObject
    public GameObject deathScreen;
    public GameObject gun;
    private Pistol pistol;

    void Start()
    {
        // Find the CountdownTimer script and subscribe to the OnCountdownFinished event
        CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
        countdownTimer.OnCountdownFinished.AddListener(DisplayDeathScreen);
        currentHealth = maxHealth;
        healthDisplay.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        pistol = gun.GetComponent<Pistol>();
        scoreDisplay.text = "Score: " + score.ToString(); // Initialize scoreDisplay
        deathScreenText = deathScreen.transform.Find("DeathText").GetComponent<Text>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthDisplay.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void DisplayDeathScreen()
    {
        deathScreenText.text = "You won!"; // Include the score in the death screen text
        deathScreenText.color = Color.green;
        deathScreen.SetActive(true);  // Display the death screen
    }

    void Die()
    {
        deathScreenText.text = "You died!";  // Include the score in the death screen text
        deathScreen.SetActive(true);
        pistol.EmptyAmmo();
    }


    // Add this function to update the score
    public void UpdateScore(int newScore)
    {
        scoreDisplay.text = "Score: " + newScore.ToString();
    }
}
