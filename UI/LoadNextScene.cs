using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string levelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
