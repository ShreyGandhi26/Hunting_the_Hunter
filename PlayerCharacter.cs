using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public float MovementSpeed;
    public GameObject Hand;
    public GameObject Gun;
    public GameObject Jetpack;
    public GameObject hunterCaught;
    public GameObject hunterCage;
    public Animator anim;
    public LayerMask layer;
    public float Jetforce;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public static bool m_FacingRight = true;
    private float movement;
    public float maxfuel;
    public float fuel;
    public float maximumHealth;
    public static float currentHealth = 100;
    public StatusIndicator _status;
    public UI_script _fuel;
    public GameObject hitEffect;
    public GameObject UpgradeScreen;
    public GameObject PauseMenu;
    bool isPlaying = false;

    private void Awake()
    {
        currentHealth = maximumHealth;
        m_FacingRight = true;
        _status.transform.Rotate(0, 0, 0);
        transform.Rotate(0f, 0f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        fuel = maxfuel;
        //GameManager.Instance.getScore(GameManager.Score);
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        if (Mathf.Abs(movement) == 0)
        {
            audioSource.Stop();
            isPlaying = false;
        }
        if (Mathf.Abs(movement) > 0 && isPlaying == false)
        {
            audioSource.Play();
            isPlaying = true;
        }
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(movement));

        if (movement > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
            audioSource.Play();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movement < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
            audioSource.Play();
        }


        if (currentHealth <= 0)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            SceneManager.LoadScene(GameManager.Instance.sceneName);
            Destroy(gameObject);
        }
        if (currentHealth >= 100)
        {
            currentHealth = maximumHealth;
        }
        if (_status != null)
        {
            _status.setHealth(currentHealth, maximumHealth);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeScreen.SetActive(!UpgradeScreen.activeSelf);
        }
        if (UpgradeScreen.activeSelf == true || PauseMenu.activeSelf == true)
        {
            Time.timeScale = 0f;
        }
        else if (UpgradeScreen.activeSelf == false || PauseMenu.activeSelf == false)
        {
            Time.timeScale = 1f;
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && fuel >= 0 && Jetpack.activeSelf)
        {

            rb.AddForce(Vector3.up * Jetforce, ForceMode2D.Impulse);
            fuel -= Time.deltaTime;
            _fuel.setFuel(fuel, maxfuel);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Jetpack.GetComponent<AudioSource>().Play();
        }
        if (isGrounded() && fuel <= 1)
        {
            fuel += Time.deltaTime;
            _fuel.setFuel(fuel, maxfuel);
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        _status.transform.Rotate(0, 180, 0);
        transform.Rotate(0f, 180f, 0f);
    }
    private bool isGrounded()
    {
        float Offset = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider2D>().bounds.extents.y + Offset, layer);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HunterCharacter>() != null)
        {
            GameManager.Score += 500;
            GameManager.Instance.getScore(GameManager.Score);
            currentHealth += 10f;
            Vector2 pos = other.GetComponent<HunterCharacter>().transform.position;
            Quaternion rot = other.GetComponent<HunterCharacter>().transform.rotation;
            Instantiate(hunterCaught, pos, rot);
            Instantiate(hunterCage, pos, rot);
            Destroy(other.GetComponent<HunterCharacter>().gameObject);
        }
        if (other.CompareTag("Jetpack") == true)
        {
            Jetpack.SetActive(true);
            _fuel.fueldata.SetActive(true);
            Destroy(GameObject.FindWithTag("Jetpack"));
        }
        if (other.GetComponent<HunterProjectile>() != null)
        {
            Hand.GetComponent<AudioSource>().Play();
        }
    }
}
