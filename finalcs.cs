// DelayedReplay.cs

using System.Collections;
using UnityEngine;

public class DelayedReplay : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float interval = 5f; // Time between each loop in seconds
    [Range(0f, 1f)][SerializeField] private float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Dynamically adding an Audiosource component
        source = gameObject.AddComponent<AudioSource>();
        // Dynamically setting the clip
        source.clip = clip;
        // Set volume
        source.volume = volume;

        // Start playing audio loop after delayInSeconds
        StartCoroutine(PlayLoopDelayed());
    }

    // Coroutine to play audio loop delayed
    IEnumerator PlayLoopDelayed()
    {
        yield return new WaitForSeconds(interval);
        PlayLoop();
    }

    // Function to play audio loop
    void PlayLoop()
    {
        // Play the audio clip
        source.Play();
        // Invoke the function again after the audio clip duration
        StartCoroutine(PlayLoopAfterDelay(source.clip.length));
    }

    // Coroutine to play audio loop after delay
    IEnumerator PlayLoopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayLoop();
    }

    // Function to set the volume of the audio source
    public void SetVolume(float vol)
    {
        volume = Mathf.Clamp01(vol);
        source.volume = volume;
    }
}



// GameEvents.cs

using UnityEngine.Events;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public UnityEvent onSupplyDropCollected;

    public void SupplyDropCollected()
    {
        if (onSupplyDropCollected != null)
        {
            onSupplyDropCollected.Invoke();
        }
    }
}



// GameMenu.cs

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



// GameMenuManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3 (head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;

    }
}



// PhysicRig.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    public Transform playerHead;
    public CapsuleCollider bodyCollider;

    public float bodyHeightMin = 0.5f;
    public float bodyHeightMax = 2;

    // Update is called once per frame
    void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height/2);
    }
}



// PlayerScore.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



// ReturnToStart.cs

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


// SceneSelector.cs

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



// DamageDealer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}


// AudioManager.cs

using UnityEngine.Audio;
using System;
using UnityEngine;

// Credit to Brackeys youtube tutorial on Audio managers, as the majority of this code and learning how to use it was made by him.
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = 1;
        [Range(-3, 3)]
        public float pitch = 1;
        public bool loop = false;
        public bool playOnAwake = false;
        [HideInInspector] public AudioSource source;

        public Sound()
        {
            volume = 1;
            pitch = 1;
            loop = false;
        }
    }

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        instance = this;

        foreach (Sound s in sounds)
        {
            if (!s.source)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            if (s.playOnAwake)
                s.source.Play();

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
}



// BulletDamageDealer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageDealer : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}


// EnemyFollow.cs

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemyfollow : MonoBehaviour, ITakeDamage
{
    public float speed = 3.0f;
    public float rotationSpeed = 1.0f;
    public Transform target;
    public float attackRange = 3f;
    public Animator animator;
    private float maxHealth = 100f;
    public float health;
    private float DamageToPlayer;
     public Slider HealthSlider;
     public GameObject supplyDrop;

    // variable for bullet collision
    // Update is called once per frame
    void Start(){
        health = maxHealth;
    }

    void Update()
    {
        //gets position of the player
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        // Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        //calculates the distance of the player vs zombie
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        Vector3 targetDirection = targetPosition - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (health <= 0f)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            Die();
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            // Player is close, switch to attack animation
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);

        }
        else
        {
            // Player is too far, switch to run animation
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
            //moves the zombie towards the user
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
        }
    }

    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 hitPoint)
    {
        health -= weapon.GetDamage();
        HealthSlider.value = health;
        Debug.Log(health);
    }

    private void Die()
    {  
        Destroy(gameObject); // Remove the zombie from the scene
    }
}


// EnemySpawner.cs

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject theHealth;
    public int minxPos;
    public int maxxPos;
    public int minzPos;
    public int maxzPos;
    public int maxEnemies;

    private int EnemyCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (EnemyCount < maxEnemies)
        {
            // Randomize spawn position
            float xPos = Random.Range(minxPos, maxxPos) + transform.position.x;
            float zPos = Random.Range(minzPos, maxzPos) + transform.position.z;
            Vector3 position = new Vector3(xPos, 0, zPos);
            float yPos = Terrain.activeTerrain.SampleHeight(position);

            // Instantiate enemy at random position
            // Instantiate(theEnemy, new Vector3(minxPos, 3, minxPos), Quaternion.identity);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);

            // Adjust spawn rate
            float spawnRate = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(spawnRate);

            EnemyCount++;
        }
    }
}


// EnvironmentalDamage.cs

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;  // Required for UnityEvent

public class EnvironmentalDamage : MonoBehaviour
{
    public float countdownDuration = 5f; // Duration of the countdown in seconds
    private float countdownTimer; // Timer for the countdown
    private bool isCountingDown = false; // Flag to indicate if countdown is active
    private bool isInsideSpecificCollider = true; // Flag to indicate if player is inside the specific collider

    private PlayerHealth playerhealth;
    private double totalDamageTaken = 0;
    private int totalDamageDocumented = 0;

    void Start()
    {
        StartCountdown();
        playerhealth = this.gameObject.GetComponentInChildren<PlayerHealth>();
        Debug.Log(playerhealth);
        Debug.Log(countdownTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countdownTimer);
        // If countdown is active, decrement the timer
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;
            Debug.Log(countdownTimer);

            // Check if countdown has reached 0
            if (countdownTimer <= 0)
            {
                Debug.Log("HHHH");
                // Perform action when countdown reaches 0
                PerformAction(1 * Time.deltaTime);
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is of the specific class
        if (other.CompareTag("Safe Zone"))
        {
            // Reset the countdown if the player exits the specific collider
            ResetCountdown();
            // Set flag indicating player is outside the specific collider
            isInsideSpecificCollider = false;
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the collider is of the specific class
        if (other.CompareTag("Safe Zone"))
        {
            // Set flag indicating player is inside the specific collider
            isInsideSpecificCollider = true;
            // Start the countdown if the player is inside the specific collider and the countdown is not already active
            if (!isCountingDown)
            {
                StartCountdown();
            }
        }
    }

    // Function to start the countdown
    void StartCountdown()
    {
        isCountingDown = true;
        countdownTimer = countdownDuration;
    }

    // Function to reset the countdown
    void ResetCountdown()
    {
        isCountingDown = false;
        countdownTimer = countdownDuration;
    }

    // Function to perform the action when countdown reaches 0
    void PerformAction(double num)
    {
        totalDamageTaken += num;
        if (totalDamageDocumented < (int)totalDamageTaken) {
            playerhealth.TakeDamage(1);
            totalDamageDocumented += 1;
        }
    }
}



// FadeScreen.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public AnimationCurve fadeCurve;
    public string colorPropertyName = "_Color";
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }
    
    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn,alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn,float alphaOut)
    {
        rend.enabled = true;

        float timer = 0;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));

            rend.material.SetColor(colorPropertyName, newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor(colorPropertyName, newColor2);

        if(alphaOut == 0)
            rend.enabled = false;
    }
}



// GameStartMenu.cs

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
            SceneTransitionManager.singleton.GoToSceneAsync(GetSceneIndex(selectedLevelName));
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



// HMDInfoManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Device Active " + XRSettings.isDeviceActive);
        Debug.Log("Device Name is " + XRSettings.loadedDeviceName);

        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No Headset plugged.");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "MockHMD Display" || XRSettings.loadedDeviceName == "Mock HMD" || XRSettings.loadedDeviceName == "MockHMDDisplay")){
            Debug.Log("Using Mock HMD");
        }
        else{
            Debug.Log("We have a headset " + XRSettings.loadedDeviceName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



// HandAnimatorController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
    }
}



// HealthBar.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// public class HealthBar : MonoBehaviour
// {
//     public Slider HealthSlider;
//     public float maxHealth = 100f;
//     public float health;
//     // Start is called before the first frame update
//     void Start()
//     {
//         health = maxHealth;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (HealthSlider.value != health)
//         {
//             HealthSlider.value = health;
//         }
//     }
// }

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Enemyfollow enemyFollow;

    void Start()
    {
        // Assign the Enemyfollow script reference
        enemyFollow = GameObject.FindWithTag("Zombie").GetComponent<Enemyfollow>();
        HealthSlider.maxValue = enemyFollow.health;
    }

    void Update()
    {
        Debug.Log(HealthSlider);
        HealthSlider.value = enemyFollow.health;
    }
}



// ITakeDamage.cs

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint);
}


// KeepUpright.cs

using UnityEngine;

public class KeepUpright : MonoBehaviour
{
    public Transform headTransform; // Drag the "head" of the zombie here in the Inspector
    private Rigidbody rb;
    private FixedJoint fixedJoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = headTransform.GetComponent<Rigidbody>(); // Assuming the head has a Rigidbody
        fixedJoint.anchor = Vector3.zero; // Center the joint
        fixedJoint.connectedAnchor = Vector3.zero; // Connect to the center of the head
        fixedJoint.enablePreprocessing = false; // Disable joint preprocessing for performance
        fixedJoint.massScale = 1; // Set mass scale to 1
        fixedJoint.connectedMassScale = 1; // Set connected mass scale to 1
    }
}



// MaterialTest.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



// MenuController.cs

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



// MeshHidder.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshHidder : MonoBehaviour
{
    private MeshRenderer[] meshes;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    public void Show()
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = false;
        }
    }
}


// PhysicsDamage.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsDamage : MonoBehaviour, ITakeDamage
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        rigidBody.AddForce(projectile.transform.forward * weapon.GetShootingForce(), ForceMode.Impulse);
    }
}


// PhysicsProjectile.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsProjectile : Projectile
{
    [SerializeField] private float lifeTime;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
        Destroy(gameObject, lifeTime);
    }

    public override void Launch()
    {
        base.Launch();
        rigidBody.AddRelativeForce(Vector3.forward * weapon.GetShootingForce(), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        ITakeDamage[] damageTakers = other.GetComponentsInParent<ITakeDamage>();

        foreach (var taker in damageTakers)
        {
            taker.TakeDamage(weapon, this, transform.position);
        }
    }
}


// Pistol.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.XR;

public class Pistol : Weapon
{
    [SerializeField] private Projectile bulletPrefab;
    private AudioSource audio;  // Just declare the field here

    [SerializeField] private int maxAmmo = 10;  // Maximum ammo capacity
    private int currentAmmo;  // Current ammo count
    public Text ammoDisplay;

    [SerializeField] private int maxMags = 5;  // Maximum magazine capacity
    private int currentMags;  // Current magazine count
    public Text magsDisplay;  // New Text variable for magazine display

    private void Start()
    {
        audio = GetComponent<AudioSource>();  // Initialize it in Start
        currentAmmo = maxAmmo;  // Initialize current ammo to max ammo
        ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
        currentMags = maxMags;  // Initialize current mags to max mags
        magsDisplay.text = currentMags.ToString() + "/" + maxMags.ToString();  // Update magsDisplay.text
    }

    private void Update()
    {
        // Check for 'R' key press on the keyboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentMags != 0 && currentAmmo == 0){
                Debug.Log("R key was pressed.");
                currentAmmo = maxAmmo;  // Reset ammo
                ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
                currentMags--;
                magsDisplay.text = currentMags.ToString() + "/" + maxMags.ToString();  // Update magsDisplay.text
            } else {
                Debug.Log("No mags left, cannot reload");
            }
        }

        // Check for primary button press on the Oculus Quest 2 controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            if (device.isValid)
            {
                bool inputValue;
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue)
                {
                    if (currentMags != 0){
                        Debug.Log("Controller key is pressed.");
                        currentAmmo = maxAmmo;  // Reset ammo
                        ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
                        currentMags--;
                        magsDisplay.text = currentMags.ToString() + "/" + maxMags.ToString();  // Update magsDisplay.text
                    } else {
                        Debug.Log("No mags left, cannot reload");
                    }
                }
            }
        }
    }

    protected override void StartShooting(XRBaseInteractor interactor)
    {
        base.StartShooting(interactor);
        if (currentAmmo > 0)  // Only shoot if there is ammo remaining
        {
            Shoot();
            audio.Play();
            currentAmmo--; // Decrease ammo count after shooting
            ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        projectileInstance.Init(this);
        projectileInstance.Launch();
        audio.Play();
        audio.Play();
    }

    protected override void StopShooting(XRBaseInteractor interactor)
    {
        base.StopShooting(interactor);
    }

    public void EmptyAmmo()
    {
        currentAmmo = 0;
        ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    private void OnEnable()
    {
        if (GameEvents.current != null)
        {
            GameEvents.current.onSupplyDropCollected.AddListener(AddMag);
            Debug.Log("Subscribed to onSupplyDropCollected event");
        }
        else
        {
            Debug.Log("Failed to subscribe to onSupplyDropCollected event because GameEvents.current is null");
        }
    }

    private void OnDisable()
    {
        if (GameEvents.current != null)
        {
            GameEvents.current.onSupplyDropCollected.RemoveListener(AddMag);
            Debug.Log("Unsubscribed from onSupplyDropCollected event");
        }
        else
        {
            Debug.Log("Failed to unsubscribe from onSupplyDropCollected event because GameEvents.current is null");
        }
    }


    public void AddMag()
    {
        Debug.Log("Add mag function ran");
        if (currentMags < maxMags)  // Only add mag if not already at max
        {
            currentMags++;
            magsDisplay.text = currentMags.ToString() + "/" + maxMags.ToString();  // Update magsDisplay.text
        }
    }
}



// PlayAudioFromAudioManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFromAudioManager : MonoBehaviour
{
    public string target;

    public void Play()
    {
        AudioManager.instance.Play(target);
    }

    public void Play(string audioName)
    {
        AudioManager.instance.Play(audioName);
    }
}



// PlayerController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    public void ResetPosition (){
        // always rotate first before resetting the position
        var rotationAngleY = playerHead.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;

        player.transform.Rotate(0, -rotationAngleY, 0);
        

        //set the position of the player
        var distanceDiff = resetTransform.position - playerHead.transform.position;

        player.transform.position +=  distanceDiff;
    }
}



// PlayerHealth.cs

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



// Projectile.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Weapon weapon;
    
    public virtual void Init(Weapon weapon)
    {
        this.weapon = weapon;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    public virtual void Launch()
    {

    }
}



// SceneTransitionManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public static SceneTransitionManager singleton;

    private void Awake()
    {
        if (singleton && singleton != this)
            Destroy(singleton);

        singleton = this;
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}



// SetOptionFromUI.cs

using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public TMPro.TMP_Dropdown turnDropdown;
    public SetTurnTypeFromPlayerPref turnTypeFromPlayerPref;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetTurnPlayerPref(int value)
    {
        PlayerPrefs.SetInt("turn", value); 
        turnTypeFromPlayerPref.ApplyPlayerPref();
    }
}



// SetTurnTypeFromPlayerPref.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnTypeFromPlayerPref : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    // Start is called before the first frame update
    void Start()
    {
        ApplyPlayerPref();
    }

    public void ApplyPlayerPref()
    {
        if(PlayerPrefs.HasKey("turn"))
        {
            int value = PlayerPrefs.GetInt("turn");
            if(value == 0)
            {
                snapTurn.leftHandSnapTurnAction.action.Enable();
                snapTurn.rightHandSnapTurnAction.action.Enable();
                continuousTurn.leftHandTurnAction.action.Disable();
                continuousTurn.rightHandTurnAction.action.Disable();
            }
            else if(value == 1)
            {
                snapTurn.leftHandSnapTurnAction.action.Disable();
                snapTurn.rightHandSnapTurnAction.action.Disable();
                continuousTurn.leftHandTurnAction.action.Enable();
                continuousTurn.rightHandTurnAction.action.Enable();
            }
        }
    }
}



// Shadow.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Working!");
            // PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damage);
            // }
        }
    }
}



// UIAudio.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string clickAudioName;
    public string hoverEnterAudioName;
    public string hoverExitAudioName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickAudioName != "")
        {
            AudioManager.instance.Play(clickAudioName);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnterAudioName != "")
        {
            AudioManager.instance.Play(hoverEnterAudioName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverExitAudioName != "")
        {
            AudioManager.instance.Play(hoverExitAudioName);
        }
    }
}



// Weapon.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] private float recoilForce;
    [SerializeField] private float damage = 10f;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onSelectEntered.AddListener(PickUpWeapon);
        interactableWeapon.onSelectExited.AddListener(DropWeapon);
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    private void PickUpWeapon(XRBaseInteractor interactor)
    {
        // interactor.GetComponent<MeshHidder>().Hide();
    }
 
    private void DropWeapon(XRBaseInteractor interactor)
    {
        // interactor.GetComponent<MeshHidder>().Show();

    }

    protected virtual void StartShooting(XRBaseInteractor interactor)
    {

    }

    protected virtual void StopShooting(XRBaseInteractor interactor)
    {

    }

    protected virtual void Shoot()
    {
        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }
}


// ZombieDamageDealer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageDealer : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}


// SetTurnType.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    public void SetTypeFromIndex(int index)
    {
        if (index == 0)
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
        else if (index == 1)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
    }
}



// SupplyDrop.cs

using UnityEngine;

public class SupplyDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // Do something
            Debug.Log("Player has collected the supply drop!");

            // Trigger the event
            GameEvents.current.SupplyDropCollected();
            Debug.Log("Event triggered");

            // Destroy the supply drop
            Destroy(gameObject);
        }
    }
}



// TimerCountdown.cs

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;  // Required for UnityEvent

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    private Text countdownDisplay;

    public UnityEvent OnCountdownFinished;  // Event that gets triggered when countdown finishes

    private void Start()
    {
        countdownDisplay = GetComponent<Text>();
        countdownTime = 120;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        OnCountdownFinished.Invoke();  // Trigger the event when countdown finishes
    }
}



