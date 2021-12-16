using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonotDestroyMusic : MonoBehaviour
{
    private Scene currentScene;
    public string sceneName;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MenuMusic");

        if (musicObj.Length > 0)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (sceneName != "Menu")
        {
            Destroy(gameObject);
        }
    }
}
