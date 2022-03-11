using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 20f;

    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        float xOffset = xMove * Time.deltaTime * controlSpeed;
        float newXPosition = transform.localPosition.x + xOffset;

        float yOffset = yMove * Time.deltaTime * controlSpeed;
        float newYPosition = transform.localPosition.y + yOffset;
        
        transform.localPosition = new Vector3 (newXPosition, newYPosition, transform.localPosition.z);
        
    }
}
