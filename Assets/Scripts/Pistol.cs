using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.XR;

public class Pistol : Weapon
{
    [SerializeField] private Projectile bulletPrefab;
    private AudioSource audio;  // Just declare the field here

    [SerializeField] private int maxAmmo = 10;  // Maximum ammo capacity
    private int currentAmmo;  // Current ammo count
    public Text ammoDisplay;

    private void Start()
    {
        audio = GetComponent<AudioSource>();  // Initialize it in Start
        currentAmmo = maxAmmo;  // Initialize current ammo to max ammo
        ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
    }

    private void Update()
    {
        // Check for 'R' key press on the keyboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key was pressed.");
            currentAmmo = maxAmmo;  // Reset ammo
            ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
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
                    Debug.Log("Primary button was pressed.");
                    currentAmmo = maxAmmo;  // Reset ammo
                    ammoDisplay.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();  // Update ammoDisplay.text
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
}
