using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                Destroy(other.gameObject);
                break;
            default:
                Debug.Log("Default");
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int curentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curentSceneIndex);
    }
}
