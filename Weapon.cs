using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float offset;
    //public float Yoffset;
    public static float recoilForce = 10f;
    public GameObject projectile;
    public Transform SpawnPoint;
    public GameObject player;
    public GameObject destroyEffect;

    private float timeBTWShot;
    public float startTimeBTWShot;

    private Vector3 lookDir;
    private bool isfacingLeft;


    private void Update()
    {
        Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        // rotZ = Mathf.Clamp(rotZ, -120, 50);
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        SpawnPoint.rotation = Quaternion.Euler(0f, 0f, rotZ);


        if (timeBTWShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(destroyEffect, SpawnPoint.position, SpawnPoint.rotation);
                Instantiate(projectile, SpawnPoint.position, SpawnPoint.rotation);
                lookDir = new Vector3(Mathf.Cos(Mathf.Abs(transform.localEulerAngles.z * Mathf.Deg2Rad)), Mathf.Sin(Mathf.Abs(transform.localEulerAngles.z * Mathf.Deg2Rad)), 0);

                player.GetComponent<Rigidbody2D>().AddForce(-lookDir * recoilForce, ForceMode2D.Impulse);
                timeBTWShot = startTimeBTWShot;
            }
        }
        else
        {
            timeBTWShot -= Time.deltaTime;
        }
    }
}
