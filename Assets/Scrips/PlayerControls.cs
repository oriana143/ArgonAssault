using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yDownRange = 3f;
    [SerializeField] float yUpRange = 10f;

    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -15f;
    [SerializeField] float controlPitchFactor = -10f;



    float xMove, yMove;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

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
        float clampedYPos = Mathf.Clamp(rawYPosition, -yDownRange, yUpRange);

        transform.localPosition = new Vector3(clampedXPOS, clampedYPos, transform.localPosition.z);
    }
}
