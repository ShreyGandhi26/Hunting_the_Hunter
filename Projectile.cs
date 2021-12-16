using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public static float damage = 20;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    }
}
