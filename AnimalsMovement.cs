using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalsMovement : MonoBehaviour
{
    public float movement = 5f;
    public PlayerCharacter player;
    float MovementSpeed = 4f;
    bool moveRight = true;
    Vector3 currentTransform;
    SpriteRenderer sr;

    private void Awake()
    {
        currentTransform = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()

    {
        if (transform.position.x >= currentTransform.x + movement)
        {
            moveRight = false;
        }
        if (transform.position.x < currentTransform.x - movement)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y);
            sr.flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y);
            sr.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>() != null)
        {
            SceneManager.LoadScene(GameManager.Instance.sceneName);
        }
    }
}
