using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] ParticleSystem playerCrashParticles;

    

    void OnTriggerEnter(Collider other) 
    {
       // Debug.Log(this.name + "--Triggered with--" + other.gameObject.name);
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        playerCrashParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
