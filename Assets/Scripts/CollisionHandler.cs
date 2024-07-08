using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                Debug.Log("Finish");
                break;
            default:
                Debug.Log("Default");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // todo: add SFX upon crash
        // todo: add particle effect
        GetComponent<Movement>().enabled = false;
        //note that the script can be accessed in a method, not just in start!
        Invoke("ReloadLevel", delayTime);
    }

    void StartSuccessSequence()
    {
        // todo: add SFX upon crash
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
