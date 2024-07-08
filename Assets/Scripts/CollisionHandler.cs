using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip crash;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _isTransitioning = false;

    }
    void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning) return;
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                _isTransitioning = true;
                Debug.Log("Finish");
                StartSuccessSequence();

                break;
            default:
                _isTransitioning = true;
                Debug.Log("Default");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(crash);
        // todo: add particle effect
        GetComponent<Movement>().enabled = false;
        //note that the script can be accessed in a method, not just in start!
        Invoke("ReloadLevel", delayTime);
    }

    void StartSuccessSequence()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(success);
        // todo: add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
