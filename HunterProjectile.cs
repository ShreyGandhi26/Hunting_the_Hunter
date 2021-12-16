using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HunterProjectile : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public float damage;
    public Rigidbody2D rb;
    AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke("DestroyProjectile", lifeSpan);
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            PlayerCharacter.currentHealth -= damage;
            other.GetComponent<PlayerCharacter>()._status.setHealth(PlayerCharacter.currentHealth, other.GetComponent<PlayerCharacter>().maximumHealth);
        }
    }
}
