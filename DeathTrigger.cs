using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    public GameObject VFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            Instantiate(VFX, other.GetComponent<PlayerCharacter>().transform.position, Quaternion.identity);
            SceneManager.LoadScene(GameManager.Instance.sceneName);
        }
    }
}
