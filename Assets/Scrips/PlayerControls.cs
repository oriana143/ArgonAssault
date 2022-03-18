using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header ("General Setup Settings")]
    [Tooltip ("How fast ship moves up and down")] [SerializeField] float controlSpeed = 20f;
    [Tooltip ("How far ship goes left and right")] [SerializeField] float xRange = 20f;
    [Tooltip ("How far ship goes up and down")] [SerializeField] float yRange = 10f;

    [Header ("Laser gun array")]
    [Tooltip ("Add all player lasers here")] [SerializeField] GameObject[] lasers;

    [Header ("Screen position based tuning")]

    [Tooltip ("Pitch = X")] [SerializeField] float positionPitchFactor = -2.5f;
    [Tooltip ("Yaw = Y")][SerializeField] float positionYawFactor = 3f;

    [Header ("Player input based tuning")]
    [Tooltip ("Roll = Z")][SerializeField] float controlRollFactor = -15f;
    [Tooltip ("Pitch = X")][SerializeField] float controlPitchFactor = -10f;


    float xMove, yMove;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlMove = yMove * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlMove;  // pitch = X
        float yaw = transform.localPosition.x * positionYawFactor;  //  yaw = Y
        float roll = xMove * controlRollFactor ;//   roll = Z
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        float xOffset = xMove * Time.deltaTime * controlSpeed;
        float rawXPosition = transform.localPosition.x + xOffset;
        float clampedXPOS = Mathf.Clamp(rawXPosition, -xRange, xRange);

        float yOffset = yMove * Time.deltaTime * controlSpeed;
        float rawYPosition = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPOS, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }  
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
