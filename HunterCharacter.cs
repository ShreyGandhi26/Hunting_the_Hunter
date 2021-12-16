using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HunterCharacter : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 30f;
    public float pathUpdateSeconds = 0f;

    [Header("Physics")]
    public float speed = 400f;
    public float JumpSpeed = 100f;
    public float nextWayPointDistance = 3f;
    public float jumpNodeHeight = .8f;
    public float JumpModefier = .3f;
    public float JumpCheckOffset = .1f;
    public Vector3 StoppingDist;

    [Header("Custom Behaviour")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;
    public LayerMask layer;

    [Header("Bullet")]
    public GameObject bullet;
    public GameObject bulletVfx;
    public Transform spawnPoint;
    public float timeBTWshot;
    public float maxHealth;
    public float Currenthealth;
    public StatusIndicator _statusIndicator;
    public GameObject hitEffect;

    private Path path;
    private bool m_FacingRight;
    private float t = -2;
    private int currentWaypoint = 0;
    private float timer;
    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D capsuleCollider2D;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        Currenthealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (TargetInDistance())
        {
            if (timeBTWshot < t)
            {
                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Instantiate(bulletVfx, spawnPoint.position, spawnPoint.rotation);
                t = 0;
            }
        }
        if (Currenthealth <= 0)
        {
            GameManager.Score += 200;
            GameManager.Instance.getScore(GameManager.Score);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (_statusIndicator != null)
        {
            _statusIndicator.setHealth(Currenthealth, maxHealth);
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position + StoppingDist, OnPathComplete);
        }

        if (isGrounded() == false)
        {
            //Destroy(gameObject);
        }

    }



    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }



        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (jumpEnabled && isGrounded())
        {
            if (direction.y > jumpNodeHeight)
            {
                rb.AddForce(Vector2.up * JumpSpeed * JumpModefier);
            }
        }

        rb.AddForce(force);
        anim.SetFloat("ForwardForce", Mathf.Abs(force.x));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        if (directionLookEnabled)
        {
            if (force.x > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (force.x < 0 && m_FacingRight)
            {
                Flip();
            }
        }

    }
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        _statusIndicator.transform.Rotate(0, 180, 0);
        transform.Rotate(0f, 180f, 0f);
    }

    public bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + JumpCheckOffset, layer);
        Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (GetComponent<Collider2D>().bounds.extents.y + JumpCheckOffset), Color.red);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>() != null)
        {
            Currenthealth -= Projectile.damage;
            audioSource.Play();
            _statusIndicator.setHealth(Currenthealth, maxHealth);
            Destroy(other.GetComponent<Projectile>().gameObject);
        }
    }
}
